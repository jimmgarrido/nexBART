using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.DataModel
{
    public class Station
    {
        public string Name { get; set; }
        public string Lines { get; set; }

        public Station(string _name, string _lines)
        {
            this.Name = _name;
            this.Lines = _lines;
        }
    }
}
