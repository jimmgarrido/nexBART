using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.DataModels
{
    class LineColors
    {
        public static Dictionary<string, RGBColor> Colors = new Dictionary<string, RGBColor>()
        {
            {"GREEN", new RGBColor(255, 51, 153, 51) },
            {"RED", new RGBColor(255, 255, 0, 0) },
            {"BLUE", new RGBColor(255, 0, 153, 204) },
            {"YELLOW", new RGBColor(255, 255, 255, 51) },
            {"ORANGE", new RGBColor(255, 255, 153, 51) },
            {"WHITE", new RGBColor(255, 255, 255, 255) },
            {"BLACK", new RGBColor(255, 0, 0, 0)}
        };
    }

    class RGBColor
    {
        public byte[] colorBytes = new byte[4];

        public RGBColor(byte a, byte r, byte g, byte b)
        {
            colorBytes[0] = a;
            colorBytes[1] = r;
            colorBytes[2] = g;
            colorBytes[3] = b;
        }
    }
}
