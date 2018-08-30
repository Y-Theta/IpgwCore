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
using IpgwCore.MVVMBase;
using IpgwCore.ViewModel;
using IpgwCore.Controls.AreaWindow;
using IpgwCore.Services.MessageServices;
using IpgwCore.Controls.Dialogs;

namespace IpgwCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        private MainPageViewModel _mpvm;

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            _mpvm = DataContext as MainPageViewModel;
            _mpvm.CommandOperation += _mpvm_CommandOperation;
            Console.WriteLine(Int32.Parse("FFFFFFFF",System.Globalization.NumberStyles.HexNumber));
        }

        private void _mpvm_CommandOperation(object sender, CommandArgs args) {

            switch (args.Command)
            {
                case "CancelCommand":
                    YT_ColorPicker s = new YT_ColorPicker();
                    s.ShowDialog(App.Current.MainWindow);
                    break;
            }
        }
    }
}
