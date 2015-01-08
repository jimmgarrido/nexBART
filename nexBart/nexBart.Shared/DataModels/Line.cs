using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace nexBart.DataModel
{
    public class Line
    {
        public string[] Destinations { get; set; }
        public string[] Times { get; set; }
        public Brush RouteColor { get; set; }

        private Dictionary<string, RGBColor> colors = new Dictionary<string, RGBColor>()
        {
            {"GREEN", new RGBColor(255, 51, 153, 51) },
            {"RED", new RGBColor(255, 255, 0, 0) },
            {"BLUE", new RGBColor(255, 0, 153, 204) },
            {"YELLOW", new RGBColor(255, 255, 255, 51) },
            {"ORANGE", new RGBColor(255, 255, 153, 51) }
        };

        public Line()
        {
            Destinations = new string[2];
            Times = new string[2];
        }

        public Line(string _dest)
        {
            Destinations = new string[2];
            Times = new string[2];

            RGBColor color = colors["GREEN"];
            RouteColor = new SolidColorBrush(Color.FromArgb(color.colorBytes[0], color.colorBytes[1], color.colorBytes[2], color.colorBytes[3]));
        }

        public void SetColor(string color )
        {

        }
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
