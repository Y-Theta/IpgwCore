using System;
using System.Collections.Generic;
using System.IO;
using NEUH_PluginControl;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mscoree;
using System.Security.Permissions;

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

        private AppDomain _pluginsDomain;

        private PluginControl _control;
        public PluginControl Control {
            get => _control;
            set => _control = value;
        }

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
            _pluginsDomain.AssemblyResolve += App.MyResolveEventHandler;
            _control = (PluginControl)_pluginsDomain.CreateInstanceAndUnwrap(typeof(PluginControl).Assembly.FullName, typeof(PluginControl).FullName);
            _control.PluginsPath = App.Root + App.Plugin;
            InitFloderWatcher();
        }

        private async void ReLoad() {
            await App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() => {
                var tempdomain = AppDomain.CreateDomain("HostAdapter", AppDomain.CurrentDomain.Evidence, _pluginsDomainsetup);
                var tempcontrol = (PluginControl)tempdomain.CreateInstanceAndUnwrap(typeof(PluginControl).Assembly.FullName, typeof(PluginControl).FullName);
                tempcontrol.PluginsPath = App.Root + App.Plugin;
                AppDomain.Unload(_pluginsDomain);
                _pluginsDomain = tempdomain;
                _control = tempcontrol;
            }));
        }

        public void Update() {
            ReLoad();
        }

        public string ShowDomains() {
            string str = "AppDomains \n";
            List<AppDomain> appDomains = new List<AppDomain>();

            IntPtr handle = IntPtr.Zero;
            CorRuntimeHost host = new CorRuntimeHost();
            try {
                host.EnumDomains(out handle);
                while (true) {
                    object domain;
                    host.NextDomain(handle, out domain);
                    if (domain == null)
                        break;
                    appDomains.Add((AppDomain)domain);
                }
            }
            finally {
                host.CloseEnum(handle);
            }

            foreach (var domain in appDomains)
                str += $"{appDomains.IndexOf(domain)} : {domain.FriendlyName.PadRight(20)} \n";
            return str;
        }

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
            _control.UpdatePlugins();
        }

        private void _updatewatcher_Changed(object sender, FileSystemEventArgs e) {
            _updatewatcher.EnableRaisingEvents = false;
            ReLoad();
            _updatewatcher.EnableRaisingEvents = true;
        }
        #endregion

        #region Constructors
        public PluginServices() { Load(); }
        #endregion
    }

}
