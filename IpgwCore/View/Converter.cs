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
using IpgwCore.ViewModel;
using IpgwCore.Services.FormatServices;
using System.Windows;
using IpgwCore.Model.BasicModel;

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

    /// <summary>
    /// 颜色转换 
    /// int -> color brush hex
    /// color brush hex -> int
    /// </summary>
    internal class ColorNumConv : IValueConverter {
        public static Color ToColor(int cp) {
            int a = cp >> 24, r = (cp >> 16) & 0xff, g = (cp >> 8) & 0xff, b = cp & 0xff;
            if (a < 0)
                a = a + 256;
            return Color.FromArgb((Byte)a, (Byte)r, (Byte)g, (Byte)b);
        }

        public static SolidColorBrush ToSolidColorBrush(int cp) {
            int a = cp >> 24, r = (cp >> 16) & 0xff, g = (cp >> 8) & 0xff, b = cp & 0xff;
            if (a < 0)
                a = a + 256;
            return new SolidColorBrush(Color.FromArgb((Byte)a, (Byte)r, (Byte)g, (Byte)b));
        }

        private int _color;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int cp;
            try { cp = System.Convert.ToInt32(value); }
            catch (Exception) { return 0; }
            if (parameter is null)
                return value;
            _color = cp;
            int a = cp >> 24, r = (cp >> 16) & 0xff, g = (cp >> 8) & 0xff, b = cp & 0xff;
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
                case "AS":
                    return a;
                case "RS":
                    return r;
                case "GS":
                    return g;
                case "BS":
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
                    catch (Exception)
                    {
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
                    catch (Exception)
                    {
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
                    catch (Exception)
                    {
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
                    catch (Exception)
                    {
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
                    catch (Exception)
                    {
                        return _color;
                    }
                case "AS":
                    try
                    {
                        _argb = Int32.Parse(value.ToString());
                        if (_argb < 256)
                            return (int)((_argb << 24) + (_color & 0x00ffffff));
                        else
                            return _color;
                    }
                    catch (Exception)
                    {
                        return _color;
                    }
                case "RS":
                    try
                    {
                        _argb = Int32.Parse(value.ToString());
                        if (_argb < 256)
                            return (int)((_argb << 16) + (_color & 0xff00ffff));
                        else
                            return _color;
                    }
                    catch (Exception)
                    {
                        return _color;
                    }
                case "GS":
                    try
                    {
                        _argb = Int32.Parse(value.ToString());
                        if (_argb < 256)
                            return (int)((_argb << 8) + (_color & 0xffff00ff));
                        else
                            return _color;
                    }
                    catch (Exception)
                    {
                        return _color;
                    }
                case "BS":
                    try
                    {
                        _argb = Int32.Parse(value.ToString());
                        if (_argb < 256)
                            return (int)(_argb + (_color & 0xffffff00));
                        else
                            return _color;
                    }
                    catch (Exception)
                    {
                        return _color;
                    }
                default: return value;
            }
        }
    }

    /// <summary>
    /// 对控件的位置进行修正
    /// </summary>
    internal class OffsetConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToDouble(value) + System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 流量信息转换器
    /// </summary>
    internal class FluxConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return (int)((double)value * 100);
            switch (parameter.ToString())
            {
                case "Path":
                    return PercentToCircle(1 - GetFluxPercent((Flux)value));
                case "Per":
                    return (int)(100 - GetFluxPercent((Flux)value) * 100);
                case "Used":
                    return GetUsed((Flux)value);
                case "Bal":
                    return GetBalance((Flux)value);
                case "ConT":
                    return (bool)value ? "Online" : "Offline";
                case "ConV":
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                case "ConNV":
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                default:
                    return (int)value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        private static string GetBalance(Flux flux) {
            return FluxFormat(GetFluxData(flux,false));
        }

        private static string GetUsed(Flux flux) {
            return FluxFormat(GetFluxData(flux,true));
        }

        /// <summary>
        /// 根据套餐获得已用流量百分比
        /// </summary>
        public double GetFluxPercent(Flux IpgwInfo) {
            try
            {
                if (Properties.Settings.Default.Package == 1)
                {
                    double all = IpgwInfo.Balance - 20;
                    if (IpgwInfo.FluxData > 60 * 1024)
                        return IpgwInfo.FluxData / (IpgwInfo.FluxData + all * 1000);
                    else
                    {
                        all = all * 1000 + 60000;
                        return IpgwInfo.FluxData / all;
                    }
                }
                else
                {
                    double all = IpgwInfo.Balance - 15;
                    if (IpgwInfo.FluxData > 27 * 1024)
                        return IpgwInfo.FluxData / (IpgwInfo.FluxData + all * 1000);
                    else
                    {
                        all = all * 1000 + 27000;
                        return IpgwInfo.FluxData / all;
                    }
                }
            }
            catch (Exception)
            {
                //TODO:
                return 0;
            }
        }

        /// <summary>
        /// 根据套餐获得已用/未用流量
        /// </summary>
        /// <param name="use">true 已使用 /false 未使用</param>
        public static double GetFluxData(Flux IpgwInfo, bool use) {
            try
            {
                if (use)
                    return IpgwInfo.FluxData;
                else
                {
                    if (Properties.Settings.Default.Package == 1)
                    {
                        double all = IpgwInfo.Balance - 20;
                        if (IpgwInfo.FluxData > 60 * 1024)
                        {
                            return all * 1000;
                        }
                        else
                        {
                            all = all * 1000 + 60000;
                            return all - IpgwInfo.FluxData;
                        }
                    }
                    else
                    {
                        double all = IpgwInfo.Balance - 15;
                        if (IpgwInfo.FluxData > 27 * 1024)
                        {
                            return all * 1000;
                        }
                        else
                        {
                            all = all * 1000 + 27000;
                            return all - IpgwInfo.FluxData;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //TODO:
                return 0;
            }
        }

        /// <summary>
        /// 将流量表示为 M G 的形式
        /// </summary>
        private static string FluxFormat(double value) {
            if (value > 1000)
                return String.Format("{0:###.#} G", value / 1000.0);
            else
                return String.Format("{0:###.#} M", value);
        }

        /// <summary>
        /// 将百分比转换为圆形路径
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private static string PercentToCircle(double a) {
            var A = a * Math.PI * 3.6 / 1.80;
            var x = 30 * Math.Sin(A);
            var y = 30 * Math.Cos(A);
            x = 36 + x;
            y = 36 - y;
            if (a <= 0.50)
                return "M 36,6 A 30,30,0,0,1," + x.ToString() + "," + y.ToString();
            else if (a == 1)
                return "M 36,6 A 30,30,0,1,1,35.9,6";
            else
                return "M 36,6 A 30,30,0,1,1," + x.ToString() + "," + y.ToString();
        }
    }

    /// <summary>
    /// 将Slider的值进行放缩
    /// </summary>
    internal class SliderConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (double)value * Double.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (double)value / Double.Parse(parameter.ToString());
        }
    }


}
