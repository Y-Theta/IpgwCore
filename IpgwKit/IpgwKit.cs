using NEUH_Contract;
using NEUHCore;
using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using NEUHCore.Services;

namespace IpgwKit {
    [Export(typeof(INEUHCoreContract))]
    public class IpgwPlugin : CoreContractBase {

        public override void Run(CaseName c) {
            switch (c) {
                case CaseName.UnLoad:
                    Invoke(this, new PluginActionArgs(Name, CaseName.UnLoad, null));
                    return;
                default:
                    return;
            }
        }

        public override void Run(CaseName c, out object t) {
            switch(c) {
                case CaseName.AreaIcon:
                    t = AreaIconServices.Instence.Drawicon();
                    return;
                default:
                    t = null;
                    return;
            }
        }

        public IpgwPlugin() {
            Name = "IpgwCore";
            Author = "Y_Theta";
            Edition = "1.0.0.0";
            Description = "use to monite ipgw flux";
            Usage = new Dictionary<CaseName, int> {
                { CaseName.MainWindowInit, 1 },
                { CaseName.AreaIcon, 1 }
            };
        }
    }
}
