using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace nexBart.DataModels
{
    public class Line
    {
        public RGBColor LineColor { get; set; }
        public Destination[] Destinations { get; set; }

        public string colorName;

        public Line()
        {
            Destinations = new Destination[2];
        }

        public Line(string color)
        {
            //RGBColor rgbColor;
            Destinations = new Destination[2];

            //Destinations[0] = new Destination(dest);

            if (LineColors.Colors.ContainsKey(color))
                LineColor = LineColors.Colors[color];
            else
                LineColor = LineColors.Colors["BLACK"];

            //LineColor =  new SolidColorBrush(Color.FromArgb(rgbColor.colorBytes[0], rgbColor.colorBytes[1], rgbColor.colorBytes[2], rgbColor.colorBytes[3]));
            colorName = color;
        }
    }
}
