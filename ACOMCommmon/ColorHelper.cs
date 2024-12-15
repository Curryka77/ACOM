using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace ACOMCommmon;
public class ColorHelper
{
    public readonly static string[] materialDesignColors = {
    "#e74c3c", "#7d3c98", "#2e86c1", "#138d75", "#e8eaf6", "#28b463", "#f1c40f","#ba4a00","#2e4053"
};

    static int cnt = 0;

    /// <summary>
    /// 自动返回一个颜色
    /// </summary>
    /// <returns></returns>
    public static string AutoGetColor()
    {
        cnt++;
        return materialDesignColors[(cnt + 2) % materialDesignColors.Length];
    }
    public static string AutoGetColor(int id)
    {
        return materialDesignColors[(id+2) % materialDesignColors.Length];
    }
    static Windows.UI.Color ColorFromHSV(double hue, double saturation, double value)
    {
        double chroma = value * saturation;
        double huePrime = hue / 60.0;
        double x = chroma * (1 - Math.Abs(huePrime % 2 - 1));

        double red = 0, green = 0, blue = 0;

        if (huePrime >= 0 && huePrime <= 1)
        {
            red = chroma;
            green = x;
        }
        else if (huePrime > 1 && huePrime <= 2)
        {
            red = x;
            green = chroma;
        }
        else if (huePrime > 2 && huePrime <= 3)
        {
            green = chroma;
            blue = x;
        }
        else if (huePrime > 3 && huePrime <= 4)
        {
            green = x;
            blue = chroma;
        }
        else if (huePrime > 4 && huePrime <= 5)
        {
            red = x;
            blue = chroma;
        }
        else if (huePrime > 5 && huePrime <= 6)
        {
            red = chroma;
            blue = x;
        }

        double m = value - chroma;

        red += m;
        green += m;
        blue += m;

        byte redByte = (byte)(red * 255);
        byte greenByte = (byte)(green * 255);
        byte blueByte = (byte)(blue * 255);

        return Windows.UI.Color.FromArgb(255, redByte, greenByte, blueByte);
    }

    static public Windows.UI.Color GenerateDistinctColor(int number)
    {
        if (number < 0 || number > 127)
        {
            number = 127;
        }

        double hue = (number * 2.83) % 360; // 将数字映射到色轮上

        return ColorFromHSV(hue, 1, 1);
    }

    public static SolidColorBrush ConvertHexToSolidColorBrush(string hexColor)
    {
        if (string.IsNullOrEmpty(hexColor))
        {
            throw new ArgumentException("hexColor 不能为空", nameof(hexColor));
        }

        // 移除前缀 #
        if (hexColor.StartsWith("#"))
        {
            hexColor = hexColor.Substring(1);
        }

        byte a = 255; // 默认不透明
        byte r = 0;
        byte g = 0;
        byte b = 0;

        if (hexColor.Length == 8) // ARGB
        {
            a = byte.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
            r = byte.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
            g = byte.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
            b = byte.Parse(hexColor.Substring(6, 2), NumberStyles.HexNumber);
        }
        else if (hexColor.Length == 6) // RGB
        {
            r = byte.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
            g = byte.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
            b = byte.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
        }
        else
        {
            throw new ArgumentException("hexColor 格式不正确", nameof(hexColor));
        }

        return new SolidColorBrush(Color.FromArgb(a, r, g, b));
    }

    //public static SolidColorBrush ConvertHexToSolidColorBrush(string hexColor)
    //{
    //    if (string.IsNullOrEmpty(hexColor))
    //    {
    //        throw new ArgumentException("hexColor 不能为空", nameof(hexColor));
    //    }

    //    // 移除前缀 #
    //    if (hexColor.StartsWith("#"))
    //    {
    //        hexColor = hexColor.Substring(1);
    //    }

    //    byte a = 255; // 默认不透明
    //    byte r = 0;
    //    byte g = 0;
    //    byte b = 0;

    //    if (hexColor.Length == 8) // ARGB
    //    {
    //        a = byte.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
    //        r = byte.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
    //        g = byte.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
    //        b = byte.Parse(hexColor.Substring(6, 2), NumberStyles.HexNumber);
    //    }
    //    else if (hexColor.Length == 6) // RGB
    //    {
    //        r = byte.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
    //        g = byte.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
    //        b = byte.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
    //    }
    //    else
    //    {
    //        throw new ArgumentException("hexColor 格式不正确", nameof(hexColor));
    //    }

    //    return new SolidColorBrush(Color.FromArgb(a, r, g, b));
    //}

    public static string ConvertSolidColorBrushToHex(SolidColorBrush brush)
    {
        Color color = brush.Color;
        return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";

    }
}

