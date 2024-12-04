using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACOMv2.Common
{
    public class ColorHelper
    {

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


    }

}
