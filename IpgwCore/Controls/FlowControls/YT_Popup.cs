using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using IpgwCore.MVVMBase;
using IpgwCore.Services.HttpServices;

namespace IpgwCore.Controls.FlowControls {
    public class YT_Popup : Popup {

        #region Properties

        private bool _locationSet { get; set; }

        private bool _fisrtrun { get; set; }

        private Timer _autohide;

        #region UIProp
        public Brush Foreground {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush),
                typeof(YT_Popup), new PropertyMetadata(null));

        public Brush Background {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush),
                typeof(YT_Popup), new PropertyMetadata(null));

        public Brush BorderBrush {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush),
                typeof(YT_Popup), new PropertyMetadata(null));

        public double BorderThickness {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double),
                typeof(YT_Popup), new PropertyMetadata(0.0));
        #endregion

        /// <summary>
        /// 关联的窗口
        /// </summary>
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

        /// <summary>
        /// 自定义位置时的偏移矩形
        /// </summary>
        [TypeConverter(typeof(PointConverter))]
        public Point RelativeRect {
            get { return (Point)GetValue(RelativeRectProperty); }
            set { SetValue(RelativeRectProperty, value); }
        }
        public static readonly DependencyProperty RelativeRectProperty =
            DependencyProperty.Register("RelativeRect", typeof(Point),
                typeof(YT_Popup), new PropertyMetadata(new Point(0, 0)));

        /// <summary>
        /// 屏幕边距
        /// </summary>
        public double PlacementMargin {
            get { return (double)GetValue(PlacementMarginProperty); }
            set { SetValue(PlacementMarginProperty, value); }
        }
        public static readonly DependencyProperty PlacementMarginProperty =
            DependencyProperty.Register("PlacementMargin", typeof(double),
                typeof(YT_Popup), new PropertyMetadata(0.0));

        /// <summary>
        /// 相对于屏幕右下角的水平偏移
        /// </summary>
        public double AbsOffsetX {
            get { return (double)GetValue(AbsOffsetXProperty); }
            set { SetValue(AbsOffsetXProperty, value); }
        }
        public static readonly DependencyProperty AbsOffsetXProperty =
            DependencyProperty.Register("AbsOffsetX", typeof(double),
                typeof(YT_Popup), new PropertyMetadata(0.0));

        /// <summary>
        /// 相对于屏幕右下角的竖直偏移
        /// </summary>
        public double AbsOffsetY {
            get { return (double)GetValue(AbsOffsetYProperty); }
            set { SetValue(AbsOffsetYProperty, value); }
        }
        public static readonly DependencyProperty AbsOffsetYProperty =
            DependencyProperty.Register("AbsOffsetY", typeof(double),
                typeof(YT_Popup), new PropertyMetadata(0.0));

        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextContent {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }
        public static readonly DependencyProperty TextContentProperty =
            DependencyProperty.Register("TextContent", typeof(string),
                typeof(YT_Popup), new PropertyMetadata(""));

        /// <summary>
        /// 自动隐藏
        /// </summary>
        public bool AutoHide {
            get { return (bool)GetValue(AutoHideProperty); }
            set { SetValue(AutoHideProperty, value); }
        }
        public static readonly DependencyProperty AutoHideProperty =
            DependencyProperty.Register("AutoHide", typeof(bool),
                typeof(YT_Popup), new PropertyMetadata(false));

        /// <summary>
        /// 依赖对象改变事件
        /// </summary>
        public event EventHandler PlacementTargetChanged;
        public new UIElement PlacementTarget {
            get => base.PlacementTarget;
            set {
                base.PlacementTarget = value;
                PlacementTargetChanged.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public new bool IsOpen {
            get => base.IsOpen;
            set {
                if (value)
                    OnOpen();
                else
                    OnClosed();
            }
        }

        public CommandBase CloseCommand { get; set; }
        #endregion


        #region Methods
        protected virtual void OnClosed() {
            base.IsOpen = false;
        }

        protected virtual void OnOpen() {
            if (!_locationSet)
                OnPlacementTargetChanged(null, null);
            if (AutoHide)
                _autohide.Enabled = true;            
            base.IsOpen = true;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            if (AutoHide)
                _autohide.Enabled = false;
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            if (AutoHide)
                _autohide.Enabled = true;
            base.OnMouseLeave(e);
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
        }

        protected override void OnStyleChanged(Style oldStyle, Style newStyle) {
            base.OnStyleChanged(oldStyle, newStyle);
            if (AttachedWindow is null)
                AttachedWindow = App.Current.MainWindow;
        }

        private void OnPlacementTargetChanged(object sender, EventArgs e) {
            _locationSet = true;
            if (Placement.Equals(PlacementMode.AbsolutePoint) || PlacementTarget is null) 
            {
                HorizontalOffset = 0;
                VerticalOffset = 0;
                PlacementRectangle = new Rect(SystemParameters.WorkArea.Width - this.Child.DesiredSize.Width - PlacementMargin,
                SystemParameters.WorkArea.Height - this.Child.DesiredSize.Height - PlacementMargin, 0, 0);
            }
            else
            {
                PlacementRectangle = new Rect(PlacementTarget.RenderSize.Width - AbsOffsetX, PlacementTarget.RenderSize.Height - AbsOffsetY, 0, 0);
                if (PlacementTarget is Window)
                    ((Window)PlacementTarget).LocationChanged += YT_Popup_LocationChanged;
            }
        }

        private void YT_Popup_LocationChanged(object sender, EventArgs e) {
            if (IsOpen)
            {
                var mi = typeof(Popup).GetMethod("UpdatePosition",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                mi.Invoke(this, null);
            }
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

        private void InitRes() {
            PlacementTargetChanged += OnPlacementTargetChanged;
            CustomPopupPlacementCallback = new CustomPopupPlacementCallback(Location);
            _locationSet = false;
            _fisrtrun = true;

            _autohide = new Timer { Interval = 4500 };
            _autohide.Elapsed += _autohide_Elapsed;
        }

        private void InitCommands() {
            CloseCommand = new CommandBase(obj =>
            {
                IsOpen = false;
            });
        }
        #endregion

        #region CallBacks
        private void _autohide_Elapsed(object sender, ElapsedEventArgs e) {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                IsOpen = false;
                _autohide.Enabled = false;
            }));
        }

        #endregion

        #region Contructor
        public YT_Popup() {
            InitRes();
            InitCommands();
        }
        #endregion
    }
}