using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IpgwCore.Controls.ComplexControls
{
    /// <summary>
    /// GraphItem.xaml 的交互逻辑
    /// </summary>
    public partial class GraphItem : UserControl
    {

        #region Properties
        public bool IconType {
            get { return (bool)GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }
        public static readonly DependencyProperty IconTypeProperty =
            DependencyProperty.Register("IconType", typeof(bool), 
                typeof(GraphItem), new PropertyMetadata(true));


        public Brush IconBrush {
            get { return (Brush)GetValue(IconBrushProperty); }
            set { SetValue(IconBrushProperty, value); }
        }
        public static readonly DependencyProperty IconBrushProperty =
            DependencyProperty.Register("IconBrush", typeof(Brush), 
                typeof(GraphItem), new PropertyMetadata(null));

        public string Icon {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string),
                typeof(GraphItem), new PropertyMetadata(null));

        public string ToolTipText {
            get { return (string)GetValue(ToolTipTextProperty); }
            set { SetValue(ToolTipTextProperty, value); }
        }
        public static readonly DependencyProperty ToolTipTextProperty =
            DependencyProperty.Register("ToolTipText", typeof(string), 
                typeof(GraphItem), new PropertyMetadata(null));

        public string InfoText {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }
        public static readonly DependencyProperty InfoTextProperty =
            DependencyProperty.Register("InfoText", typeof(string), 
                typeof(GraphItem), new PropertyMetadata(null));

        public double InfoWidth {
            get { return (double)GetValue(InfoWidthProperty); }
            set { SetValue(InfoWidthProperty, value); }
        }
        public static readonly DependencyProperty InfoWidthProperty =
            DependencyProperty.Register("InfoWidth", typeof(double), 
                typeof(GraphItem), new PropertyMetadata(72.0));

        public FontFamily IconFontFamily {
            get { return (FontFamily)GetValue(IconFontFamilyProperty); }
            set { SetValue(IconFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty IconFontFamilyProperty =
            DependencyProperty.Register("IconFontFamily", typeof(FontFamily),
                typeof(GraphItem), new PropertyMetadata(null));


        public double IconFontSize {
            get { return (double)GetValue(IconFontSizeProperty); }
            set { SetValue(IconFontSizeProperty, value); }
        }
        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.Register("IconFontSize", typeof(double),
                typeof(GraphItem), new PropertyMetadata(12.0));


        #endregion

        private void GraphItem_Loaded(object sender, RoutedEventArgs e) {
            Binding bd = new Binding
            {
                Source = this,
                Path = new PropertyPath(ToolTipTextProperty),
                Mode = BindingMode.OneWay
            };

            BindingOperations.SetBinding(IconToolTip, TextBlock.TextProperty, bd);
        }

        public GraphItem()
        {
            InitializeComponent();
            Loaded += GraphItem_Loaded;
        }

        static GraphItem() {

        }

    }
}
