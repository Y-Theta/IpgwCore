using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using IpgwCore.ViewModel;

namespace IpgwCore.Services.MessageServices {
    internal class PopupMessageServices : IMessageServices {
        #region Properties
        private static PopupMessageServices _instence;
        private static readonly object _singleton_Lock = new object();
        public static PopupMessageServices Instence {
            get {
                if (_instence == null)
                    lock (_singleton_Lock)
                        if (_instence == null)
                            _instence = new PopupMessageServices();
                return _instence;
            }
        }

        private IMessagePoster _messageHolder;
        public IMessagePoster MessageHolder {
            get => _messageHolder;
            set => _messageHolder = value;
        }

        #endregion

        #region Methods
        public bool ShowContent(object obj) {
            throw new NotImplementedException();
        }

        public void ShowString(string str) {
            MessageHolder._message = str;
        }

        public bool ShowContentAt(UIElement obj, object con) {
            throw new NotImplementedException();
        }

        #endregion

        #region Constructors
        public PopupMessageServices() {

        }

        #endregion
    }

}
