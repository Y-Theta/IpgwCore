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

        public static int ToInt(ColorD c) {
            return c.ToArgb();
        }

        public static int ToInt(Color c) {
            return (c.A << 24) + (c.R << 16) + (c.G << 8) + c.B;
        }
        public static Color ToColor(ColorD c) {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static ColorD ToColorD(Color s) {
            return ColorD.FromArgb(s.A, s.R, s.G, s.B);
        }

        public static SolidColorBrush ToColorBrush(ColorD c) {
            return new SolidColorBrush(Color.FromArgb(c.A, c.R, c.G, c.B));
        }

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

    internal class ColorNumConv: IValueConverter {
        private int _color;
    
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int cp;
            try { cp = System.Convert.ToInt32(value); }
            catch (Exception) { return 0; }
            if (parameter is null)
                return value;
            _color = cp;
            int a = cp >> 24 , r = (cp >> 16) & 0xff , g = (cp >> 8) & 0xff , b = cp & 0xff;
            if (a < 0)
                a = a + 256;
            switch (parameter.ToString())
            {
                case "Color":
                    return Color.FromArgb((Byte)a, (Byte)r, (Byte)g, (Byte)b);
                case "Brush":
                    return new SolidColorBrush(Color.FromArgb((Byte)a, (Byte)r, (Byte)g, (Byte)b));
                case "HEX":
                    return String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", a, r, g, b);
                case "A":
                    return a;
                case "R":
                    return r;
                case "G":
                    return g;
                case "B":
                    return b;
                default: return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            int _argb = 0;
            switch (parameter.ToString())
            {
                case "HEX":
                    String hex = value.ToString();
                    try { return Int32.Parse(hex.Substring(1), NumberStyles.HexNumber); }
                    catch (Exception) {
                        return _color;
                    }
                case "A":
                    try
                    {
                        _argb = (int)Math.Round((Double)value);
                        if (_argb < 256)
                            return (int)((_argb << 24) + (_color & 0x00ffffff));
                        else
                            return _color;
                    }
                    catch (Exception) {
                        return _color;
                    }
                case "R":
                    try
                    {
                        _argb = (int)Math.Round((Double)value);
                        if (_argb < 256)
                            return (int)((_argb << 16) + (_color & 0xff00ffff));
                        else
                            return _color;
                    }
                    catch (Exception) {
                        return _color;
                    }
                case "G":
                    try
                    {
                        _argb = (int)Math.Round((Double)value);
                        if (_argb < 256)
                            return (int)((_argb << 8) + (_color & 0xffff00ff));
                        else
                            return _color;
                    }
                    catch (Exception) {
                        return _color;
                    }
                case "B":
                    try
                    {
                        _argb = (int)Math.Round((Double)value);
                        if (_argb < 256)
                            return (int)(_argb + (_color & 0xffffff00));
                        else
                            return _color;
                    }
                    catch (Exception) {
                        return _color;
                    }
                default: return value;
            }
        }
    }

}
