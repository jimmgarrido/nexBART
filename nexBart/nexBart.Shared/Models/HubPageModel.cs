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
        public static async void StationSelected(StationData selection, SchedulesModel model)
        {
            Station tempStation = new Station(selection);
            tempStation.LinesList = await DeparturesHelper.GetDepartures(selection);

            model.SetStation(tempStation);
        }
    }
}
