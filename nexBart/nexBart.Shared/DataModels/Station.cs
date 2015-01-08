using nexBart.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.DataModel
{
    public class Station
    {
        public string Name { get; set; }
        public ObservableCollection<Line> LinesList { get; set; }

        public string abbrv;

        private Dictionary<string, string> abbreviations = new Dictionary<string, string>()
        {
            {"12th St. Oakland City Center", "12th" }
        };

        public Station() { }

        public Station(StationData _station)
        {
            this.Name = _station.Name;
            this.abbrv = _station.abbrv;
            LinesList = new ObservableCollection<Line>();
        }
    }
}
