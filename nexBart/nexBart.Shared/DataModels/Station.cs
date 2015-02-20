using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.DataModels
{
    public class Station
    {
        private ObservableCollection<Line> _lines;

        public string Name { get; set; }
        public string Abbrv { get; set; }
        public ObservableCollection<Line> Lines 
        { 
            get 
            { 
                return _lines; 
            } 
            private set 
            { 
                _lines = value; 
            } 
        }

        public Station() { }

        public Station(StationData data)
        {
            Name = data.Name;
            Abbrv = data.Abbrv;
            Lines = new ObservableCollection<Line>();
        }

        public void AddLineList(ObservableCollection<Line> lines)
        {
            Lines = lines;
        }

        public StationData GetData()
        {
            return new StationData(Name, Abbrv);
        }
    }
}
