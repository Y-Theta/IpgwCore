using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IpgwCore.Services.MessageServices {
    /// <summary>
    /// 信息发送方所要具备的参数
    /// </summary>
    internal interface IMessagePoster {
        /// <summary>
        /// 要发送的信息
        /// </summary>
        object _message { get; set; }
    }

}
