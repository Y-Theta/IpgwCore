using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IpgwCore.Services.MessageServices {
    /// <summary>
    /// 用于全局消息的发送
    /// </summary>
    internal interface IMessageServices {

        IMessagePoster MessageHolder { get; set; }

        /// <summary>
        /// 显示控件内容
        /// </summary>
        /// <param name="obj">要显示的控件</param>
        /// <returns>是否已经显示</returns>
        bool ShowContent(Style obj);

        /// <summary>
        /// 显示控件内容文本
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        bool ShowContent(string s);

        /// <summary>
        /// 在状态栏显示提示
        /// </summary>
        /// <param name="str">要显示的提示</param>
        void ShowString(string str);

        /// <summary>
        /// 在指定控件上显示消息
        /// </summary>
        /// <param name="obj">指定控件</param>
        /// <param name="con">消息内容</param>
        /// <returns>是否成功</returns>
        bool ShowContentAt(UIElement obj, Style con);
    }
}
