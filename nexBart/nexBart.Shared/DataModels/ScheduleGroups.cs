using nexBart.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.DataModel
{
    public class ScheduleGroup
    {
        public List<StationData> stationList { get; set; }
        public ObservableCollection<string> Name { get; set; }
        public ObservableCollection<Station> selectedStation { get; set; }

        public ScheduleGroup()
        {
            stationList = new List<StationData>();
            selectedStation = new ObservableCollection<Station>();
            //selectedStation.Add(new Station());

            stationList.Add(new StationData("12th St. Oakland City Center", "12th"));
            stationList.Add(new StationData("16th St. Mission", "16th"));
            stationList.Add( new StationData("19th St. Oakland", "19th"));
            stationList.Add( new StationData("24th St. Mission", "24th"));
            stationList.Add( new StationData("Ashby", "ashb"));
            stationList.Add( new StationData("Balboa Park", "balb"));
            stationList.Add( new StationData("Bay Fair", "bayf"));
            stationList.Add( new StationData("Castro Valley", "cast"));
            stationList.Add( new StationData("Civic Center", "civc"));
            stationList.Add( new StationData("Coliseum/Oakland Airport", "cols"));
            stationList.Add( new StationData("Colma", "colm"));
            stationList.Add( new StationData("Concord", "conc"));
            stationList.Add( new StationData("Daly City", "daly"));
            stationList.Add( new StationData("Downtown Berkeley", "dbrk"));
            stationList.Add( new StationData("Dublin/Pleasanton", "dubl"));
            stationList.Add( new StationData("El Cerrito del Norte", "deln"));
            stationList.Add( new StationData("El Cerrito Plaza", "plza"));
            stationList.Add( new StationData("Embarcadero ", "embr"));
            stationList.Add( new StationData("Fremont", "frmt"));
            stationList.Add( new StationData("Fruitvale ", "ftvl"));
            stationList.Add( new StationData("Glen Park", "glen"));
            stationList.Add( new StationData("Hayward", "hayw"));
            stationList.Add( new StationData("Lafayette", "lafy"));
            stationList.Add( new StationData("Lake Merritt", "lake"));
            stationList.Add( new StationData("MacArthur", "mcar"));
            stationList.Add( new StationData("Millbrae", "mlbr"));
            stationList.Add( new StationData("Montgomery St.", "mont"));
            stationList.Add( new StationData("North Berkeley", "nbrk"));
            stationList.Add( new StationData("North Concord/Martinez", "ncon"));
            stationList.Add( new StationData("Orinda", "orin"));
            stationList.Add( new StationData("Pittsburg/Bay Point", "pitt"));
            stationList.Add( new StationData("Pleasant Hill", "phil"));
            stationList.Add( new StationData("Powell St.", "powl"));
            stationList.Add( new StationData("Richmond", "rich"));
            stationList.Add( new StationData("Rockridge", "rock"));
            stationList.Add( new StationData("San Bruno", "sbrn"));
            stationList.Add( new StationData("San Francisco Int'l Airport", "sfia"));
            stationList.Add( new StationData("San Leandro", "sanl"));
            stationList.Add( new StationData("South Hayward", "shay"));
            stationList.Add( new StationData("South San Francisco", "ssan"));
            stationList.Add( new StationData("Union City", "ucty"));
            stationList.Add( new StationData("Walnut Creek", "wcrk"));
            stationList.Add( new StationData("West Dublin", "wdub"));
            stationList.Add( new StationData("West Oakland", "woak"));


            //{ new StationData("16th St. Mission", "16th")},
            //{ new StationData("19th St. Oakland", "19th")},
            //{ new StationData("24th St. Mission", "24th")},
            //{ new StationData("Ashby", "ashb")},
            //{ new StationData("Balboa Park", "balb")},
            //{ new StationData("Bay Fair", "bayf")},
            //{ new StationData("Castro Valley", "cast")},
            //{ new StationData("Civic Center", "civc")},
            //{ new StationData("Coliseum/Oakland Airport", "cols")},
            //{ new StationData("Colma", "colm")},
            //{ new StationData("Concord", "conc")},
            //{ new StationData("Daly City", "daly")},
            //{ new StationData("Downtown Berkeley", "dbrk")},
            //{ new StationData("Dublin/Pleasanton", "dubl")},
            //{ new StationData("El Cerrito del Norte", "deln")},
            //{ new StationData("El Cerrito Plaza", "plza")},
            //{ new StationData("Embarcadero ", "embr")},
            //{ new StationData("Fremont", "frmt")},
            //{ new StationData("Fruitvale ", "ftvl")},
            //{ new StationData("Glen Park", "glen")},
            //{ new StationData("Hayward", "hayw")},
            //{ new StationData("Lafayette", "lafy")},
            //{ new StationData("Lake Merritt", "lake")},
            //{ new StationData("MacArthur", "mcar")},
            //{ new StationData("Millbrae", "mlbr")},
            //{ new StationData("Montgomery St.", "mont")},
            //{ new StationData("North Berkeley", "nbrk")},
            //{ new StationData("North Concord/Martinez", "ncon")},
            //{ new StationData("Orinda", "orin")},
            //{ new StationData("Pittsburg/Bay Point", "pitt")},
            //{ new StationData("Pleasant Hill", "phil")},
            //{ new StationData("Powell St.", "powl")},
            //{ new StationData("Richmond", "rich")},
            //{ new StationData("Rockridge", "rock")},
            //{ new StationData("San Bruno", "sbrn")},
            //{ new StationData("San Francisco Int'l Airport", "sfia")},
            //{ new StationData("San Leandro", "sanl")},
            //{ new StationData("South Hayward", "shay")},
            //{ new StationData("South San Francisco", "ssan")},
            //{ new StationData("Union City", "ucty")},
            //{ new StationData("Walnut Creek", "wcrk")},
            //{ new StationData("West Dublin", "wdub")},
            //{ new StationData("West Oakland", "woak")}
        }

        public void SetStation(Station selection)
        {
            selectedStation.Clear();
            selectedStation.Add(selection);
        }
    }
}
