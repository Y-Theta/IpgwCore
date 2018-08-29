using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Model.BasicModel {
    /// <summary>
    /// Xml文件中的路径标识
    /// </summary>
    internal struct XmlPath {
        /// <summary>
        /// 文件名
        /// </summary>
        public String Doc { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public String[] Path { get; set; }

        /// <summary>
        /// 主键标识
        /// </summary>
        public String Key { get; set; }
    }

}
