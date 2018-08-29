using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Model.BasicModel {
    /// <summary>
    /// 流量
    /// </summary>
    internal class Flux {
        #region Properties
        /// <summary>
        /// 流量信息
        /// </summary>
        public double FluxData { get; set; }

        /// <summary>
        /// 流量余额
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// 获取时间
        /// </summary>
        public DateTime InfoTime { get; set; }
        #endregion
    }

    /// <summary>
    /// 流量趋势
    /// </summary>
    internal class FluxTrendGroup {
        /// <summary>
        /// 流量组
        /// </summary>
        public Flux[] FluxInfos { get; set; }

        /// <summary>
        /// 绘制时坐标值
        /// </summary>
        public double[] PointData { get; set; }

        /// <summary>
        /// 坐标值
        /// </summary>
        public double[] VTicks { get; set; }

    }
}
