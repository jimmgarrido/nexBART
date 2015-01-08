using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.DataModel
{
    public class StationGroup
    {
        public static ObservableCollection<Station> StationItems = new ObservableCollection<Station>();

        public StationGroup()
        {

        }

        public static ObservableCollection<Station> GetStations()
        {
            return StationItems;
        }
    }
}
