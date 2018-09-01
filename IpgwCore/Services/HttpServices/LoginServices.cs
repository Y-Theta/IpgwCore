using System;
using IpgwCore.Model.BasicModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO.Compression;
using System.Net;
using IpgwCore.MVVMBase;
using IpgwCore.Services.MessageServices;
using IpgwCore.Services.FormatServices;

namespace IpgwCore.Services.HttpServices {
    /// <summary>
    /// 登录服务
    /// </summary>
    internal class LoginServices : ILogin {
        #region Properties

        private static LoginServices _instence;
        private static readonly object _singleton_Lock = new object();
        public static LoginServices Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new LoginServices();
                return _instence;
            }
        }

        /// <summary>
        /// 网络是否连接
        /// </summary>
        public event PropertyChangedCallBack IpgwConnectedChanged;
        private bool _ipgwConnected;
        public bool IpgwConnected {
            get => _ipgwConnected;
            set {
                IpgwConnectedChanged?.Invoke(_ipgwConnected, value);
                _ipgwConnected = value;
            }
        }

        private InfoSet _infSet;
        /// <summary>
        /// 网站信息
        /// </summary>
        public InfoSet InfSet {
            get => _infSet;
            set => _infSet = value;
        }

        /// <summary>
        /// 字符形式的网页
        /// </summary>
        private String _result { get; set; }

        /// <summary>
        /// 用于验证的信息
        /// </summary>
        private String _id { get; set; }

        /// <summary>
        /// 模拟登录的类
        /// </summary>
        private HttpClient _httpClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private HttpClientHandler _clientHandler { get; set; }

        /// <summary>
        /// 接收网页返回的类
        /// </summary>
        private HttpResponseMessage _response { get; set; }

        #endregion


        #region Methods

        private void RefrashinfSet(string name) {
            if (InfSet != null && InfSet.name.Equals(name))
                return;
            InfSet = XmlDocService.Instence.GetNode<InfoSet>(new XmlPath { Key = name });
        }

        private void KeyValuePairsCheck() {
            for (int i = 0; i < InfSet.KeyValuePairs.Count; i++)
                if (InfSet.KeyValuePairs[i].Value == "")
                {
                    PopupMessageServices.Instence.ShowContent("请完善登录信息！");
                    break;
                }
            if (InfSet.Cookies.Count != 0)
            {
                foreach (KeyValuePair<string, string> kv in InfSet.Cookies)
                {
                    Cookie cookie = new Cookie(kv.Key, kv.Value)
                    {
                        Expires = DateTime.MaxValue
                    };
                    _clientHandler.CookieContainer.Add(new Uri(InfSet.Uris[0]), cookie);
                }
                InfSet.HasCookie = true;
            }
        }

        public Stream GetStream(string uri) {
            Stream temp;
            try { temp = _httpClient.GetStreamAsync(uri).Result; }
            catch (AggregateException)
            {
                return null;
            }
            return temp;
        }

        public string GetString(string uri,bool compress) {
            if (compress)
                return GetDatasetByString(GetStream(uri));
            else
                try {
                    return _httpClient.GetStringAsync(uri).Result;
                }
                catch (AggregateException)
                {
                    return null;
                }
        }

        public bool Post(string uri, List<KeyValuePair<string, string>> items) {
            try
            {
                if (InfSet.NeedLogin)
                    _response = _httpClient.PostAsync(uri + _id, new FormUrlEncodedContent(items)).Result;
                else
                    _response = _httpClient.PostAsync(uri, new FormUrlEncodedContent(items)).Result;
                return true;
            }
            catch (AggregateException)
            {
                return false;
            }
        }

        public bool Post(string uri, Dictionary<string, string> keyValuePairs) {
            List<KeyValuePair<string, string>> KVs = new List<KeyValuePair<string, string>>();
            foreach (var kvs in InfSet.KeyValuePairs)
            {
                if (keyValuePairs.ContainsKey(kvs.Key))
                    continue;
                KVs.Add(kvs);
            }
            foreach(var kvs in keyValuePairs) 
                KVs.Add(new KeyValuePair<string, string>(kvs.Key, kvs.Value));
            try
            {
                if (InfSet.NeedLogin)
                    _response = _httpClient.PostAsync(uri + _id, new FormUrlEncodedContent(KVs)).Result;
                else
                    _response = _httpClient.PostAsync(uri, new FormUrlEncodedContent(KVs)).Result;
                return true;
            }
            catch (AggregateException)
            {
                return false;
            }
        }

        /// <summary>
        /// 网页解压
        /// </summary>
        private String GetDatasetByString(Stream Value) {
            string strHTML = "";
            GZipStream gzip = new GZipStream(Value, CompressionMode.Decompress);//解压缩
            using (StreamReader reader = new StreamReader(gzip, Encoding.GetEncoding(InfSet.CharSet)))//中文编码处理
            {
                strHTML = reader.ReadToEnd();
            }
            return strHTML;
        }
        #endregion

        #region OuterMethod

        public async void IpgwConnectTest() {
            Task tsk = new Task(() =>
            {
                string str = "";
                Stream baidu = GetStream(@"https://www.baidu.com/");
                if (baidu is null)
                    App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        IpgwConnected = false;
                    }));
                else
                {
                    GZipStream gzip = new GZipStream(baidu, CompressionMode.Decompress);
                    using (StreamReader reader = new StreamReader(gzip, Encoding.GetEncoding("utf-8")))
                    {
                        str = reader.ReadToEnd();
                    }
                    if (str.Contains("百度搜索"))
                        App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                        {
                            IpgwConnected = true;
                        }));
                    else
                        App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                        {
                            IpgwConnected = false;
                        }));
                }
            });
            tsk.Start();
            await tsk;
        }

        public bool LoginIpgw() {
            RefrashinfSet(ConstTable.IPGW);
            Post(InfSet.Uris[0], InfSet.KeyValuePairs);
            String rest = GetString(InfSet.Uris[1], InfSet.Compressed);
            if (rest.Equals("not_online"))
            {
                IpgwConnected = false;
                PopupMessageServices.Instence.ShowContent("网关被占用,请先断开连接后重新连接!");
            }
            else
            {
                IpgwConnected = true;
                PopupMessageServices.Instence.ShowContent("网络已连接.");
            }
            return IpgwConnected;
        }

        public bool LogoutIpgw() {
            RefrashinfSet(ConstTable.IPGW);
            Post(InfSet.Uris[0], new Dictionary<string, string>() {
                {"action","logout" },
            });
            PopupMessageServices.Instence.ShowContent("网络已断开.");
            return IpgwConnected;
        }

        public string GetFluxInfo() {
            RefrashinfSet(ConstTable.IPGW);
            if (IpgwConnected)
                return GetString(InfSet.Uris[1], InfSet.Compressed);
            else
            {
                RefrashinfSet(ConstTable.IPGW);
                Post(InfSet.Uris[0], InfSet.KeyValuePairs);
                return GetString(InfSet.Uris[1], InfSet.Compressed);
            }
        }
        #endregion

        #region Constructors
        public LoginServices() {
            _clientHandler = new HttpClientHandler { UseCookies = true };
            _httpClient = new HttpClient(_clientHandler);
            _httpClient.MaxResponseContentBufferSize = 25600;
            _httpClient.Timeout = TimeSpan.FromSeconds(3);
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        }
        #endregion

    }

}
