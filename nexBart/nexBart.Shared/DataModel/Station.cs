using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.DataModel
{
    public class Station
    {
        public string Name { get; set; }
        public List<Line> LinesList { get; set; }

        public int numLines;

        public Station(string _name)
        {
            this.Name = _name;

            LinesList = new List<Line>();
        }
    }
}
