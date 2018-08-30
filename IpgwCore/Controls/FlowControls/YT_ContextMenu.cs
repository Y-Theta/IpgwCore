using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IpgwCore.Controls.FlowControls {
    public class YT_ContextMenu : ContextMenu {

        static YT_ContextMenu() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_ContextMenu), new FrameworkPropertyMetadata(typeof(YT_ContextMenu)));
        }
    }

    public class YT_MenuItem : MenuItem {

        #region IconOnly
        public Visibility ContentTextVisiblity {
            get { return (Visibility)GetValue(ContentTextVisiblityProperty); }
            set { SetValue(ContentTextVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ContentTextVisiblityProperty =
            DependencyProperty.Register("ContentTextVisiblity", typeof(Visibility), typeof(YT_MenuItem),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region TextOnly
        public Visibility IconVisiblity {
            get { return (Visibility)GetValue(IconVisiblityProperty); }
            set { SetValue(IconVisiblityProperty, value); }
        }
        public static readonly DependencyProperty IconVisiblityProperty =
            DependencyProperty.Register("IconVisiblity", typeof(Visibility),
                typeof(YT_MenuItem), new PropertyMetadata(Visibility.Visible));
        #endregion

        #region HeadIcon
        public string HeadIcon {
            get { return (string)GetValue(HeadIconProperty); }
            set { SetValue(HeadIconProperty, value); }
        }
        public static readonly DependencyProperty HeadIconProperty =
            DependencyProperty.Register("HeadIcon", typeof(string),
                typeof(YT_MenuItem), new PropertyMetadata(""));
        #endregion

        #region ButtonColor
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(YT_MenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(YT_MenuItem),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(YT_MenuItem),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(YT_MenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(YT_MenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(YT_MenuItem),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        #region ContentText
        public string ContentText {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string), typeof(YT_MenuItem),
                new PropertyMetadata(""));
        #endregion

        #region ContentTextFontWeight
        public double ContentTextFontSize {
            get { return (double)GetValue(ContentTextFontSizeProperty); }
            set { SetValue(ContentTextFontSizeProperty, value); }
        }
        public static readonly DependencyProperty ContentTextFontSizeProperty =
            DependencyProperty.Register("ContentTextFontSize", typeof(double), typeof(YT_MenuItem),
                new PropertyMetadata(0.0));
        #endregion

        static YT_MenuItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_MenuItem), new FrameworkPropertyMetadata(typeof(YT_MenuItem)));
        }
    }

}
