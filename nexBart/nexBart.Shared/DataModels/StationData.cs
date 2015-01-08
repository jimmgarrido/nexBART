using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.DataModels
{
    public class StationData
    {
        public string Name { get; set; }
        private string abbrv;

        public StationData(string _name, string _abbrv)
        {
            Name = _name;
            abbrv = _abbrv;
        }
    }
}
