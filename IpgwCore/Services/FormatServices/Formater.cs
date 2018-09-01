using System;
using IpgwCore.Model.BasicModel;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpgwCore.Services.HttpServices;
using IpgwCore.MVVMBase;

namespace IpgwCore.Services.FormatServices {
    /// <summary>
    /// 格式化服务
    /// </summary>
    internal class Formater {
        #region Properties

        private static Formater _instence;
        private static readonly object _singleton_Lock = new object();
        public static Formater Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new Formater();
                return _instence;
            }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        private DateTime _daten { get; set; }

        /// <summary>
        /// 课程网址
        /// </summary>
        private HtmlDocument _courseInfo { get; set; }

        private Flux _ipgwInfo = null;
        /// <summary>
        /// 流量信息
        /// </summary>
        public event PropertyChangedCallBack IpgwInfoChanged;
        public Flux IpgwInfo {
            get => GetIpgwInfo();
            set {
                IpgwInfoChanged?.Invoke(_ipgwInfo, value);
                _ipgwInfo = value;
            }
        }

        private CourseSet _courseSet { get; set; }
        /// <summary>
        /// 课程表
        /// </summary>
        public CourseSet CourseSet {
            get => (!_daten.Equals(Properties.Settings.Default.WeekNowSet)) ? GetCourseSet() :
                _courseSet is null ? GetCourseSet() :
                _courseSet;
            set => _courseSet = value;
        }

        #endregion

        #region Methods

        #region Flux
        /// <summary>
        /// 获取流量数据
        /// </summary>
        private Flux GetIpgwInfo() {
            if (_ipgwInfo != null)
                return _ipgwInfo;
            else
            {
                var flux = XmlDocService.Instence.GetNode<Flux>(null);
                if (flux is null)
                    flux = RefreshInfo();
                if (flux != null)
                    return flux;
                else
                    return null;
            }
        }

        private Flux RefreshInfo() {
            return GetIpgwDataInf(LoginServices.Instence.GetFluxInfo());
        }

        private Flux GetIpgwDataInf(string data)  //Ipgw网关信息格式化获取
{
            Flux info = new Flux();
            try
            {
                string[] t = data.Split(new char[] { ',' });
                info.FluxData = FluxFormater(t[0]);
                info.Balance = Convert.ToDouble(t[2]);
                info.InfoTime = DateTime.Now;
            }
            catch (IndexOutOfRangeException)
            {
                // MessageService.Instence.ShowError(null, "用户名或密码错误");
                return null;
            }
            catch (NullReferenceException)
            {
                //  MessageService.Instence.ShowError(null, "网络未连接");
                return null;
            }

            XmlDocService.Instence.SetNode(info, null);
            return info;
        }

        private double FluxFormater(string flux)//流量信息格式化 in(Byte)
{
            Int64 a = 0;
            try { a = Convert.ToInt64(flux); }
            catch (FormatException)
            {//
            }
            return a / 1000000.0;
        }

        public void UpdateFlux() {
            if (RefreshInfo() is null)
                IpgwInfo = GetIpgwInfo();
            else
                IpgwInfo = RefreshInfo();
        }
        #endregion

        private CourseSet GetCourseSet() {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructors
        #endregion
    }

}
