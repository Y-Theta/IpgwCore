using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEUH_PluginControl {
    public delegate void PluginChangedEventHandler(object sender, PluginChangedArgs args);

    public class PluginChangedArgs : EventArgs {
        #region Properties
        public string Name { get; set; }

        public PluginAction Action { get; set; }
        #endregion

        #region Constructors
        public PluginChangedArgs(string name, PluginAction action) {
            Name = name;
            Action = action;
        }
        #endregion
    }

    public enum PluginAction {
        Load,
        Unload
    }
}