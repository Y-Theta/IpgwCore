using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OS_CD {
    /// <summary>
    /// 窗口关闭对话框
    /// 一般用于提醒保存操作或进行一些选择
    /// </summary>
    public class YT_GeneralDialog : YT_DialogBase {

        #region Properties
        public string ContentText {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string),
                typeof(YT_GeneralDialog), new PropertyMetadata(""));
        #endregion

        protected override void OnClosing(CancelEventArgs e) {
            this.Content = null;
            base.OnClosing(e);
        }

        public YT_GeneralDialog() {
            this.WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        static YT_GeneralDialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_GeneralDialog), new FrameworkPropertyMetadata(typeof(YT_GeneralDialog)));
        }
    }
}
