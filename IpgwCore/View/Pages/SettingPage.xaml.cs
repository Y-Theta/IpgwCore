using IpgwCore.Controls.Dialogs;
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
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page {
        SettingPageViewModel _spvm;


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            AreaIconFontSize.GiveupFocus();
        }

        private void _spvm_CommandOperation(object sender, MVVMBase.CommandArgs args) {
            switch (args.Parameter) {
                case "Color":
                    YT_ColorPicker cp = new YT_ColorPicker();
                    cp.ShowDialog(App.Current.MainWindow, Properties.Settings.Default.AreaFontColor);
                    Properties.Settings.Default.AreaFontColor = ColorNumConv.ToColorD(cp.Argb);
                    break;
            }
        }

        private void SettingPage_Unloaded(object sender, RoutedEventArgs e) {
            _spvm.CommandOperation -= _spvm_CommandOperation;
        }


        private void SettingPage_Loaded(object sender, RoutedEventArgs e) {
            (App.Current.Resources["MainPageVM"] as MainPageViewModel).Msg = "软件设置";
            _spvm = DataContext as SettingPageViewModel;
            _spvm.CommandOperation += _spvm_CommandOperation;
        }

        public SettingPage() {
            InitializeComponent();
            Loaded += SettingPage_Loaded;
            Unloaded += SettingPage_Unloaded;
        }
    }
}
