using NEUHCore.Services;
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
using YControls;

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
            Console.WriteLine(PluginServices.Instence.ShowDomains());
            Console.WriteLine(PluginServices.Instence.Control.ShowPlugins());
            PluginServices.Instence.Control.AddInContracts[0].Run(NEUH_Contract.UseCase.MainWindowInit);
        }
    }
}
