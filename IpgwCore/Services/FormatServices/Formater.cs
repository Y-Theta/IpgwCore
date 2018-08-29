using System;
using IpgwCore.Model.BasicModel;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpgwCore.Services.HttpServices;

namespace IpgwCore.Services.FormatServices {
    /// <summary>
    /// 格式化服务
    /// </summary>
    internal class Formater {
        #region Properties
        /// <summary>
        /// 网络是否连接
        /// </summary>
        private bool _ipgeConnected { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        private DateTime _daten { get; set; }

        /// <summary>
        /// 课程网址
        /// </summary>
        private HtmlDocument _courseInfo { get; set; }

        private Flux ipgwInfo = null;
        /// <summary>
        /// 流量信息
        /// </summary>
        public Flux IpgwInfo {
            get => GetIpgwInfo();
            set => ipgwInfo = value;
        }

        private CourseSet courseSet { get; set; }
        /// <summary>
        /// 课程表
        /// </summary>
        public CourseSet CourseSet {
            get => (!_daten.Equals(Properties.Settings.Default.WeekNowSet)) ? GetCourseSet() :
                courseSet is null ? GetCourseSet() :
                courseSet;
            set => courseSet = value;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 获取流量数据
        /// </summary>
        /// <returns></returns>
        private Flux GetIpgwInfo() {
            if (ipgwInfo != null)
                return ipgwInfo;
            else {
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
           return  GetIpgwDataInf(LoginServices.Instence.GetString("NEUIpgw"));
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
                _ipgeConnected = false;
                return null;
            }
            catch (NullReferenceException)
            {
                //  MessageService.Instence.ShowError(null, "网络未连接");
                _ipgeConnected = false;
                return null;
            }

            XmlDocService.Instence.SetNode(info, null);
            return info;
        }

        private double FluxFormater(string flux)//流量信息格式化 in(Byte)
{
            Int64 a = 0;
            try
            {
                a = Convert.ToInt64(flux);
            }
            catch (FormatException)
            {
                
            }
            return a / 1000000.0;
        }

        private CourseSet GetCourseSet() {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructors
        #endregion
    }

}
