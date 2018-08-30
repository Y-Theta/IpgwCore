using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IpgwCore.Controls.Dialogs {
    internal class YT_ColorPicker : YT_DialogBase {
        #region Properties
        public Int32 Argb {
            get { return (Int32)GetValue(ArgbProperty); }
            set { SetValue(ArgbProperty, value); }
        }
        public static readonly DependencyProperty ArgbProperty =
            DependencyProperty.Register("Argb", typeof(Int32),
                typeof(YT_ColorPicker), new PropertyMetadata(Int32.Parse("ff000000", System.Globalization.NumberStyles.HexNumber)));
        #endregion

        #region Methods
        protected override void OnClosing(CancelEventArgs e) {

        }

        public void ShowDialog(Window Holder, Color color) {
            base.ShowDialog(Holder);
            Argb = color.ToArgb();
        }
        #endregion

        #region Constructors
        public YT_ColorPicker() {
            this.WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
        }

        static YT_ColorPicker() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_ColorPicker), new FrameworkPropertyMetadata(typeof(YT_ColorPicker)));
        }
        #endregion
    }

}
