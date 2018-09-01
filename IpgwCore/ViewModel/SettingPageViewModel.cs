using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpgwCore.MVVMBase;

namespace IpgwCore.ViewModel {
    internal class SettingPageViewModel : ViewModelBase {
        #region Properties
        public CommandBase Operation { get; set; }

        public event CommandActionEventHandler CommandOperation;
        #endregion

        #region Methods
        private void InitCommand() {
            Operation = new CommandBase(obj => CommandOperation?.Invoke(this, new CommandArgs(obj, "Operation")));
        }
        #endregion

        #region Constructors
        public SettingPageViewModel() {
            InitCommand();
        }
        #endregion
    }
}