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
using IpgwCore.Services.SystemServices;
using IpgwCore.Services.HttpServices;
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
        private YT_TitleBar _titlebar;
        bool _oa, _ob;

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            _mpvm = DataContext as MainPageViewModel;
            _mpvm.CommandOperation += _mpvm_CommandOperation;
            _titlebar = (YT_TitleBar)GetTemplateChild("TitleBar");
            _titlebar.CloseCommand.Execution += CloseCommand_Execution;
            RootFrame.Navigate(new Uri(ConstTable.PagePath + "MainPage.xaml", UriKind.Relative));
        }

        private void CloseCommand_Execution(object para = null) {
            GC.Collect(2, GCCollectionMode.Forced);
            if (!Properties.Settings.Default.ExitAsk)
            {
                _oa = Properties.Settings.Default.ExitAsk;
                _ob = Properties.Settings.Default.ExitArea;
                _mpvm.ExitTip = Properties.Settings.Default.ExitArea ? "是否以托盘状态运行?" : "是否直接退出?";
                YT_FormDialog dialog = new YT_FormDialog { Style = App.Current.Resources["ExitDialog"] as Style };
                dialog.CancelAction += Dialog_NoAction;
                dialog.YesAction += Dialog_YesAction;
                dialog.NoAction += Dialog_NoAction;
                dialog.ShowDialog(this);
            }
            else
            {
                if (Properties.Settings.Default.ExitArea)
                    App.Current.MainWindow.Hide();
                else
                {
                    Properties.Settings.Default.Save();
                    App.Current.Shutdown();
                }

            }
        }

        private void Dialog_NoAction(object para = null) {
            Properties.Settings.Default.ExitAsk = _oa;
            Properties.Settings.Default.ExitArea = _ob;
        }

        private void Dialog_YesAction(object para = null) {
            if (Properties.Settings.Default.ExitArea)
                App.Current.MainWindow.Hide();
            else
            {
                Properties.Settings.Default.Save();
                Application.Current.Shutdown();
            }
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            int a = wParam.ToInt32();
            int b = lParam.ToInt32();
            switch (msg)
            {
                case 8:
                    PopupMessageServices.Instence.MainWindowVisibility = Visibility.Hidden;
                    break;
                case 7:
                    PopupMessageServices.Instence.MainWindowVisibility = Visibility.Visible;
                    break;
            }
            return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
        }

        private void _mpvm_CommandOperation(object sender, CommandArgs args) {

            switch (args.Command)
            {
                case "Operation":
                    switch (args.Parameter.ToString())
                    {
                        case "Exit":
                            App.Current.Shutdown();
                            break;
                        case "Show":
                            App.Current.MainWindow.Show();
                            break;
                        case "About":
                            App.Current.MainWindow.Show();
                            RootFrame.Navigate(new Uri(ConstTable.PagePath + "AboutPage.xaml", UriKind.Relative));
                            break;
                    }
                    break;
                case "Nvigate":
                    RootFrame.Navigate(new Uri(ConstTable.PagePath + args.Parameter.ToString(), UriKind.Relative));
                    break;
            }
        }
    }
}
