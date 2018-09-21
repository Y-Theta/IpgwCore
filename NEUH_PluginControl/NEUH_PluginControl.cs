using NEUH_Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NEUH_PluginControl
{
    public class PluginControl : MarshalByRefObject {
        #region Properties
        [ImportMany(typeof(INEUHCoreContract), AllowRecomposition = true)]
        private List<INEUHCoreContract> _addInContracts;
        public List<INEUHCoreContract> AddInContracts {
            get => PluginsCheck();
            set => _addInContracts = value;
        }

        private String _pluginsPath;
        public String PluginsPath {
            get => _pluginsPath;
            set => InitPlugins(value);
        }

        private HashSet<string> _enabledPlugins;
        public HashSet<string> EnabledPlugins {
            get => _enabledPlugins;
            set => _enabledPlugins = value;
        }

        private Dictionary<string, string> _pluginNameReflection;
        public Dictionary<string, string> PluginNameReflection {
            get => _pluginNameReflection;
            set => _pluginNameReflection = value;
        }

        private CompositionContainer _container;

        private DirectoryCatalog _catalog;

        #endregion

        #region Methods

        private void InitPlugins(string path) {
            _pluginsPath = path;
            _catalog = new DirectoryCatalog(_pluginsPath);
            _container = new CompositionContainer(_catalog);
            _enabledPlugins = new HashSet<string>();
            _pluginNameReflection = new Dictionary<string, string>();
            foreach (var plugins in _container.GetExportedValues<INEUHCoreContract>())
                _enabledPlugins.Add(plugins.Name);
        }

        public void UpdatePlugins() {
            _catalog.Refresh();
            _container.ComposeExportedValue(_catalog.Parts);
            if (_enabledPlugins is null)
                AddInContracts = _container.GetExportedValues<INEUHCoreContract>().ToList();
            else {
                AddInContracts = new List<INEUHCoreContract>();
                foreach (var plugins in _container.GetExportedValues<INEUHCoreContract>().ToList())
                    if (_enabledPlugins.Contains(plugins.Name))
                        AddInContracts.Add(plugins);
            }
            if(AddInContracts.Count > 0)
                foreach (Assembly asy in AppDomain.CurrentDomain.GetAssemblies()) {
                    Console.WriteLine(asy.GetName().Name + "   " + asy.Location);
                    PluginNameReflection.Add(asy.Location, asy.GetName().Name);
                }
        }

        public string ShowPlugins() {
            string str = "All Plugins \n";
            foreach (var plugin in AddInContracts)
                str += String.Format("-{0}-\n{1}\n", AddInContracts.IndexOf(plugin), plugin.InfoStringFormat());
            return str;
        }

        private List<INEUHCoreContract> PluginsCheck() {
            if (_addInContracts is null)
                return new List<INEUHCoreContract>();
            else
                return _addInContracts;
        }
        #endregion

        #region Constructors
        public PluginControl() { }
        #endregion
    }
}
