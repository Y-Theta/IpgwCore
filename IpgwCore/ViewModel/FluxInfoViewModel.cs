using IpgwCore.Model.BasicModel;
using IpgwCore.MVVMBase;
using IpgwCore.Services.FormatServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpgwCore.Services.MessageServices;

namespace IpgwCore.ViewModel {
    internal class FluxInfoViewModel :ViewModelBase{
        #region Properties
        //private double _percent;
        ///// <summary>
        ///// 流量剩余百分比
        ///// </summary>
        //public double Percent {
        //    get => _percent;
        //    set => SetValue(out _percent, value, Percent);
        //}

        public bool Connected {
            get => Formater.Instence.IpgwConnected;
        }

        public Flux FluxData {
            get => Formater.Instence.IpgwInfo;
        }

        public CommandBase Operation { get; set; }

        #endregion

        #region Methods
        private void InitCommand() {
            Operation = new CommandBase(obj =>
            {
                switch (obj.ToString())
                {
                    case "Connect":
                        PopupMessageServices.Instence.ShowContent("sdsdsd");
                        break;
                    case "Refresh":
                        App.Current.MainWindow.Hide();
                        break;
                    case "Disconnect":
                        App.Current.MainWindow.Show();
                        break;
                }
            });
        }
        #endregion

        #region Constructors
        public FluxInfoViewModel() {
            InitCommand();
        }
        #endregion
    }

}
