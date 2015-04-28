using nexBart.DataModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.Models
{
    public class StationDetailModel
    {
        //public ObservableCollection<Station> Selection = new ObservableCollection<Station>();
        public Station Selection;

        public StationDetailModel() { }

        public StationDetailModel(Station station)
        {
            Selection = station;
            //Selection.Add(station);

            foreach(Line l in Selection.Lines)
            {
                l.MakeTimesArray(0);
                l.MakeTimesArray(1);
            }
        }
    }
}
