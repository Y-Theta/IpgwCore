using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IpgwCore {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        public static string RootPath = AppDomain.CurrentDomain.BaseDirectory;
        public const string WebConfig = @"\Configs\Web-config.xml";
        public const string FluxLog = @"\Configs\FluxLog.xml";
        public const string CourseData = @"\Configs\CourseData.xml";

    }
}
