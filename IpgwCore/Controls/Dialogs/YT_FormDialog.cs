using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IpgwCore.Controls.Dialogs {
    /// <summary>
    /// 内容对话框
    /// </summary>
    public sealed class YT_FormDialog :YT_DialogBase {

        #region Properties
        public string Question {
            get { return (string)GetValue(QuestionProperty); }
            set { SetValue(QuestionProperty, value); }
        }
        public static readonly DependencyProperty QuestionProperty =
            DependencyProperty.Register("Question", typeof(string),
                typeof(YT_FormDialog), new PropertyMetadata(""));
        #endregion

        #region Actions

        #endregion

        #region Overrides

        #endregion

        #region Contructor
        public YT_FormDialog() {
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        static YT_FormDialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_FormDialog), new FrameworkPropertyMetadata(typeof(YT_FormDialog)));
        }
        #endregion
    }
}
