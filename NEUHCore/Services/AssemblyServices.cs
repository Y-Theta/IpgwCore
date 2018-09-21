using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YFrameworkBase;

namespace NEUHCore.Services {
   public class AssemblyServices {
        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// 将这个方法关联到需要从其它地方载入程序集的程序域(AppDomain)上
        /// 并设置此程序域的GetData为程序集所在路径地址集合
        /// 以"LibsPath"为键值
        /// </summary>
        public static void HockResolve(AppDomain dom) {
            dom.AssemblyResolve += MyResolveEventHandler;
        }

        public static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args) {
            Assembly MyAssembly;
            String MisAssembly= "";
            if (args.Name.Contains(".resources")) {
                return null;
            }
            string[] Directorys = (string[])((AppDomain)sender).GetData("LibsPath");
            foreach (var dir in Directorys) {
                string[] items = Directory.GetFileSystemEntries(dir, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
                if (items.Count() > 0) {
                    MisAssembly = items.First();
                    break;
                }
            }
            MyAssembly = Assembly.LoadFrom(MisAssembly);

            return MyAssembly;
        }
        #endregion

        #region Constructors
        static AssemblyServices() {}
        #endregion
    }
}
