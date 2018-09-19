using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace NEUHCore {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        public static string Root = AppDomain.CurrentDomain.BaseDirectory;
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

        private void LoadLibs() {
            AppDomain.CurrentDomain.AssemblyResolve += MyResolveEventHandler; ;
        }

        public static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args) {
            Assembly MyAssembly, objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames) {
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(","))) {
                    strTempAssmbPath = Root + Component + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    break;
                }
            }
            MyAssembly = Assembly.LoadFrom(strTempAssmbPath);

            return MyAssembly;
        }
        #endregion

        #region Constructor
        public App() {
            LoadLibs();
            Startup += App_Startup;
        }
        #endregion
    }
}
