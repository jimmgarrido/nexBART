using nexBart.DataModel;
using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.Models
{
    public class SchedulesModel
    {
        public static List<StationData> stationList { get; set; }
        public ObservableCollection<Station> selectedStation { get; set; }

       public SchedulesModel()
       {
           selectedStation = new ObservableCollection<Station>();
           stationList = new List<StationData>()
           {
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
           //stationsList.Add(new StationData("12th St. Oakland City Center", "12th"));
           //stationsList.Add(new StationData("16th St. Mission", "16th"));
           //stationsList.Add( new StationData("19th St. Oakland", "19th"));
           //stationsList.Add( new StationData("24th St. Mission", "24th"));
           //stationsList.Add( new StationData("Ashby", "ashb"));
           //stationsList.Add( new StationData("Balboa Park", "balb"));
           //stationsList.Add( new StationData("Bay Fair", "bayf"));
           //stationsList.Add( new StationData("Castro Valley", "cast"));
           //stationsList.Add( new StationData("Civic Center", "civc"));
           //stationsList.Add( new StationData("Coliseum/Oakland Airport", "cols"));
           //stationsList.Add( new StationData("Colma", "colm"));
           //stationsList.Add( new StationData("Concord", "conc"));
           //stationsList.Add( new StationData("Daly City", "daly"));
           //stationsList.Add( new StationData("Downtown Berkeley", "dbrk"));
           //stationsList.Add( new StationData("Dublin/Pleasanton", "dubl"));
           //stationsList.Add( new StationData("El Cerrito del Norte", "deln"));
           //stationsList.Add( new StationData("El Cerrito Plaza", "plza"));
           //stationsList.Add( new StationData("Embarcadero ", "embr"));
           //stationsList.Add( new StationData("Fremont", "frmt"));
           //stationsList.Add( new StationData("Fruitvale ", "ftvl"));
           //stationsList.Add( new StationData("Glen Park", "glen"));
           //stationsList.Add( new StationData("Hayward", "hayw"));
           //stationsList.Add( new StationData("Lafayette", "lafy"));
           //stationsList.Add( new StationData("Lake Merritt", "lake"));
           //stationsList.Add( new StationData("MacArthur", "mcar"));
           //stationsList.Add( new StationData("Millbrae", "mlbr"));
           //stationsList.Add( new StationData("Montgomery St.", "mont"));
           //stationsList.Add( new StationData("North Berkeley", "nbrk"));
           //stationsList.Add( new StationData("North Concord/Martinez", "ncon"));
           //stationsList.Add( new StationData("Orinda", "orin"));
           //stationsList.Add( new StationData("Pittsburg/Bay Point", "pitt"));
           //stationsList.Add( new StationData("Pleasant Hill", "phil"));
           //stationsList.Add( new StationData("Powell St.", "powl"));
           //stationsList.Add( new StationData("Richmond", "rich"));
           //stationsList.Add( new StationData("Rockridge", "rock"));
           //stationsList.Add( new StationData("San Bruno", "sbrn"));
           //stationsList.Add( new StationData("San Francisco Int'l Airport", "sfia"));
           //stationsList.Add( new StationData("San Leandro", "sanl"));
           //stationsList.Add( new StationData("South Hayward", "shay"));
           //stationsList.Add( new StationData("South San Francisco", "ssan"));
           //stationsList.Add( new StationData("Union City", "ucty"));
           //stationsList.Add( new StationData("Walnut Creek", "wcrk"));
           //stationsList.Add( new StationData("West Dublin", "wdub"));
           //stationsList.Add( new StationData("West Oakland", "woak"));
       }

       public void SetStation(Station selection)
       {
           selectedStation.Clear();
           selectedStation.Add(selection);
       }

       public static async void StationSelected(StationData selection, SchedulesModel model)
       {
           Station tempStation = new Station(selection);

           //Add progress bar here

           tempStation.LinesList = await DeparturesHelper.GetDepartures(selection);
           model.SetStation(tempStation);           
       }
    }
}
