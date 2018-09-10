using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpgwCore.Model.BasicModel {
    public class InfoSet {
        #region Properties
        /// <summary>
        /// 是否需要登录信息
        /// </summary>
        public bool NeedLogin { get; set; }

        /// <summary>
        /// 网页是否压缩
        /// </summary>
        public bool Compressed { get; set; }

        /// <summary>
        /// 是否需要验证码
        /// </summary>
        public bool Verify { get; set; }

        /// <summary>
        /// 是否已有Cookies
        /// </summary>
        public bool HasCookie { get; set; }

        /// <summary>
        /// 标识名称
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// 网页编码格式
        /// </summary>
        public String CharSet { get; set; }

        /// <summary>
        /// 验证码地址
        /// </summary>
        public String VerifyCode { get; set; }

        private List<String> _uris;
        /// <summary>
        /// 网页所需uri
        /// </summary>
        public List<String> Uris {
            get {
                if (_uris is null)
                    _uris = new List<String>();
                return _uris;
            }
            set => _uris = value;
        }

        private List<KeyValuePair<String, String>> _keyValuePairs;
        /// <summary>
        /// 网页所需登录信息
        /// </summary>
        public List<KeyValuePair<String, String>> KeyValuePairs {
            get {
                if (!NeedLogin)
                    return null;
                else if (_keyValuePairs is null)
                    _keyValuePairs = new List<KeyValuePair<String, String>>();
                return _keyValuePairs;
            }
            set => _keyValuePairs = value;
        }

        private List<KeyValuePair<String, String>> _cookies;
        /// <summary>
        /// 网页所需Cookies
        /// </summary>
        public List<KeyValuePair<String, String>> Cookies {
            get {
                if (!NeedLogin)
                    return null;
                else if (_cookies is null)
                    _cookies = new List<KeyValuePair<String, String>>();
                return _cookies;
            }
            set => _cookies = value;
        }

        private KeyValuePair<String, String> _idcodes;
        /// <summary>
        /// 网页所需验证码
        /// </summary>
        public KeyValuePair<String, String> IdCodes {
            get => _idcodes;
            set => _idcodes = value;
        }
        #endregion
    }

}
