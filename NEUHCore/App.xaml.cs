using NEUHCore.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Windows;

namespace NEUHCore {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        public static string Root = AppDomain.CurrentDomain.BaseDirectory;
        public const string LibsPath = "LibsPath";
        public const string Data = @"Data\";
        public const string Component = @"Data\Bin\";
        public const string Plugin = @"Data\Plugins\";
        public const string Cache = @"Data\Cache\";

        #region Method
        private void App_Startup(object sender, StartupEventArgs e) {
            App.Current.SessionEnding += Current_SessionEnding;
            MainWindow window = new MainWindow();
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);
        }

        private void Current_SessionEnding(object sender, SessionEndingCancelEventArgs e) {
           
        }

        #endregion

        #region Constructor
        public App() {
            AppDomain.CurrentDomain.SetData(LibsPath,new string[] {
                App.Root + App.Component
            });
            AssemblyServices.HockResolve(AppDomain.CurrentDomain);
            Startup += App_Startup;
        }
        #endregion
    }
}
