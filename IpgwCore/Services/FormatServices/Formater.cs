using System;
using IpgwCore.Model.BasicModel;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Services.FormatServices {
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
            get => ipgwInfo is null ? GetIpgwInfo() :
                     ipgwInfo;
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
        private Flux GetIpgwInfo() {
           
        }

        private CourseSet GetCourseSet() {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructors
        #endregion
    }

}
