using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows;
using App = IpgwCore.App;
using Drawing = System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IpgwCore.Controls.AreaWindow {
    internal interface IDrawicon {
        Icon Drawicon();
    }

    public class YT_AreaIcon: Control,IDrawicon
    {
        //NormalProperties
        private System.Windows.Forms.NotifyIcon FlowIcon;

        #region AttachedWindow
        public object AttachedWindow {
            get { return (object)GetValue(AttachedWindowProperty); }
            set { SetValue(AttachedWindowProperty, value); }
        }
        public static readonly DependencyProperty AttachedWindowProperty =
            DependencyProperty.Register("AttachedWindow", typeof(object), typeof(YT_AreaIcon),
                new PropertyMetadata(null, new PropertyChangedCallback(OnAttachedWindowChanged)));
        private static void OnAttachedWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            YT_AreaIcon dai = (YT_AreaIcon)d;
        }
        #endregion

        #region AreaVisibility
        public bool AreaVisibility {
            get { return (bool)GetValue(AreaVisibilityProperty); }
            set { SetValue(AreaVisibilityProperty, value); }
        }
        public static readonly DependencyProperty AreaVisibilityProperty =
            DependencyProperty.Register("AreaVisibility", typeof(bool), typeof(YT_AreaIcon),
                new PropertyMetadata(false, new PropertyChangedCallback(OnAreaVisibilityChanged)));
        private static void OnAreaVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            YT_AreaIcon dai = (YT_AreaIcon)d;
            dai.FlowIcon.Visible = (bool)e.NewValue;
        }
        #endregion

        #region Dcontextmenu

        #endregion

        #region 初始化
        private void InitNotifyIcon()
        {
            FlowIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = AreaVisibility,
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip()
            };
            FlowIcon.MouseClick += FlowIcon_MouseClick;
            FlowIcon.MouseMove += FlowIcon_MouseMove;
            FlowIcon.MouseDoubleClick += FlowIcon_MouseDoubleClick;
        }

        #endregion

        #region 事件方法
        private void FlowIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Right:
                    break;
                case System.Windows.Forms.MouseButtons.Left:
                    break;
            }
        }

        private void FlowIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {


        }

        private void FlowIcon_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }
        #endregion

        #region GraphUpdata -- 托盘画图
        public Icon Drawicon() {
            throw new NotImplementedException();
        }
        #endregion

        #region 公共方法

        #endregion

        #region 设置更新


        #endregion

        public YT_AreaIcon()
        {
            App.Current.MainWindow.Closing += MainWindow_Closing;
            InitNotifyIcon();
        }

        static YT_AreaIcon() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_AreaIcon), new FrameworkPropertyMetadata(typeof(YT_AreaIcon)));
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
