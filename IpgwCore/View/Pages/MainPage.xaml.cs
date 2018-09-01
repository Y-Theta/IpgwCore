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
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page {
        private MainPageViewModel _mpvm;

        public MainPage() {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e) {
            _mpvm.Mainpage = false;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e) {
            _mpvm = (MainPageViewModel)DataContext;
            _mpvm.CommandOperation += _mpvm_CommandOperation;
            _mpvm.Mainpage = true;
        }

        private void _mpvm_CommandOperation(object sender, MVVMBase.CommandArgs args) {
            
        }
    }
}
