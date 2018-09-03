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
using IpgwCore.Services.SystemServices;
using IpgwCore.Services.FormatServices;
using IpgwCore.Services.MessageServices;

namespace IpgwCore.View.Pages {
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page {
        SettingPageViewModel _spvm;
        YT_FormDialog _resd;


        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            AreaIconFontSize.GiveupFocus();
        }

        private void _spvm_CommandOperation(object sender, MVVMBase.CommandArgs args) {
            switch (args.Parameter)
            {
                case "Color":
                    YT_ColorPicker cp = new YT_ColorPicker();
                    cp.ShowDialog(App.Current.MainWindow, Properties.Settings.Default.AreaFontColor);
                    Properties.Settings.Default.AreaFontColor = ColorNumConv.ToColorD(cp.Argb);
                    break;
                case "Free":
                    XmlDocService.Instence.ResetAll();
                    _spvm.UpdateCache();
                    PopupMessageServices.Instence.ShowContent("已清空全部缓存");
                    break;
                case "Reset":
                    _resd.Question = "确认清除所有数据?\n(包括自定义设置)";
                    _resd.YesAction += _resd_YesAction;
                    _resd.ShowDialog(App.Current.MainWindow);
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

        private void Default_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e) {
            switch (e.SettingName)
            {
                case "SelfRunning":
                    SystemServices.Instence.SetSelfRunning((bool)e.NewValue);
                    break;
            }
        }

        private void _resd_YesAction(object para = null) {
            XmlDocService.Instence.ResetAll();
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            _spvm.UpdateCache();
            PopupMessageServices.Instence.ShowContent("已恢复至初始设置");
        }

        public SettingPage() {
            InitializeComponent();
            _resd = new YT_FormDialog { Style = App.Current.Resources["QuestionDialog"] as Style };
            Properties.Settings.Default.SettingChanging += Default_SettingChanging;
            Loaded += SettingPage_Loaded;
            Unloaded += SettingPage_Unloaded;
        }


    }
}
