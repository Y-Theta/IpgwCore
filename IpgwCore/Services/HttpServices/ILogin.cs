using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Services.HttpServices {

    public interface ILogin {
        /// <summary>
        /// post请求
        /// </summary>
        bool Post(string uri, List<KeyValuePair<string, string>> items);

        /// <summary>
        /// 带验证post请求
        /// </summary>
        bool Post(string uri, Dictionary<string, string> keyValuePairs);

        /// <summary>
        /// 获得字符型返回数据
        /// </summary>
        String GetString(string uri,bool compress);

        /// <summary>
        /// 获得流型返回数据
        /// </summary>
        Stream GetStream(string uri);
    }
}
