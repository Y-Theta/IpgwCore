using IpgwCore.Controls.ChartControl;
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
    /// YT_Chart.xaml 的交互逻辑
    /// </summary>
    public partial class YT_Chart : UserControl
    {
        public YT_Chart()
        {
            InitializeComponent();
            Loaded += YT_Chart_Loaded;
        }

        private void YT_Chart_Loaded(object sender, RoutedEventArgs e) {
            
        }
    }
}
