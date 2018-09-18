using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEUH_Contract {

    public enum UseCase {

    }

    /// <summary>
    /// NEUH核心程序的插件接口
    /// </summary>
    public interface INEUHCoreContract {
        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 插件作者
        /// </summary>
        string Author { get; set; }
        /// <summary>
        /// 插件版本
        /// </summary>
        string Edition { get; set; }
        /// <summary>
        /// 插件描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 插件的使用位置，决定插件在载入主程序后何时使用
        /// </summary>
        UseCase[] Usage { get; set; }


        /// <summary>
        /// 获取插件信息的字符描述
        /// </summary>
        string InfoStringFormat();
        /// <summary>
        /// 运行插件
        /// </summary>
        void Run(UseCase c);
        /// <summary>
        /// 运行插件并带回必要值
        /// </summary>
        void Run(UseCase c, out object t);
    }
}
