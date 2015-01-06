using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;

namespace nexBart.DataModel
{
    public class Line
    {
        public string[] Destinations { get; set; }
        public string[] Times { get; set; }
        public Color RouteColor;

        public Line()
        {
            Destinations = new string[2];
            Times = new string[2];
        }

        public Line(string _dest)
        {
            Destinations = new string[2];
            Times = new string[2];

            Destinations[0] = _dest;
        }

        public void SetColor(string color)
        {
            //RouteColor = 
        }
    }
}
