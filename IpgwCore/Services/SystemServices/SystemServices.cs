using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpgwCore.Services.MessageServices;

namespace IpgwCore.Services.SystemServices {
   internal class SystemServices {
        #region Properties
        private static SystemServices _instence;
        private static readonly object _singleton_Lock = new object();
        public static SystemServices Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new SystemServices();
                return _instence;
            }
        }

        #endregion

        #region Methods


        private bool IsExistKey(string keyName) {
            bool _exist = false;
            try
            {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                RegistryKey runs = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null)
                {
                    RegistryKey key2 = localKey.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName)
                {
                    if (strName.ToUpper() == keyName.ToUpper())
                    {
                        _exist = true;
                        return _exist;
                    }
                }
            }
            catch { }
            return _exist;
        }

        private bool SelfRunning(bool isStart, string exeName, string path) {
            try
            {
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                RegistryKey key = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    localKey.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                if (isStart)//若开机自启动则添加键值对
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames)
                    {
                        if (keyName.ToUpper() == exeName.ToUpper())
                        {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception) { }

            return true;
        }

        public void SetSelfRunning(bool isStart) {
            if (!IsExistKey("IpgwCore") && isStart)
            {
                SelfRunning(isStart, "IpgwCore", Process.GetCurrentProcess().MainModule.FileName + " -s");
                PopupMessageServices.Instence.ShowContent("已设置开机自启动");
            }
            else if (IsExistKey("IpgwCore") && !isStart)
            {
                SelfRunning(isStart, "IpgwCore", Process.GetCurrentProcess().MainModule.FileName + " -s");
                PopupMessageServices.Instence.ShowContent("已取消开机自启动");
            }
        }

        #endregion

        #region Constructors
        #endregion
    }

}
