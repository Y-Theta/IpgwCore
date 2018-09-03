using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows;
using Drawing = System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using IpgwCore.Services.HttpServices;
using IpgwCore.Services.FormatServices;
using IpgwCore.Controls.FlowControls;
using IpgwCore.Model.BasicModel;
using IpgwCore.View;

namespace IpgwCore.Controls.AreaWindow {


    public class YT_AreaIcon : Control {
        //NormalProperties
        private System.Windows.Forms.NotifyIcon _flowicon;

        private IntPtr _ico = IntPtr.Zero;

        private PrivateFontCollection _pfc;

        private Font _areafont;

        private float _areafontsize;

        private Color _areafontcolor;

        #region AttachedWindow
        public object AttachedWindow {
            get { return (object)GetValue(AttachedWindowProperty); }
            set { SetValue(AttachedWindowProperty, value); }
        }
        public static readonly DependencyProperty AttachedWindowProperty =
            DependencyProperty.Register("AttachedWindow", typeof(object), typeof(YT_AreaIcon),
                new PropertyMetadata(null, new PropertyChangedCallback(OnAttachedWindowChanged)));
        private static void OnAttachedWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            YT_AreaIcon dai = (YT_AreaIcon)d;
        }
        #endregion

        #region AreaVisibility
        public bool AreaVisibility {
            get { return (bool)GetValue(AreaVisibilityProperty); }
            set { SetValue(AreaVisibilityProperty, value); }
        }
        public static readonly DependencyProperty AreaVisibilityProperty =
            DependencyProperty.Register("AreaVisibility", typeof(bool), typeof(YT_AreaIcon),
                new PropertyMetadata(false, new PropertyChangedCallback(OnAreaVisibilityChanged)));
        private static void OnAreaVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            YT_AreaIcon dai = (YT_AreaIcon)d;
            dai._flowicon.Visible = (bool)e.NewValue;
        }
        #endregion

        #region CheckPop
        public YT_Popup CheckPop {
            get { return (YT_Popup)GetValue(CheckPopProperty); }
            set { SetValue(CheckPopProperty, value); }
        }
        public static readonly DependencyProperty CheckPopProperty =
            DependencyProperty.Register("CheckPop", typeof(YT_Popup),
                typeof(YT_AreaIcon), new PropertyMetadata(null));
        #endregion

        #region Ccontextmenu
        public YT_ContextMenu DContextmenu {
            get { return (YT_ContextMenu)GetValue(DContextmenuProperty); }
            set { SetValue(DContextmenuProperty, value); }
        }
        public static readonly DependencyProperty DContextmenuProperty =
            DependencyProperty.Register("DContextmenu", typeof(YT_ContextMenu),
                typeof(YT_AreaIcon), new PropertyMetadata(null));
        #endregion

        #region 初始化
        private void InitNotifyIcon() {
            InitRes();
            _flowicon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(),
                Icon = Drawicon(FluxConv.GetFluxPercent(Formater.Instence.IpgwInfo).ToString())
            };
            string str = FluxConv.GetFluxPercent(Formater.Instence.IpgwInfo).ToString();
            _flowicon.MouseClick += _flowicon_MouseClick;
            _flowicon.MouseMove += _flowicon_MouseMove;
            _flowicon.MouseDoubleClick += _flowicon_MouseDoubleClick;
        }

        private void InitRes() {
            byte[] myText = null;
            IntPtr MeAdd = IntPtr.Zero;

            myText = (byte[])Properties.Resources.ResourceManager.GetObject("Rect");
            _pfc = new PrivateFontCollection();
            MeAdd = Marshal.AllocHGlobal(myText.Length);
            Marshal.Copy(myText, 0, MeAdd, myText.Length);
            _pfc.AddMemoryFont(MeAdd, myText.Length);

            _areafont = new Font(_pfc.Families[0], _areafontsize);
        }

        private void InitHock() {
            Properties.Settings.Default.SettingChanging += Default_SettingChanging;
            Formater.Instence.IpgwInfoChanged += Instence_IpgwInfoChanged;
        }
        #endregion

        #region 事件方法
        private void _flowicon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Right:
                    if (DContextmenu is null)
                        return;
                    if (DContextmenu.IsOpen)
                        return;
                    DContextmenu.IsOpen = true;
                    break;
                case System.Windows.Forms.MouseButtons.Left:
                    if (CheckPop is null)
                        return;
                    if (CheckPop.IsOpen)
                        return;
                    LoginServices.Instence.IpgwConnectTest();
                    CheckPop.IsOpen = true;
                    break;
            }
        }

        private void _flowicon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e) {


        }

        private void _flowicon_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {

        }


        #endregion

        #region GraphUpdata -- 托盘画图

        public Icon Drawicon(string num) {
            int size = 32;
            Drawing.Image bufferedimage;
            if (_ico == IntPtr.Zero)
                bufferedimage = new Bitmap(size, size, Drawing.Imaging.PixelFormat.Format32bppArgb);
            else
                bufferedimage = Bitmap.FromHicon(_ico);

            Graphics g = Graphics.FromImage(bufferedimage);
            g.Clear(Drawing.Color.FromArgb(0, 255, 255, 255));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            Drawing.Pen pen = new Drawing.Pen(_areafontcolor, 1f);
            SizeF infsize = g.MeasureString(num, _areafont);
            g.DrawString(num, _areafont, pen.Brush,
                new Drawing.Point((int)((size - infsize.Width) / 2), (int)((size - infsize.Height) / 2)));
            _ico = (bufferedimage as Bitmap).GetHicon();

            bufferedimage.Dispose();
            g.Dispose();

            return Icon.FromHandle(_ico);
        }
        #endregion

        #region 公共方法

        #endregion

        #region 设置更新
        private void Default_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e) {
            switch (e.SettingName)
            {
                case "AreaFontSize":
                    _areafontsize = (float)e.NewValue;
                    _areafont = new Font(_pfc.Families[0], _areafontsize);
                    _flowicon.Icon = Drawicon(FluxConv.GetFluxPercent(Formater.Instence.IpgwInfo).ToString());
                    break;
                case "AreaFontColor":
                    _areafontcolor = (Color)e.NewValue;
                    _flowicon.Icon = Drawicon(FluxConv.GetFluxPercent(Formater.Instence.IpgwInfo).ToString());
                    break;
            }
        }

        private bool Instence_IpgwInfoChanged(object op, object np) {
            _flowicon.Icon = Drawicon(FluxConv.GetFluxPercent((Flux)np).ToString());
            return true;
        }

        #endregion

        public YT_AreaIcon() {
            _areafontsize = Properties.Settings.Default.AreaFontSize;
            _areafontcolor = Properties.Settings.Default.AreaFontColor;
            InitHock();
            InitNotifyIcon();
        }


    }
}
