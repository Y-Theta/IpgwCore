using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IpgwCore.AWindow {
    public class YT_TitleBar : ContentControl {
        #region AttachedWindow
        private Window attachedWindow;
        public Window AttachedWindow {
            get => attachedWindow;
            set { attachedWindow = value; }
        }
        #endregion

        #region Methods Overrides
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            AttachedWindow.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        public override void EndInit() {
            DependencyObject RootElement = this;
            while (!(RootElement is Window)) { RootElement = VisualTreeHelper.GetParent(RootElement); }
            AttachedWindow = RootElement as Window;

            base.EndInit();
        }
        #endregion

        #region EventActions

        #endregion

        #region Constructors
        public YT_TitleBar() {

        }

        static YT_TitleBar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_TitleBar), new FrameworkPropertyMetadata(typeof(YT_TitleBar)));
        }
        #endregion
    }
}
