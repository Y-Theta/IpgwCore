using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace IpgwCore.Controls.FlowControls {
    public class YT_Popup : Popup {

        #region Properties
        public Window AttachedWindow {
            get { return (Window)GetValue(AttachedWindowProperty); }
            set {
                SetValue(AttachedWindowProperty, value);
                AttachedWindow.LocationChanged += YT_Popup_LocationChanged;
            }
        }
        public static readonly DependencyProperty AttachedWindowProperty =
            DependencyProperty.Register("AttachedWindow", typeof(Window),
                typeof(YT_Popup), new PropertyMetadata(null));


        [TypeConverter(typeof(PointConverter))]
        public Point RelativeRect {
            get { return (Point)GetValue(RelativeRectProperty); }
            set { SetValue(RelativeRectProperty, value); }
        }
        public static readonly DependencyProperty RelativeRectProperty =
            DependencyProperty.Register("RelativeRect", typeof(Point),
                typeof(YT_Popup), new PropertyMetadata(new Point(0, 0)));

        #endregion


        #region Methods
        private void YT_Popup_LocationChanged(object sender, EventArgs e) {
            if (IsOpen)
            {
                var mi = typeof(Popup).GetMethod("UpdatePosition",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                mi.Invoke(this, null);
            }
        }

        public override void EndInit() {
            base.EndInit();
            if (AttachedWindow is null)
                AttachedWindow = App.Current.MainWindow;

        }

        private CustomPopupPlacement[] Location(Size popupSize, Size targetSize, Point offset) {

            CustomPopupPlacement placement1 =
                    new CustomPopupPlacement(new Point(RelativeRect.X, targetSize.Height + RelativeRect.Y), PopupPrimaryAxis.Vertical);

            CustomPopupPlacement placement2 =
                new CustomPopupPlacement(new Point(0, 0), PopupPrimaryAxis.Horizontal);

            CustomPopupPlacement[] ttplaces =
                    new CustomPopupPlacement[] { placement1, placement2 };
            return ttplaces;
        }
        #endregion

        #region Contructor
        public YT_Popup() {
            CustomPopupPlacementCallback = new CustomPopupPlacementCallback(Location);
        }

        #endregion
    }
}
