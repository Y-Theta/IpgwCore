using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OS_CD {
    public sealed class YT_FormDialog :YT_DialogBase {

        #region Properties
        public string[] FormItems {
            get { return (string[])GetValue(FormItemsProperty); }
            set { SetValue(FormItemsProperty, value); }
        }
        public static readonly DependencyProperty FormItemsProperty =
            DependencyProperty.Register("FormItems", typeof(string[]),
                typeof(YT_FormDialog), new PropertyMetadata(null));

        public int FormItemCounts {
            get { return (int)GetValue(FormItemCountsProperty); }
            set { SetValue(FormItemCountsProperty, value);
                FormItems = new string[value];
            }
        }
        public static readonly DependencyProperty FormItemCountsProperty =
            DependencyProperty.Register("FormItemCounts", typeof(int), 
                typeof(YT_FormDialog), new PropertyMetadata(2,new PropertyChangedCallback(OnCountChanged)));
        private static void OnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        
        }

        #endregion

        #region Actions
        #endregion

        #region Overrides
        #endregion


        public YT_FormDialog() {
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        static YT_FormDialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_FormDialog), new FrameworkPropertyMetadata(typeof(YT_FormDialog)));
        }
    }
}
