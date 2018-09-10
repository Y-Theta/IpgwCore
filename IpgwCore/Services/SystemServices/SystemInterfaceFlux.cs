using IpgwCore.Model.BasicModel;
using IpgwCore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IpgwCore.Services.SystemServices {
    public class SystemInterfaceFlux {
        #region Properties
        private static SystemInterfaceFlux _instence;
        private static readonly object _singleton_Lock = new object();

        public static SystemInterfaceFlux Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new SystemInterfaceFlux();
                return _instence;
            }
        }

        private List<NetworkInterface> _Ipv4InterList { get; set; }

        private double _receivedbytes { get; set; }

        private double _sendbytes { get; set; }

        public double Frequency {
            get => _updatetimer.Interval;
            set => _updatetimer.Interval = value;
        }

        private Timer _updatetimer;
        #endregion

        #region Methods
        private void InitializeNetworkInterface() {
            _Ipv4InterList = new List<NetworkInterface>();
            foreach (NetworkInterface t in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (t.OperationalStatus.ToString() == "Up")
                    if (t.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                        _Ipv4InterList.Add(t);
            }
        }

        private void InitRes() {
            _updatetimer = new Timer { Interval = 400 };
            _updatetimer.Elapsed += _updatetimer_Elapsed;
            _updatetimer.Enabled = true;
        }

        public void GetUploadSpeed() {
            double sentBytes = 0;
            double recivedBytes = 0;

            foreach (NetworkInterface nic in _Ipv4InterList)
            {
                IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics();
                sentBytes += interfaceStats.BytesSent;
                recivedBytes += interfaceStats.BytesReceived;
            }

            double sentSpeed = (sentBytes - _sendbytes) * (1000 / Frequency);
            double recivedSpeed = (recivedBytes - _receivedbytes) * (1000 / Frequency);

            if (_sendbytes == 0 && _receivedbytes == 0)
            {
                sentSpeed = 0;
                recivedSpeed = 0;
            }

            _sendbytes = sentBytes;
            _receivedbytes = recivedBytes;

           // Console.WriteLine(" Down:{0:###.##}   Up:{1:###.##}", FluxConv.FluxFormat(recivedSpeed, FluxType.B), FluxConv.FluxFormat(sentSpeed, FluxType.B));
        }
        #endregion

        #region Invokes
        private void _updatetimer_Elapsed(object sender, ElapsedEventArgs e) {
            GetUploadSpeed();
        }
        #endregion

        #region Constructors
        public SystemInterfaceFlux() {
            InitRes();
            InitializeNetworkInterface();
        }
        #endregion
    }

}
