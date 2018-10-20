using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEUH_Contract {

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
        Dictionary<CaseName, int> Usage { get; set; }
        /// <summary>
        /// 控件的某些行为需要核心程序作出响应回调
        /// </summary>
        event CrossAppDomainDelegate ActionCallBack;

        /// <summary>
        /// 触发ActionCallBack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void InvokeCallback();
        /// <summary>
        /// 获取插件信息的字符描述
        /// </summary>
        string InfoStringFormat();
        /// <summary>
        /// 运行插件
        /// </summary>
        void Run(CaseName c);
        /// <summary>
        /// 运行插件并带回必要值
        /// </summary>
        void Run(CaseName c, out object t);
    }

    /// <summary>
    /// 插件基类
    /// </summary>
    public class CoreContractBase : MarshalByRefObject, INEUHCoreContract {
        public event CrossAppDomainDelegate ActionCallBack;

        public string Name { get; set; }

        public string Author { get; set; }

        public string Edition { get; set; }

        public string Description { get; set; }

        public Dictionary<CaseName, int> Usage { get; set; }

        public virtual string InfoStringFormat() {
            return String.Format($"Name        :  {Name}\n" +
                                 $"Author      :  {Author}\n" +
                                 $"Edition     :  {Edition}\n" +
                                 $"Description :  {Description}\n");
        }

        public virtual void Run(CaseName c) {

        }

        public virtual void Run(CaseName c, out object t) {
            t = null;
        }

        public void InvokeCallback() {
            ActionCallBack?.Invoke();
        }
    }
}
