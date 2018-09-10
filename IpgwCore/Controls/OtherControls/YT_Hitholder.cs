using IpgwCore.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace IpgwCore.Controls {

    [ContentProperty("Content")]
    public class YT_Hitholder : Control {
        #region Properties
        private Brush _obg;
        private Brush _opg;
        /// <summary>
        /// 鼠标悬浮颜色
        /// </summary>
        public Brush PointBrush {
            get { return (Brush)GetValue(PointBrushProperty); }
            set { SetValue(PointBrushProperty, value); }
        }
        public static readonly DependencyProperty PointBrushProperty =
            DependencyProperty.Register("PointBrush", typeof(Brush),
                typeof(YT_Hitholder), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// 鼠标点击颜色
        /// </summary>
        public Brush PressBrush {
            get { return (Brush)GetValue(PressBrushProperty); }
            set { SetValue(PressBrushProperty, value); }
        }
        public static readonly DependencyProperty PressBrushProperty =
            DependencyProperty.Register("PressBrush", typeof(Brush),
                typeof(YT_Hitholder), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// 提示文本
        /// </summary>
        public String ToolTipText {
            get { return (String)GetValue(ToolTipTextProperty); }
            set { SetValue(ToolTipTextProperty, value); }
        }
        public static readonly DependencyProperty ToolTipTextProperty =
            DependencyProperty.Register("ToolTipText", typeof(String), 
                typeof(YT_Hitholder), new PropertyMetadata(null));

        /// <summary>
        /// 内容
        /// </summary>
        public UIElement Content {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(UIElement),
                typeof(YT_Hitholder), new PropertyMetadata(null));

        /// <summary>
        /// 鼠标悬浮事件
        /// </summary>
        public CommandAction OnPointEnter {
            get { return (CommandAction)GetValue(OnPointEnterrProperty); }
            set { SetValue(OnPointEnterrProperty, value); }
        }
        public static readonly DependencyProperty OnPointEnterrProperty =
            DependencyProperty.Register("OnPointEnter", typeof(CommandAction),
                typeof(YT_Hitholder), new PropertyMetadata(null));


        public CommandAction OnPointExit {
            get { return (CommandAction)GetValue(OnPointExitProperty); }
            set { SetValue(OnPointExitProperty, value); }
        }
        public static readonly DependencyProperty OnPointExitProperty =
            DependencyProperty.Register("OnPointExit", typeof(CommandAction),
                typeof(YT_Hitholder), new PropertyMetadata(null));


        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand),
                typeof(YT_Hitholder), new PropertyMetadata(null));

        public object CommandPara {
            get { return (object)GetValue(CommandParaProperty); }
            set { SetValue(CommandParaProperty, value); }
        }
        public static readonly DependencyProperty CommandParaProperty =
            DependencyProperty.Register("CommandPara", typeof(object),
                typeof(YT_Hitholder), new PropertyMetadata(null));
        #endregion

        #region Override
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            Command?.Execute(CommandPara);
            _opg = Background;
            Background = PressBrush;
            e.Handled = true;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonUp(e);
            Background = _opg;
        }

        protected override void OnMouseEnter(MouseEventArgs e) {
            base.OnMouseEnter(e);
            _obg = Background;
            Background = PointBrush;
            OnPointEnter?.Invoke(DataContext);
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            OnPointExit?.Invoke(DataContext);
            Background = _obg;
        }


        private void YT_Hitholder_Loaded(object sender, RoutedEventArgs e) {
            
        }
        #endregion

        #region Constructor
        public YT_Hitholder() {
            Loaded += YT_Hitholder_Loaded;
        }
        static YT_Hitholder() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_Hitholder), new FrameworkPropertyMetadata(typeof(YT_Hitholder)));
        }
        #endregion
    }
}
