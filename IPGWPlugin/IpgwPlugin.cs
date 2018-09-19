using NEUH_Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPGWPlugin
{
    [Export(typeof(INEUHCoreContract))]
    public class IpgwPlugin : CoreContractBase {

        public override void Run(UseCase c) {
            
        }

        public override void Run(UseCase c, out object t) {

            t = null;
        }

        public IpgwPlugin() {
            Name = "IpgwCore";
            Author = "Y_Theta";
            Edition = "1.0.0.0";
            Description = "use to monite ipgw flux";
        }
    }
}
