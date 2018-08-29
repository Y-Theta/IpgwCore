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

}
