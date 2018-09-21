using NEUHCore.Services;
using NEUHCore.UserSetting;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using YControls;
using NEUH_Contract;

namespace NEUHCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        public MainWindow() {
            foreach (var plugins in PluginServices.Instence.Control.AddInContracts) {
                if (plugins.Usage.ContainsKey(CaseName.AreaIcon)) {
                    AllowAreaIcon = true;
                    RegisterAreaIcon(plugins.Name);
                    plugins.Run(CaseName.AreaIcon, out object obj);
                    AreaIcons[plugins.Name].Areaicon = (Icon)obj;
                }
            }
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            PluginServices.Instence.OnPluginChanged += Instence_OnPluginChanged;
            foreach (var plugins in PluginServices.Instence.Control.AddInContracts) {
                if (plugins.Usage.ContainsKey(CaseName.MainWindowInit)) {
                    plugins.Run(CaseName.MainWindowInit);
                }
            }
        }

        private void Instence_OnPluginChanged(object sender, NEUH_PluginControl.PluginChangedArgs args) {
            switch (args.Action) {
                case NEUH_PluginControl.PluginAction.Unload:
                    DisposAreaIcon(args.Name);
                    foreach (var plugins in PluginServices.Instence.Control.AddInContracts)
                        if (plugins.Name.Equals(args.Name))
                            plugins.Run(CaseName.UnLoad);
                    break;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            Console.WriteLine(PluginServices.Instence.Control.ShowPlugins());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            PluginServices.Instence.Unload("IpgwCore");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            PluginServices.Instence.Update();
        }
    }
}
