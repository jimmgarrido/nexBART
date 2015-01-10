using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.DataModels
{
    public class Station
    {
        public string Name { get; set; }
        public ObservableCollection<Line> LinesList { get; set; }

        public string abbrv;

        public Station() { }

        public Station(StationData _station)
        {
            this.Name = _station.Name;
            this.abbrv = _station.Abbrv;
            LinesList = new ObservableCollection<Line>();
        }
    }
}
