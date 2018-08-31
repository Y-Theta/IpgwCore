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
using IpgwCore.View;
using IpgwCore.Controls.FlowControls;
using IpgwCore.Controls;

namespace IpgwCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        private MainPageViewModel _mpvm;
        YT_Popup pop = new YT_Popup();

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            _mpvm = DataContext as MainPageViewModel;
            _mpvm.CommandOperation += _mpvm_CommandOperation;
            pop.Style = App.Current.Resources["FluxInfoPopup"] as Style;
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            int a = wParam.ToInt32();
            switch (a)
            {
                case Win32Funcs.W_HIDE:
                    PopupMessageServices.Instence.MainWindowVisibility = App.Current.MainWindow.Visibility;
                    break;
            }
            return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
        }

        private void _mpvm_CommandOperation(object sender, CommandArgs args) {

            switch (args.Command)
            {
                case "CancelCommand":
                    pop.IsOpen = true;
                    break;
            }
        }
    }
}
