using IpgwCore.ViewModel;
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

namespace IpgwCore.View.Pages {
    /// <summary>
    /// InfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class InfoPage : Page {
        public InfoPage() {
            InitializeComponent();
            Loaded += InfoPage_Loaded;
        }

        private void InfoPage_Loaded(object sender, RoutedEventArgs e) {
            (App.Current.Resources["MainPageVM"] as MainPageViewModel).Msg = "用户信息";

        }
    }
}
