using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Drawing;
using ColorD = System.Drawing.Color;
using Color = System.Windows.Media.Color;

namespace IpgwCore.View {
    /// <summary>
    /// 颜色转换 无参 ->Brush else ->Color
    /// </summary>
    internal class ColorConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ColorD c = (ColorD)value;
            if (parameter is null)
                return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
            else
                return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
            {
                Color s = ((SolidColorBrush)value).Color;
                return ColorD.FromArgb(s.A, s.R, s.G, s.B);
            }
            else
            {
                Color s = (Color)value;
                return ColorD.FromArgb(s.A, s.R, s.G, s.B);
            }
        }
    }

}
