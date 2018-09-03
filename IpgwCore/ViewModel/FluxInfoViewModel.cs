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
using IpgwCore.Controls.Dialogs;
using System.Windows;

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

        private int _package;
        public int PackageN {
            get => _package;
            set => SetValue(out _package, value, PackageN, PackageChanged);
        }


        public CommandBase Operation { get; set; }

        #endregion

        #region Methods

        public void ForceUpdate() {
            FluxData = Formater.Instence.IpgwInfo;
        }

        private void InitRes() {
            Connected = LoginServices.Instence.IpgwConnected;
            _package = Properties.Settings.Default.Package;
            FluxData = Formater.Instence.IpgwInfo;
            LoginServices.Instence.IpgwConnectedChanged += Instence_IpgwConnectedChanged;
            Formater.Instence.IpgwInfoChanged += Instence_IpgwInfoChanged;
        }

        private bool PackageChanged(object op, object np) {
            Properties.Settings.Default.Package = (int)np;
            return true;
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
                        Formater.Instence.UpdateFlux();
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
