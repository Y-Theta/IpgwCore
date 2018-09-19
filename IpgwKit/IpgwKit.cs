using NEUH_Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using Drawing = System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YControls;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace IpgwKit
{
    [Export(typeof(INEUHCoreContract))]
    public class IpgwPlugin : CoreContractBase {
        private IntPtr _ico = IntPtr.Zero;

        public override void Run(UseCase c) {
            YT_AreaIcon Icon = new YT_AreaIcon();
            Icon.Areaicon = Drawicon();
        }

        private Icon Drawicon() {
            int size = 36;
            Drawing.Image bufferedimage;
            if (_ico == IntPtr.Zero)
                bufferedimage = new Bitmap(size, size, Drawing.Imaging.PixelFormat.Format32bppArgb);
            else
                bufferedimage = Bitmap.FromHicon(_ico);

            Graphics g = Graphics.FromImage(bufferedimage);
            g.Clear(Color.FromArgb(0, 255, 255, 255));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            Pen pen = new Pen(Color.AliceBlue, 1f);
            g.FillRectangle(pen.Brush,new RectangleF(0,0,24,24));
            _ico = (bufferedimage as Bitmap).GetHicon();

            bufferedimage.Dispose();
            g.Dispose();

            return Icon.FromHandle(_ico);
        }

        public override void Run(UseCase c, out object t) {
            t = null;
        }

        public IpgwPlugin() {
            Name = "IpgwCore";
            Author = "Y_Theta";
            Edition = "1.0.0.0";
            Description = "use to monite ipgw flux";
        }
    }
}
