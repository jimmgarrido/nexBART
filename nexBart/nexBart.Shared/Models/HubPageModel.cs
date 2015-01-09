using nexBart.DataModel;
using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace nexBart.Models
{
    public class HubPageModel
    {
        public static async void StationSelected(StationData selection, ScheduleGroup group)
        {
            Station tempStation = new Station(selection);
            tempStation.LinesList = await DeparturesHelper.GetDepartures(selection);
            //tempStation.LinesList[0].RouteColor[0] = new SolidColorBrush(Colors.Azure);
            //tempStation.Name = "efweefwefw";

            group.SetStation(tempStation);
      
        }
    }
}
