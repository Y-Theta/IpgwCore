using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IpgwCore.Controls.AreaWindow {
    public class YT_Window : Window {

        #region ExtendToTitleBar
        public bool ExtendToTitleBar {
            get { return (bool)GetValue(ExtendToTitleBarProperty); }
            set { SetValue(ExtendToTitleBarProperty, value); }
        }
        public static readonly DependencyProperty ExtendToTitleBarProperty =
            DependencyProperty.Register("ExtendToTitleBar", typeof(bool),
                typeof(YT_Window), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region TitleBar
        public object TitleArea {
            get { return (object)GetValue(TitleAreaProperty); }
            set { SetValue(TitleAreaProperty, value); }
        }
        public static readonly DependencyProperty TitleAreaProperty =
            DependencyProperty.Register("TitleArea", typeof(object),
                typeof(YT_Window), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region AreaIcon
        public YT_AreaIcon AreaIcon {
            get { return (YT_AreaIcon)GetValue(AreaIconProperty); }
            set { SetValue(AreaIconProperty, value); }
        }
        public static readonly DependencyProperty AreaIconProperty =
            DependencyProperty.Register("AreaIcon", typeof(YT_AreaIcon), 
                typeof(YT_Window), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region override
        protected override void OnInitialized(EventArgs e) {
            SourceInitialized += new EventHandler(WSInitialized);
            base.OnInitialized(e);
        }

        private void WSInitialized(object sender, EventArgs e) {
            (PresentationSource.FromVisual((Visual)sender) as HwndSource).AddHook(new HwndSourceHook(WndProc));
        }

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            return IntPtr.Zero;
        }
        #endregion

        #region 控件初始化

        #endregion

        #region 部件事件

        #endregion

        public YT_Window() {

        }

        static YT_Window() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Window), new FrameworkPropertyMetadata(typeof(YT_Window)));
        }
    }
}
