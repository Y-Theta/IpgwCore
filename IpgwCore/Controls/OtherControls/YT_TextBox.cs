using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IpgwCore.Controls {
    internal class YT_TextBox : TextBox {
        #region Properties
        private Brush _ft;
        private Brush _bt;
        /// <summary>
        /// 焦点前景色
        /// </summary>
        public Brush ForegroundF {
            get { return (Brush)GetValue(ForegroundFProperty); }
            set { SetValue(ForegroundFProperty, value); }
        }

        public static readonly DependencyProperty ForegroundFProperty =
            DependencyProperty.Register("ForegroundF", typeof(Brush),
                typeof(YT_TextBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        /// <summary>
        /// 焦点背景色
        /// </summary>
        public Brush BackgroundF {
            get { return (Brush)GetValue(BackgroundFProperty); }
            set { SetValue(BackgroundFProperty, value); }
        }
        public static readonly DependencyProperty BackgroundFProperty =
            DependencyProperty.Register("BackgroundF", typeof(Brush),
                typeof(YT_TextBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// 内部内边距
        /// </summary>
        public Thickness ContentPadding {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }
        public static readonly DependencyProperty ContentPaddingProperty =
            DependencyProperty.Register("ContentPadding", typeof(Thickness), 
                typeof(YT_TextBox), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 内部外边距
        /// </summary>
        public Thickness ContentMargin {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness),
                typeof(YT_TextBox), new PropertyMetadata(new Thickness(0)));
        #endregion

        #region Methods

        protected override void OnGotFocus(RoutedEventArgs e) {
            _bt = Background;
            Background = BackgroundF;
            _ft = Foreground;
            Foreground = ForegroundF;
            base.OnGotFocus(e);
            
        }

        protected override void OnLostFocus(RoutedEventArgs e) {
            Background = _bt;
            Foreground = _ft;
            base.OnLostFocus(e);
        }
        #endregion

        #region Constructors
        static YT_TextBox() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_TextBox), new FrameworkPropertyMetadata(typeof(YT_TextBox)));
        }
        #endregion
    }

}
