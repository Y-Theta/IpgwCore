using System;
using IpgwCore.Model.BasicModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Services.FormatServices {
    /// <summary>
    /// Xml操作
    /// </summary>
    interface IXmlOperation {
        /// <summary>
        /// 通过路径获取某个节点的元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="path">节点路径</param>
        /// <returns></returns>
        object GetNode<T>(XmlPath path);

        /// <summary>
        /// 通过路径设置某个节点的元素
        /// </summary>
        bool SetNode<T>(T Item, XmlPath path);

        /// <summary>
        /// 通过路径删除某个节点的元素
        /// </summary>
        bool DeleteNode<T>(T Item, XmlPath path);

        /// <summary>
        /// 通过路径更新某个节点的元素
        /// </summary>
        bool UpdateNode<T>(T Item, XmlPath path);

    }

}
