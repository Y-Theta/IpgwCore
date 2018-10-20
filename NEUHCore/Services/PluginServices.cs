using System;
using System.Collections.Generic;
using System.IO;
using NEUH_PluginControl;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mscoree;
using System.Security.Permissions;
using NEUHCore.UserSetting;
using NEUH_Contract;
using System.Reflection;

namespace NEUHCore.Services {
    public class PluginServices {
        #region Properties
        private static PluginServices _instence;
        private static readonly object _singleton_Lock = new object();
        public static PluginServices Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new PluginServices();
                return _instence;
            }
        }

        public event PluginChangedEventHandler OnPluginChanged;

        private PluginControl _control;
        public PluginControl Control {
            get => _control;
            set => _control = value;
        }

        private AppDomain _pluginsDomain;

        private FileSystemWatcher _updatewatcher;

        private AppDomainSetup _pluginsDomainsetup;
        #endregion

        #region Methods
        private void Load() {
            _pluginsDomainsetup = new AppDomainSetup {
                CachePath = App.Root + App.Cache,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = App.Root + App.Plugin
            };
            _pluginsDomain = AppDomain.CreateDomain("HostAdapter", AppDomain.CurrentDomain.Evidence, _pluginsDomainsetup);
            _pluginsDomain.SetData(App.LibsPath, new string[] {
                App.Root + App.Component,
                App.Root + App.Plugin
                });
            AssemblyServices.HockResolve(_pluginsDomain);
            _control = (PluginControl)_pluginsDomain.CreateInstanceAndUnwrap(typeof(PluginControl).Assembly.FullName, typeof(PluginControl).FullName);
            _control.PluginsPath = App.Root + App.Plugin;
            foreach (var item in CommonSettings.Instence.DisabledPlugins)
                _control.EnabledPlugins.Remove(item);
            _control.UpdatePlugins();
            InitFloderWatcher();
        }

        private async Task ReLoad() {
            await App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => {
                var tempdomain = AppDomain.CreateDomain("HostAdapter", AppDomain.CurrentDomain.Evidence, _pluginsDomainsetup);
                tempdomain.SetData(App.LibsPath, new string[] {
                App.Root + App.Component,
                App.Root + App.Plugin
                });
                AssemblyServices.HockResolve(tempdomain);
                var tempcontrol = (PluginControl)tempdomain.CreateInstanceAndUnwrap(typeof(PluginControl).Assembly.FullName, typeof(PluginControl).FullName);
                tempcontrol.PluginsPath = App.Root + App.Plugin;
                foreach (var item in CommonSettings.Instence.DisabledPlugins)
                    tempcontrol.EnabledPlugins.Remove(item);
                tempcontrol.UpdatePlugins();
                AppDomain.Unload(_pluginsDomain);
                _pluginsDomain = tempdomain;
                _control = tempcontrol;
            }));
        }

        public async void Update() {
            await ReLoad();
        }

        public async void Unload(string name) {
            CommonSettings.Instence.DisabledPlugins.Add(name);
            await ReLoad();
            OnPluginChanged?.Invoke(this, new PluginChangedArgs(name, PluginAction.Unload));
        }

        public async void Load(string name) {
            if (CommonSettings.Instence.DisabledPlugins.Contains(name))
                CommonSettings.Instence.DisabledPlugins.Remove(name);
            await ReLoad();
            OnPluginChanged?.Invoke(this, new PluginChangedArgs(name, PluginAction.Load));
        }

        //public string ShowDomains() {
        //    string str = "AppDomains \n";
        //    List<AppDomain> appDomains = new List<AppDomain>();

        //    IntPtr handle = IntPtr.Zero;
        //    CorRuntimeHost host = new CorRuntimeHost();
        //    try {
        //        host.EnumDomains(out handle);
        //        while (true) {
        //            object domain;
        //            host.NextDomain(handle, out domain);
        //            if (domain == null)
        //                break;
        //            appDomains.Add((AppDomain)domain);
        //        }
        //    }
        //    finally {
        //        host.CloseEnum(handle);
        //    }
        //    foreach (var domain in appDomains)
        //        str += $"{appDomains.IndexOf(domain)} : {domain.FriendlyName.PadRight(20)} \n";
        //    return str;
        //}

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void InitFloderWatcher() {
            _updatewatcher = new FileSystemWatcher {
                Path = App.Root + App.Plugin,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess,
                Filter = "*.dll",
                IncludeSubdirectories = false
            };
            _updatewatcher.Changed += _updatewatcher_Changed;
            _updatewatcher.Deleted += _updatewatcher_Deleted;
            _updatewatcher.EnableRaisingEvents = true;
        }

        private void _updatewatcher_Deleted(object sender, FileSystemEventArgs e) {
            App.Current.Dispatcher.Invoke(new Action(() => {
                OnPluginChanged?.Invoke(this, new PluginChangedArgs(e.Name.Split('.').First(), PluginAction.Unload));
            }), System.Windows.Threading.DispatcherPriority.Normal);
            _control.UpdatePlugins();
        }

        private async void _updatewatcher_Changed(object sender, FileSystemEventArgs e) {
            _updatewatcher.EnableRaisingEvents = false;
            await ReLoad();
            _updatewatcher.EnableRaisingEvents = true;
        }
        #endregion

        #region Constructors
        public PluginServices() { Load(); }
        #endregion
    }
}