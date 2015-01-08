using nexBart.DataModel;
using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.Models
{
    public class HubPageModel
    {
        public static async void StationSelected(StationData selection, ScheduleGroup group)
        {
            group.SetStation(new Station(selection));

            group.selectedStation[0].LinesList = await DeparturesHelper.GetDepartures(selection);       
        }
    }
}
