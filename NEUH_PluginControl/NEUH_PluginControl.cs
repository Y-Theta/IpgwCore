using NEUH_Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEUH_PluginControl
{
    public class NEUH_PluginControl:MarshalByRefObject
    {
        #region Properties
        [ImportMany(typeof(INEUHCoreContract), AllowRecomposition = true)]
        private List<INEUHCoreContract> _iAddInContracts;
        public List<INEUHCoreContract> IAddInContracts {
            get => _iAddInContracts;
            set => _iAddInContracts = value;
        }

        private CompositionContainer _container;

        private DirectoryCatalog _catalog;

        private String _pluginsPath;
        public String PluginsPath {
            get => _pluginsPath;
            set => InitPlugins(value);
        }
        #endregion

        #region Methods

        private void InitPlugins(string path) {
            _pluginsPath = path;
            _catalog = new DirectoryCatalog(_pluginsPath); 
            _container = new CompositionContainer(_catalog);
            _container.ComposeExportedValue(_container);
            _iAddInContracts = _container.GetExportedValues<INEUHCoreContract>().ToList();
        }

        public void UpdatePlugins() {
            _catalog.Refresh();
            _container.ComposeExportedValue(_catalog.Parts);
            _iAddInContracts = _container.GetExportedValues<INEUHCoreContract>().ToList();
        }

        public string ShowPlugins() {
            string str = "All Plugins \n";
            foreach (var plugin in _iAddInContracts)
                str += String.Format("{0}\n", plugin.InfoStringFormat());
            return str;
        }
        #endregion

        #region Constructors
        public NEUH_PluginControl() { }
        #endregion
    }
}
