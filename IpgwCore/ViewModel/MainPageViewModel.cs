using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IpgwCore.MVVMBase;
using IpgwCore.Services.MessageServices;
using PMS = IpgwCore.Services.MessageServices.PopupMessageServices;

namespace IpgwCore.ViewModel {
    internal class MainPageViewModel : ViewModelBase, IMessagePoster {
        #region Properties
        public object _message { get => Msg; set => Msg = value.ToString(); }

        private string _msg;
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg {
            get => _msg;
            set => SetValue(out _msg, value, Msg);
        }

        private string _exittip;
        /// <summary>
        /// 退出提示
        /// </summary>
        public string ExitTip {
            get => _exittip;
            set => SetValue(out _exittip, value, ExitTip);
        }

        private bool _mainpage;
        /// <summary>
        /// 是否在主页
        /// </summary>
        public bool Mainpage {
            get => _mainpage;
            set => SetValue(out _mainpage, value, Mainpage);
        }


        public CommandBase Operation { get; set; }

        public CommandBase Nvigate { get; set; }


        public event CommandActionEventHandler CommandOperation;

        #endregion

        #region Methods
        private void InitCommand() {
            Operation = new CommandBase(obj => CommandOperation?.Invoke(this, new CommandArgs(obj, "Operation")));
            Nvigate = new CommandBase(obj => CommandOperation?.Invoke(this, new CommandArgs(obj, "Nvigate")));
        }

        #endregion

        #region Constructors
        public MainPageViewModel() {
            PMS.Instence.MessageHolder = this;
            InitCommand();
        }
        #endregion
    }

}
