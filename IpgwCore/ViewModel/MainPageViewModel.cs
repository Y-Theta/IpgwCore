using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpgwCore.MVVMBase;

namespace IpgwCore.ViewModel {
    internal class MainPageViewModel : ViewModelBase {
        #region Properties
        private string _name;
        public string Name {
            get => _name;
            set => SetValue(out _name, value, Name);
        }
        #endregion

        #region Methods
        #endregion

        #region Constructors
        #endregion
    }

}
