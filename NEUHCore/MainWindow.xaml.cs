using NEUHCore.Services;
using NEUHCore.UserSetting;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using YControls;
using NEUH_Contract;
using Button = System.Windows.Controls.Button;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NEUHCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            PluginServices.Instence.OnPluginChanged += Instence_OnPluginChanged;

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
            foreach (var plugins in PluginServices.Instence.Control.AddInContracts) {
                if (plugins.Name.Equals("IpgwCore")) {
                    plugins.Run(CaseName.AreaIcon, out object t);
                    Console.WriteLine(((ObservableCollection<String>)t)[1]);
                }
            }
            // PluginServices.Instence.Unload("IpgwCore");
        }

    }
}
