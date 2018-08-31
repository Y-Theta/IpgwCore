using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using IpgwCore.ViewModel;
using IpgwCore.Controls.FlowControls;

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

        public event EventHandler MainWindowVisibilityChanged;

        private Visibility _mainWindowVisibility;
        public Visibility MainWindowVisibility {
            get => _mainWindowVisibility;
            set {
                MainWindowVisibilityChanged?.Invoke(value, null);
                if (value.Equals(Visibility.Hidden))
                    PopupInstence.IsOpen = false;
                _mainWindowVisibility = value;
            }
        }

        private YT_Popup _popupInstence;
        protected YT_Popup PopupInstence {
            get {
                if (_popupInstence is null)
                    _popupInstence = new YT_Popup { AutoHide = true , AttachedWindow = App.Current.MainWindow };
                return _popupInstence;
            }
            set => _popupInstence = value;
        }

        private IMessagePoster _messageHolder;
        public IMessagePoster MessageHolder {
            get => _messageHolder;
            set => _messageHolder = value;
        }

        #endregion

        #region Methods
        public bool ShowContent(Style obj) {
            if (PopupInstence.IsOpen)
                PopupInstence.IsOpen = false;
            PopupInstence.Style = obj;
            if (MainWindowVisibility.Equals(Visibility.Hidden))
                PopupInstence.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
            else
            {
                PopupInstence.Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint;
                PopupInstence.PlacementTarget = App.Current.MainWindow;
            }
            PopupInstence.IsOpen = true;
            return true;
        }

        public void ShowString(string str) {
            MessageHolder._message = str;
        }

        public bool ShowContentAt(UIElement obj, Style con) {
            return true;
        }

        public bool ShowContent(string s) {
            if (PopupInstence.IsOpen)
                PopupInstence.IsOpen = false;
            PopupInstence.TextContent = s;
            PopupInstence.Style = App.Current.Resources["MessageContent"] as Style;
            if (MainWindowVisibility.Equals(Visibility.Hidden))
                PopupInstence.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
            else
            {
                PopupInstence.Placement = System.Windows.Controls.Primitives.PlacementMode.RelativePoint;
                PopupInstence.PlacementTarget = App.Current.MainWindow;
            }
            PopupInstence.IsOpen = true;
            return true;
        }

        #endregion

        #region Constructors
        public PopupMessageServices() {
            
        }

        #endregion
    }

}
