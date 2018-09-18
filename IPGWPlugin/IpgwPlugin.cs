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
    public class IpgwPlugin : MarshalByRefObject, INEUHCoreContract {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string Description { get; set; }
        public UseCase[] Usage { get; set; }

        public string InfoStringFormat() {
            return String.Format($"Name        : {Name.PadLeft(20)}\n" +
                                 $"Author      : {Author.PadLeft(20)}\n" +
                                 $"Edition     : {Edition.PadLeft(20)}\n" +
                                 $"Description : {Description}\n");
        }

        public void Run(UseCase c) {
            
        }

        public void Run(UseCase c, out object t) {

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
