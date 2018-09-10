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
using SettingS = IpgwCore.Properties.Settings;
using IpgwCore.ViewModel;
using IpgwCore.Services.FormatServices;
using System.Windows;
using IpgwCore.Model.BasicModel;

namespace IpgwCore.View {
    /// <summary>
    /// 颜色转换 无参 ->Brush else ->Color
    /// </summary>
    public class ColorConv : IValueConverter {

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
    public class ColorNumConv : IValueConverter {
        public static Color ToColor(int cp) {
            int a = cp >> 24, r = (cp >> 16) & 0xff, g = (cp >> 8) & 0xff, b = cp & 0xff;
            if (a < 0)
                a = a + 256;
            return Color.FromArgb((Byte)a, (Byte)r, (Byte)g, (Byte)b);
        }

        public static ColorD ToColorD(int cp) {
            int a = cp >> 24, r = (cp >> 16) & 0xff, g = (cp >> 8) & 0xff, b = cp & 0xff;
            if (a < 0)
                a = a + 256;
            return ColorD.FromArgb((Byte)a, (Byte)r, (Byte)g, (Byte)b);
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
    /// 流量信息转换器
    /// </summary>
    public class FluxConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return (int)((double)value * 100);
            string[] str = parameter.ToString().Split('|');
            switch (str[0])
            {
                case "Path":
                    return PercentToCircle(GetFluxPercent((Flux)value) / 100, Int32.Parse(str[1]), Int32.Parse(str[2]));
                case "Per":
                    return GetFluxPercent((Flux)value);
                case "Used":
                    return GetUsed((Flux)value);
                case "Bal":
                    return GetBalance((Flux)value);
                case "Mon":
                    return GetMon((Flux)value);
                case "AllMon":
                    return GetAllMoney((Flux)value);
                case "Span":
                    return GetLastUpdate((Flux)value);
                case "ConT":
                    return (bool)value ? "Online" : "Offline";
                case "PathTest":
                    return PercentToCircle((double)value, Int32.Parse(str[1]), Int32.Parse(str[2]));
                default:
                    return (int)value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }


        public static string GetMon(Flux value) {
            if (value is null)
                return "未知";
            return String.Format("{0:###.##}  R", value.Balance);
        }

        public static string GetBalance(Flux flux) {
            if (flux is null)
                return "未知";
            return FluxFormat(GetFluxData(flux, false), FluxType.MB);
        }

        public static string GetUsed(Flux flux) {
            if (flux is null)
                return "未知";
            return FluxFormat(GetFluxData(flux, true), FluxType.MB);
        }

        /// <summary>
        /// 获取距离上次更新时间
        /// </summary>
        public static string GetLastUpdate(Flux flux) {
            if (flux is null)
                return "未知";
            DateTime time = DateTime.Now;
            TimeSpan span = time.Subtract(flux.InfoTime);
            if (span.TotalHours < 0.15)
                return "刚刚更新";
            else if (span.TotalHours < 0.5)
                return "半小时内";
            else if (span.TotalHours < 1)
                return "一小时内";
            return String.Format("{0:###.#}  H", span.TotalHours);
        }

        /// <summary>
        /// 获取本月累计充值的网费
        /// </summary>
        public static string GetAllMoney(Flux flux) {
            if (flux is null)
                return "未知";
            double use = GetFluxData(flux, true);
            double balance = flux.Balance;
            double all;
            if (SettingS.Default.Package == 0)
            {
                if (use > SettingS.Default.C1F * 1024)
                    all = (use - 1000 * SettingS.Default.C1F) / 1000 + balance;
                else
                    all = balance;
            }
            else
            {
                if (use > SettingS.Default.C2F * 1024)
                    all = (use - 1000 * SettingS.Default.C2F) / 1000 + balance;
                else
                    all = balance;
            }
            return String.Format("{0:###.##}  R", all);
        }

        /// <summary>
        /// 获取在线时间
        /// </summary>
        public static string GetTime(double sec) {
            return String.Format("{0:###}  H", sec / 3600);
        }

        /// <summary>
        /// 根据套餐获得已用流量百分比
        /// </summary>
        public static double GetFluxPercent(Flux IpgwInfo) {
            if (IpgwInfo is null)
                return 0;
            double used = GetFluxData(IpgwInfo, true);
            double balance = GetFluxData(IpgwInfo, false);
            double per = balance * 100 / (used + balance);
            double _out = Math.Round(per, 0, MidpointRounding.AwayFromZero);
            return _out;
        }

        /// <summary>
        /// 根据套餐获得已用/未用流量 M
        /// </summary>
        /// <param name="use">true 已使用 /false 未使用</param>
        public static double GetFluxData(Flux IpgwInfo, bool use) {
            try
            {
                if (use)
                    return IpgwInfo.FluxData;
                else
                {
                    if (SettingS.Default.Package == 0)
                    {
                        double all = IpgwInfo.Balance - SettingS.Default.C1P;
                        if (IpgwInfo.FluxData > SettingS.Default.C1F * 1024)
                        {
                            return all * 1000;
                        }
                        else
                        {
                            all = all * 1000 + 1000 * SettingS.Default.C1F;
                            return all - IpgwInfo.FluxData;
                        }
                    }
                    else
                    {
                        double all = IpgwInfo.Balance - SettingS.Default.C2P;
                        if (IpgwInfo.FluxData > SettingS.Default.C2F * 1024)
                        {
                            return all * 1000;
                        }
                        else
                        {
                            all = all * 1000 + 1000 * SettingS.Default.C2F;
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
        public static string FluxFormat(double value, FluxType type = FluxType.KB) {
            switch (type)
            {
                case FluxType.Bit:
                    value *= 8;
                    return FormatByte(value);
                case FluxType.B:
                    return FormatByte(value);
                case FluxType.KB:
                    if (value > 1000000)
                        return String.Format("{0:###.##}  G", value / 1000000.0);
                    else if (value > 1000)
                        return String.Format("{0:###.##}  M", value / 1000.0);
                    else
                        return String.Format("{0:###.##}  K", value);
                case FluxType.MB:
                    if (value > 1000)
                        return String.Format("{0:###.##}  G", value / 1000.0);
                    else
                        return String.Format("{0:###.##}  M", value);
                default: return "<0>";
            }

        }

        private static string FormatByte(double value) {
            if (value > 1000000000)
                return String.Format("{0:###.##}  G", value / 1000000000.0);
            else if (value > 1000000)
                return String.Format("{0:###.##}  M", value / 1000000.0);
            else if (value > 1000)
                return String.Format("{0:###.##}  K", value / 1000.0);
            else
                return String.Format("{0:###.##}  B", value);
        }


        /// <summary>
        /// 将百分比转换为圆形路径
        /// </summary>
        /// <param name="a">百分比 小数表示</param>
        /// <param name="cs">区域大小</param>
        /// <param name="r">半径</param>
        /// <returns></returns>
        public static string PercentToCircle(double a, int cs, int r) {
            string R = r.ToString();
            int center = cs / 2;
            string Center = center.ToString();
            string Gap = ((int)((cs - 2 * r) / 2)).ToString();
            var A = a * Math.PI * 3.6 / 1.80;
            var x = r * Math.Sin(A);
            var y = r * Math.Cos(A);
            x = center + x;
            y = center - y;
            if (a <= 0.50)
                return "M " + Center + "," + Gap + " A " + R + "," + R + ",0,0,1," + x.ToString() + "," + y.ToString();
            else if (a >= 1)
                return "M " + Center + "," + Gap + " A " + R + "," + R + ",0,0,1," + string.Format("{0:###.#}", center - 1) + "," + y.ToString();
            else
                return "M " + Center + "," + Gap + " A " + R + "," + R + ",0,1,1," + x.ToString() + "," + y.ToString();
        }
    }

    /// <summary>
    /// 将Slider的值进行放缩
    /// </summary>
    public class SliderConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            if (value is Double)
                return Math.Round((double)value * Double.Parse(parameter.ToString()), 0);
            else if (value is float)
                return Math.Round((float)value * Double.Parse(parameter.ToString()), 0);
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (double)value / Double.Parse(parameter.ToString());
        }
    }

    /// <summary>
    /// 对控件的位置进行修正
    /// </summary>
    public class OffsetConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return System.Convert.ToDouble(value) + System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 对Bool值进行转换，将true转换为para后的参数
    /// </summary>
    public class BoolConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            string[] str = parameter.ToString().Split('|');
            switch (str[0])
            {
                case "Visible":
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                case "Collapsed":
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                case "String":
                    return (bool)value ? str[1] : str[2];
                default: return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            string[] str = parameter.ToString().Split('|');
            switch (str[0])
            {
                case "Visible":
                    return ((Visibility)value).Equals(Visibility.Visible) ? true : false;
                case "Collapsed":
                    return ((Visibility)value).Equals(Visibility.Visible) ? false : false;
                default: return value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StringConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            switch (parameter.ToString())
            {
                case "Visible":
                    return value is null || value.ToString().Equals(String.Empty) ? Visibility.Visible : Visibility.Collapsed;
                case "Collapsed":
                    return value is null || value.ToString().Equals(String.Empty) ? Visibility.Collapsed : Visibility.Visible;
                default: return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DoubleConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            string[] str = parameter.ToString().Split('|');
            switch (str[0])
            {
                case "String":
                    return String.Format("{0:##.##}", System.Convert.ToDouble(value));
                default: return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            string[] str = parameter.ToString().Split('|');
            switch (str[0])
            {
                case "String":
                    try
                    {
                        return Double.Parse(value.ToString(), NumberStyles.Float);
                    }
                    catch (Exception) { return 11.8; }
                default: return value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PackageConv : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            switch (parameter.ToString())
            {
                case "V0":
                    return (int)value == 0 ? Visibility.Visible : Visibility.Collapsed;
                case "V1":
                    return (int)value == 1 ? Visibility.Visible : Visibility.Collapsed;
                case "B0":
                    return (int)value == 0 ? true : false;
                case "B1":
                    return (int)value == 1 ? true : false;
                case "P0":
                    return String.Format("{0:##}元{1:##}G", SettingS.Default.C1P, SettingS.Default.C1F);
                case "P1":
                    return String.Format("{0:##}元{1:##}G", SettingS.Default.C2P, SettingS.Default.C2F);
                default: return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is null)
                return value;
            switch (parameter.ToString())
            {
                case "B0":
                    return (bool)value ? 0 : 1;
                case "B1":
                    return (bool)value ? 1 : 0;

                default: return value;
            }
        }
    }
}
