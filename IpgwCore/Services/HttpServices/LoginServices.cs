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

        private void KeyValuePairsCheck()//请求键值对检查
       {
            for (int i = 0; i < InfSet.KeyValuePairs.Count; i++)
            {
                if (InfSet.KeyValuePairs[i].Value == "")
                {
                    //TODO:
                    //MessageService.Instence.ShowError(null, "请完善登陆信息");
                    break;
                }
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
                //MessageService.Instence.ShowError(null, "请检查网络连接");
                return null;
            }
            return temp;
        }

        public string GetString(string uri) {
            if (InfSet.Compressed)
                return GetDatasetByString(GetStream(uri));
            else
                try { return _httpClient.GetStringAsync(uri).Result; }
                catch (AggregateException)
                {
                    // MessageService.Instence.ShowError(null, "请检查网络连接");
                    return null;
                }
        }

        public void Post(string uri, List<KeyValuePair<string, string>> items) {
            try
            {
                if (InfSet.NeedLogin)
                    _response = _httpClient.PostAsync(uri + _id, new FormUrlEncodedContent(items)).Result;
                else
                    _response = _httpClient.PostAsync(uri, new FormUrlEncodedContent(items)).Result;
            }
            catch (AggregateException)
            { // MessageService.Instence.ShowError(null, "请检查网络连接"); } 
            }
        }

        public void Post(string uri, Dictionary<string, string> keyValuePairs) {
            for (int kvp = 0; kvp < InfSet.KeyValuePairs.Count; kvp++)
                if (keyValuePairs.ContainsKey(InfSet.KeyValuePairs[kvp].Key))
                    InfSet.KeyValuePairs[kvp] = new KeyValuePair<string, string>(InfSet.KeyValuePairs[kvp].Key, keyValuePairs[InfSet.KeyValuePairs[kvp].Key]);
            if (InfSet.NeedLogin)
                try { _response = _httpClient.PostAsync(uri + _id, new FormUrlEncodedContent(InfSet.KeyValuePairs)).Result; }
                catch (AggregateException)
                {
                    //MessageService.Instence.ShowError(null, "请检查网络连接");
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

        #region Constructors
        public LoginServices() {
            _clientHandler = new HttpClientHandler { UseCookies = true };
            _httpClient = new HttpClient(_clientHandler);
            _httpClient.MaxResponseContentBufferSize = 25600;
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            _httpClient.Timeout = TimeSpan.FromDays(1);
        }
        #endregion

    }

}
