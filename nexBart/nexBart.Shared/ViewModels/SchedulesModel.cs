using nexBart.DataModels;
using nexBart.Helpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.ViewModels
{
    public class SchedulesModel
    {
        private static List<StationData> _stationList;
        private ObservableCollection<Station> _selectedStation;

        public static List<StationData> StationList
        {
            get
            {
                return _stationList;
            }
            private set
            {
                _stationList = value;
            }
        }
        public ObservableCollection<Station> SelectedStation
        {
            get
            {
                return _selectedStation;
            }
            private set
            {
                _selectedStation = value;
            }
        }
        public string ButtonText { get; set; }

       public SchedulesModel()
       {
           SelectedStation = new ObservableCollection<Station>();
           ButtonText = "Test Text";
           StationList = new List<StationData>()
           {
               { new StationData("12th St. Oakland", "12th")},
               { new StationData("16th St. Mission", "16th")},
               { new StationData("19th St. Oakland", "19th")},
               { new StationData("24th St. Mission", "24th")},
               { new StationData("Ashby", "ashb")},
               { new StationData("Balboa Park", "balb")},
               { new StationData("Bay Fair", "bayf")},
               { new StationData("Castro Valley", "cast")},
               { new StationData("Civic Center", "civc")},
               { new StationData("Coliseum/Oakland Airport", "cols")},
               { new StationData("Colma", "colm")},
               { new StationData("Concord", "conc")},
               { new StationData("Daly City", "daly")},
               { new StationData("Downtown Berkeley", "dbrk")},
               { new StationData("Dublin/Pleasanton", "dubl")},
               { new StationData("El Cerrito del Norte", "deln")},
               { new StationData("El Cerrito Plaza", "plza")},
               { new StationData("Embarcadero ", "embr")},
               { new StationData("Fremont", "frmt")},
               { new StationData("Fruitvale ", "ftvl")},
               { new StationData("Glen Park", "glen")},
               { new StationData("Hayward", "hayw")},
               { new StationData("Lafayette", "lafy")},
               { new StationData("Lake Merritt", "lake")},
               { new StationData("MacArthur", "mcar")},
               { new StationData("Millbrae", "mlbr")},
               { new StationData("Montgomery St.", "mont")},
               { new StationData("North Berkeley", "nbrk")},
               { new StationData("North Concord/Martinez", "ncon")},
               { new StationData("Orinda", "orin")},
               { new StationData("Pittsburg/Bay Point", "pitt")},
               { new StationData("Pleasant Hill", "phil")},
               { new StationData("Powell St.", "powl")},
               { new StationData("Richmond", "rich")},
               { new StationData("Rockridge", "rock")},
               { new StationData("San Bruno", "sbrn")},
               { new StationData("San Francisco Int'l Airport", "sfia")},
               { new StationData("San Leandro", "sanl")},
               { new StationData("South Hayward", "shay")},
               { new StationData("South San Francisco", "ssan")},
               { new StationData("Union City", "ucty")},
               { new StationData("Walnut Creek", "wcrk")},
               { new StationData("West Dublin", "wdub")},
               { new StationData("West Oakland", "woak")}
           };
       }

       public async Task StationSelected(StationData selection)
       {
           Station tempStation = new Station(selection);

           tempStation.AddLineList(await WebHelper.GetPredictions(selection));

           SelectedStation.Clear();
           SelectedStation.Add(tempStation);
       }
    }
}
