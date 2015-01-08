using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.DataModel
{
    public class Station
    {
        public string Name { get; set; }
        public List<Line> LinesList { get; set; }

        private string abbrv;

        private Dictionary<string, string> abbreviations = new Dictionary<string, string>()
        {
            {"12th St. Oakland City Center", "12th" }
        };

        public Station() { }

        public Station(string _name)
        {
            this.Name = _name;

            LinesList = new List<Line>();
        }
    }
}
