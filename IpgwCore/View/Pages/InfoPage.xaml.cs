using IpgwCore.Model.BasicModel;
using IpgwCore.Services.FormatServices;
using IpgwCore.Services.MessageServices;
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
        SettingPageViewModel _spvm;

        public InfoPage() {
            InitializeComponent();
            Loaded += InfoPage_Loaded;
        }

        private void InfoPage_Loaded(object sender, RoutedEventArgs e) {
            (App.Current.Resources["MainPageVM"] as MainPageViewModel).Msg = "用户信息";
            InitControls();
            _spvm = DataContext as SettingPageViewModel;
            _spvm.CommandOperation += _spvm_CommandOperation;
        }

        private void _spvm_CommandOperation(object sender, MVVMBase.CommandArgs args) {
            switch (args.Command)
            {
                case "Operation":
                    switch (args.Parameter)
                    {
                        case "IPGWSubmit":
                            if (IPGWA.IsEnabled)
                                TestIpgwValue();
                            else
                                EnableIpgwInput();
                            break;
                    }
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 初始化控件视觉效果
        /// </summary>
        private void InitControls() {
            if (Properties.Settings.Default.IPGWS)
            {
                IPGWA.IsEnabled = false;
                IPGWP.IsEnabled = false;
                Pack1.IsEnabled = false;
                Pack2.IsEnabled = false;
                IPGWA.Text = Properties.Settings.Default.IPGWA;
                IPGWP.Password = Properties.Settings.Default.IPGWP;
                if (Properties.Settings.Default.Package == 0)
                    Pack2.Visibility = Visibility.Collapsed;
                else
                    Pack1.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 保存Ipgw设置
        /// </summary>
        private void TestIpgwValue() {
            if (IPGWA.Text.Equals(string.Empty) || IPGWP.Password.Equals(String.Empty) || IPGWA.Text.Length != 8 && IPGWA.Text.Length != 7)
            {
                PopupMessageServices.Instence.ShowContent("请输入有效学号 7/8位");
                return;
            }
            Properties.Settings.Default.IPGWS = true;
            Properties.Settings.Default.IPGWA = IPGWA.Text;
            Properties.Settings.Default.IPGWP = IPGWP.Password;
            XmlDocService.Instence.UpdateNode(new Dictionary<string, string> {
                { "username" , IPGWA.Text},
                { "password" , IPGWP.Password}
            }, new XmlPath { Key = ConstTable.IPGW });
            IPGWA.IsEnabled = false;
            IPGWP.IsEnabled = false;
            Pack1.IsEnabled = false;
            Pack2.IsEnabled = false;
            if (Properties.Settings.Default.Package == 0)
                Pack2.Visibility = Visibility.Collapsed;
            else
                Pack1.Visibility = Visibility.Collapsed;
            PopupMessageServices.Instence.ShowContent("信息已保存!");
        }

        /// <summary>
        /// 修改Ipgw设置
        /// </summary>
        private void EnableIpgwInput() {
            Properties.Settings.Default.IPGWS = false;
            IPGWA.IsEnabled = true;
            IPGWP.IsEnabled = true;
            Pack1.IsEnabled = true;
            Pack2.IsEnabled = true;
            Pack1.Visibility = Visibility.Visible;
            Pack2.Visibility = Visibility.Visible;
        }
    }
}
