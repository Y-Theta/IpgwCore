using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEUH_Contract {
    public delegate void PluginActionEventHandler(object sender, PluginActionArgs args);

    public class PluginActionArgs : EventArgs {

        public string Name { get; set; }

        public CaseName Case { get; set; }

        public object ConvertValue { get; set; }


        public PluginActionArgs(string name,CaseName casen,object value) {
            Name = name;
            Case = casen;
            ConvertValue = value;
        }
    }
}
