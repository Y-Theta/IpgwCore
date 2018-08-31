using IpgwCore.Model.BasicModel;
using IpgwCore.MVVMBase;
using IpgwCore.Services.FormatServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpgwCore.Services.MessageServices;
using IpgwCore.Services.HttpServices;

namespace IpgwCore.ViewModel {
    internal class FluxInfoViewModel :ViewModelBase{
        #region Properties

        private bool _connected;
        public bool Connected {
            get => _connected;
            set => SetValue(out _connected, value, Connected);
        }


        private Flux _fluxdata;
        public Flux FluxData {
            get => _fluxdata;
            set => SetValue(out _fluxdata, value, FluxData);
        }


        public CommandBase Operation { get; set; }

        #endregion

        #region Methods

        private void InitRes() {
            Connected = LoginServices.Instence.IpgwConnected;
            FluxData = Formater.Instence.IpgwInfo;
            LoginServices.Instence.IpgwConnectedChanged += Instence_IpgwConnectedChanged;
            Formater.Instence.IpgwInfoChanged += Instence_IpgwInfoChanged;
        }

        private bool Instence_IpgwInfoChanged(object op, object np) {
            FluxData = np as Flux;
            return true;
        }

        private bool Instence_IpgwConnectedChanged(object op, object np) {
            Connected = (bool)np;
            return true;
        }

        private void InitCommand() {
            Operation = new CommandBase(obj =>
            {
                switch (obj.ToString())
                {
                    case "Connect":
                        LoginServices.Instence.LoginIpgw();
                        break;
                    case "Refresh":

                        break;
                    case "Disconnect":
                        LoginServices.Instence.LogoutIpgw();
                        break;
                }
            });
        }
        #endregion

        #region Constructors
        public FluxInfoViewModel() {
            InitRes();
            InitCommand();
        }
        #endregion
    }

}
