using nexBart.DataModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.Models
{
    public class StationDetailModel
    {
        public ObservableCollection<Station> Selection = new ObservableCollection<Station>();

        public StationDetailModel() { }

        public StationDetailModel(Station station)
        {
            Selection.Add(station);
        }
    }
}
