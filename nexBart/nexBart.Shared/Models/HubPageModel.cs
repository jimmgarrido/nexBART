using nexBart.DataModel;
using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.Models
{
    public class HubPageModel
    {
        public static void StationSelected(StationData selection, ref ScheduleGroup group)
        {
            group.SetStation(new Station(selection.Name));
            DeparturesHelper.GetDepartures(selection);
        }
    }
}
