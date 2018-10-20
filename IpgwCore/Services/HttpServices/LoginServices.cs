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
    public class LoginServices : ILogin {
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
        private HttpClientHandler _clientHandler { get; set; }

        /// <summary>
        /// 接收网页返回的类
        /// </summary>
        private HttpResponseMessage _response { get; set; }

        #endregion


        #region Methods

        private bool RefrashinfSet(string name) {
            InfSet = XmlDocService.Instence.GetNode<InfoSet>(new XmlPath { Key = name });
            return KeyValuePairsCheck();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool KeyValuePairsCheck() {
            for (int i = 0; i < InfSet.KeyValuePairs.Count; i++)
                if (InfSet.KeyValuePairs[i].Value == "")
                    return false;
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
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public Stream GetStream(string uri) {
            Stream temp;
            try { temp = _httpClient.GetStreamAsync(uri).Result; }
            catch (AggregateException)
            {
                return null;
            }
            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetString(string uri, bool compress) {
            if (compress)
                return GetDatasetByString(GetStream(uri));
            else
                try
                {
                    return _httpClient.GetStringAsync(uri).Result;
                }
                catch (AggregateException)
                {
                    return null;
                }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public bool Post(string uri, Dictionary<string, string> keyValuePairs) {
            List<KeyValuePair<string, string>> KVs = new List<KeyValuePair<string, string>>();
            foreach (var kvs in InfSet.KeyValuePairs)
            {
                if (keyValuePairs.ContainsKey(kvs.Key))
                    continue;
                KVs.Add(kvs);
            }
            foreach (var kvs in keyValuePairs)
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
        /// <summary>
        /// 检测网络连接
        /// </summary>
        public async void IpgwConnectTest() {
            if (RefrashinfSet(ConstTable.IPGW))
            {
                Task tsk = new Task(() =>
                {
                    string rest = "";
                    rest = GetString(InfSet.Uris[1], InfSet.Compressed);
                    if (rest is null)
                    {
                        IpgwConnected = false;
                        return;
                    }
                    if (rest.Equals("not_online"))
                        App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                        {
                            IpgwConnected = false;
                        }));
                    else
                        App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                        {
                            IpgwConnected = true;
                            foreach (var kv in InfSet.KeyValuePairs)
                            {
                                if (kv.Key.Equals("username"))
                                    Properties.Settings.Default.UserID = kv.Value;
                            }
                        }));
                });
                tsk.Start();
                await tsk;
            }
        }

        /// <summary>
        /// 登录IPGW网关
        /// </summary>
        public bool LoginIpgw() {
            if (RefrashinfSet(ConstTable.IPGW))
            {
                Post(InfSet.Uris[0], InfSet.KeyValuePairs);
                String rest = GetString(InfSet.Uris[1], InfSet.Compressed);
                if (rest is null)
                {
                    IpgwConnected = false;
                    PopupMessageServices.Instence.ShowContent("请确保物理网络已连接!");
                    return false;
                }
                if (rest.Equals("not_online"))
                {
                    IpgwConnected = false;
                    String ori = _response.Content.ReadAsStringAsync().Result;
                    if (ori.Contains("用户不存在"))
                        PopupMessageServices.Instence.ShowContent("请输入正确的用户名!");
                    else if(ori.Contains("密码错误"))
                        PopupMessageServices.Instence.ShowContent("密码错误!");
                    else
                        PopupMessageServices.Instence.ShowContent("网关被占用,请先断开连接并重新登录!");
                    return IpgwConnected;
                }
                else
                {
                    IpgwConnected = true;
                    foreach (var kv in InfSet.KeyValuePairs)
                    {
                        if (kv.Key.Equals("username"))
                            Properties.Settings.Default.IPGWA =  Properties.Settings.Default.UserID = kv.Value;
                        if (kv.Key.Equals("password"))
                            Properties.Settings.Default.IPGWP = kv.Value;
                    }
                    PopupMessageServices.Instence.ShowContent("网络已连接.");
                    return IpgwConnected;
                }
            }
            else
                PopupMessageServices.Instence.ShowContent("请完善登录信息!");
            IpgwConnected = false;
            return IpgwConnected;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ConnectForUpdate() {
            Post(InfSet.Uris[0], InfSet.KeyValuePairs);
        }

        /// <summary>
        /// 断开IPGW网关
        /// </summary>
        public bool LogoutIpgw() {
            if (RefrashinfSet(ConstTable.IPGW))
            {
                Post(InfSet.Uris[0], new Dictionary<string, string>() {
                {"action","logout" }, });
                IpgwConnected = false;
                PopupMessageServices.Instence.ShowContent("网络已断开.");
            }
            else
                PopupMessageServices.Instence.ShowContent("请完善登录信息！");
            return IpgwConnected;
        }

        /// <summary>
        /// 获取流量信息
        /// </summary>
        public string GetFluxInfo() {
            if (RefrashinfSet(ConstTable.IPGW))
            {
                if (IpgwConnected)
                    return GetString(InfSet.Uris[1], InfSet.Compressed);
                else
                {
                    Post(InfSet.Uris[0], InfSet.KeyValuePairs);
                    return GetString(InfSet.Uris[1], InfSet.Compressed);
                }
            }
            return null;
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
