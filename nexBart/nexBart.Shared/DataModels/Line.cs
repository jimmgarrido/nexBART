using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace nexBart.DataModels
{
    public class Line
    {
        private string[] _destinations;
        private string[] _times;
        private Brush _lineColor;

        public string[] Destinations 
        {
            get
            {
                return _destinations;
            }
            private set
            {
                _destinations = value;
            }
        }
        public string[] Times
        {
            get
            {
                return _times;
            }
            private set
            {
                _times = value;
            }
        }
        public Brush LineColor
        {
            get
            {
                return _lineColor;
            }
            private set
            {
                _lineColor = value;
            }
        }
        public string colorName;

        public Line()
        {
            Destinations = new string[2];
            Times = new string[2];
        }

        public Line(string dest, string color)
        {
            Destinations = new string[2];
            Times = new string[2];

            Destinations[0] = dest;
            RGBColor rgbColor = LineColors.Colors[color];
            LineColor = new SolidColorBrush(Color.FromArgb(rgbColor.colorBytes[0], rgbColor.colorBytes[1], rgbColor.colorBytes[2], rgbColor.colorBytes[3]));
            colorName = color;
        }
    }
}
