using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IpgwCore.MVVMBase {
    public class CommandBase : ICommand {
        #region Properties
        public event EventHandler CanExecuteChanged;

        private event EnableAction _ability;
        public event EnableAction Ability {
            add => _ability = value;
            remove => _ability -= value;
        }

        private event CommandAction _execution;
        public event CommandAction Execution {
            add => _execution = value;
            remove => _execution -= value;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter) {
            return _ability is null ? true : _ability.Invoke(parameter);
        }

        public void Execute(object parameter) {
            _execution?.Invoke(parameter);
        }
        #endregion

        #region Constructors
        public CommandBase(CommandAction action) {
            _execution += action;
        }
        #endregion
    }

}
