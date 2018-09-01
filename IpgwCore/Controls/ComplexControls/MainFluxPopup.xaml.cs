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

namespace IpgwCore.Controls.ComplexControls {
    /// <summary>
    /// MainFluxPopup.xaml 的交互逻辑
    /// </summary>
    public partial class MainFluxPopup : UserControl {
        public MainFluxPopup() {
            InitializeComponent();
        }

        private void GraphPanel_MouseEnter(object sender, MouseEventArgs e) {
            PercentNum.Foreground = Percent.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            PercentEffect.Opacity = 1;
            PercentNumEffect.Opacity = 1;
        }

        private void GraphPanel_MouseLeave(object sender, MouseEventArgs e) {
            PercentNum.Foreground = Percent.Stroke = App.Current.Resources["FluxPopup_Fg"] as SolidColorBrush;
            PercentEffect.Opacity = 0;
            PercentNumEffect.Opacity = 0;
        }

        private void GraphPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            PercentNum.Foreground = Percent.Stroke = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255));
            PercentEffect.BlurRadius = 0;
        }

        private void GraphPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            PercentNum.Foreground = Percent.Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            PercentEffect.BlurRadius = 20;
        }
    }
}
