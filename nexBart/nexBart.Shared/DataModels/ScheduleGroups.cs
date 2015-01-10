using nexBart.DataModels;
using nexBart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.DataModel
{
    public class ScheduleGroup
    {
        public List<StationData> stationsList { get; set; }
        public ObservableCollection<Station> selectedStation { get; set; }

        public ScheduleGroup()
        {
            //stationsList = SchedulesModel.GetStationList();
            selectedStation = new ObservableCollection<Station>();  
        }

        public void SetStation(Station selection)
        {
            selectedStation.Clear();
            selectedStation.Add(selection);
        }
    }
}
