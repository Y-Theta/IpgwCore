using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Services.HttpServices {

    internal interface ILogin {
        /// <summary>
        /// post请求
        /// </summary>
        void Post(string uri, List<KeyValuePair<string, string>> items);

        /// <summary>
        /// 带验证post请求
        /// </summary>
        void Post(string uri, Dictionary<string, string> keyValuePairs);

        /// <summary>
        /// 获得字符型返回数据
        /// </summary>
        String GetString(string uri);

        /// <summary>
        /// 获得流型返回数据
        /// </summary>
        Stream GetStream(string uri);
    }
}
