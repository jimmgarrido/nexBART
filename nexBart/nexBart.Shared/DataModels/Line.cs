﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace nexBart.DataModels
{
    public class Line
    {
        //private string[] _destinations;
        //private string[] _times;
        private Brush _lineColor;

        public string[] Destinations { get; set; }
        public string[] Times { get; set; }
        public string[] DirOneTimes { get; set; }
        public string[] DirTwoTimes { get; set; }

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
            RGBColor rgbColor;

            Destinations[0] = dest;
            if (LineColors.Colors.ContainsKey(color))
                rgbColor = LineColors.Colors[color];
            else
                rgbColor = LineColors.Colors["BLACK"];
            LineColor = new SolidColorBrush(Color.FromArgb(rgbColor.colorBytes[0], rgbColor.colorBytes[1], rgbColor.colorBytes[2], rgbColor.colorBytes[3]));
            colorName = color;
        }

        public void MakeTimesArray(int index)
        {
            string[] sep = {", "};

            if(index == 0)
            {
                if (Times[index] != null)
                {
                    //Times[index] = 
                    DirOneTimes = Times[index].Split(sep, StringSplitOptions.None);
                }
            }
            else
            {
                if (Times[index] != null)
                {
                    DirTwoTimes = Times[index].Split(sep, StringSplitOptions.None);
                }
            }
        }
    }
}
