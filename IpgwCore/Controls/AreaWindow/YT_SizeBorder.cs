using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using IpgwCore.Controls;

namespace IpgwCore.AWindow {
    public class YT_SizeBorder : Border {
        private int resDirection;
        public int ResDirection {
            get { return resDirection; }
            set { resDirection = value; }
        }

        #region DirectionDictionary
        private Dictionary<int, Cursor> cursors = new Dictionary<int, Cursor>
        {
            {4,Cursors.SizeNWSE},//"LeftTop"
            {8,Cursors.SizeNWSE},//"RightBottom"
            {3,Cursors.SizeNS },//"Top"
            {6,Cursors.SizeNS },//"Bottom"
            {5,Cursors.SizeNESW },//"RightTop"
            {7,Cursors.SizeNESW },//"LeftBottom"
            {1,Cursors.SizeWE },//"Left"
            {2,Cursors.SizeWE }//"Right"
        };
        #endregion

        public static readonly DependencyProperty AttachedWindowProperty =
            DependencyProperty.Register("AttachedWindow", typeof(Window), typeof(YT_SizeBorder),
            new FrameworkPropertyMetadata(null));
        public Window AttachedWindow {
            get { return (Window)GetValue(AttachedWindowProperty); }
            set { SetValue(AttachedWindowProperty, value); }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            Cursor = Cursors.Arrow;
            double TK = this.BorderThickness.Bottom;
            if (TK < 3) TK = 3;
            double Hig = this.ActualHeight;
            double Wid = this.ActualWidth;
            Point p = Mouse.GetPosition(e.Source as FrameworkElement);

            resDirection = p.X <= TK && p.Y <= TK ? 4 :
                            p.X <= TK && p.Y <= Hig - TK ? 1 :
                            p.X <= TK && p.Y >= Hig - TK ? 7 :
                            p.X >= Wid - TK && p.Y <= TK ? 5 :
                            p.X >= Wid - TK && p.Y <= Hig - TK ? 2 :
                            p.X >= Wid - TK && p.Y >= Hig - TK ? 8 :
                            p.X <= Wid - TK && p.X >= TK && p.Y <= TK ? 3 : 6;

            if (AttachedWindow.WindowState != WindowState.Maximized)
                Cursor = cursors[ResDirection];
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);
            Win32Funcs.SendMessage(new WindowInteropHelper(AttachedWindow as Window).Handle, 0x112, (IntPtr)(61440 + resDirection), IntPtr.Zero);
        }

        static YT_SizeBorder() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_SizeBorder), new FrameworkPropertyMetadata(typeof(YT_SizeBorder)));
        }
    }
}
