using nexBart.DataModels;
using nexBart.Helpers;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.ViewModels
{
    public class SchedulesModel : INotifyPropertyChanged
    {
        private Station _selectedStation;

        public List<Station> StationList { get; set; }
        
        public Station SelectedStation
        {
            get
            {
                return _selectedStation;
            }
            private set
            {
                _selectedStation = value;
                NotifyPropertyChanged("SelectedStation");
            }
        }

       public SchedulesModel()
       {
           StationList = new List<Station>()
           {
               { new Station("12th St. Oakland", "12th")},
               { new Station("16th St. Mission", "16th")},
               { new Station("19th St. Oakland", "19th")},
               { new Station("24th St. Mission", "24th")},
               { new Station("Ashby", "ashb")},
               { new Station("Balboa Park", "balb")},
               { new Station("Bay Fair", "bayf")},
               { new Station("Castro Valley", "cast")},
               { new Station("Civic Center", "civc")},
               { new Station("Coliseum/Oakland Airport", "cols")},
               { new Station("Colma", "colm")},
               { new Station("Concord", "conc")},
               { new Station("Daly City", "daly")},
               { new Station("Downtown Berkeley", "dbrk")},
               { new Station("Dublin/Pleasanton", "dubl")},
               { new Station("El Cerrito del Norte", "deln")},
               { new Station("El Cerrito Plaza", "plza")},
               { new Station("Embarcadero ", "embr")},
               { new Station("Fremont", "frmt")},
               { new Station("Fruitvale ", "ftvl")},
               { new Station("Glen Park", "glen")},
               { new Station("Hayward", "hayw")},
               { new Station("Lafayette", "lafy")},
               { new Station("Lake Merritt", "lake")},
               { new Station("MacArthur", "mcar")},
               { new Station("Millbrae", "mlbr")},
               { new Station("Montgomery St.", "mont")},
               { new Station("North Berkeley", "nbrk")},
               { new Station("North Concord/Martinez", "ncon")},
               { new Station("Orinda", "orin")},
               { new Station("Pittsburg/Bay Point", "pitt")},
               { new Station("Pleasant Hill", "phil")},
               { new Station("Powell St.", "powl")},
               { new Station("Richmond", "rich")},
               { new Station("Rockridge", "rock")},
               { new Station("San Bruno", "sbrn")},
               { new Station("San Francisco Int'l Airport", "sfia")},
               { new Station("San Leandro", "sanl")},
               { new Station("South Hayward", "shay")},
               { new Station("South San Francisco", "ssan")},
               { new Station("Union City", "ucty")},
               { new Station("Walnut Creek", "wcrk")},
               { new Station("West Dublin", "wdub")},
               { new Station("West Oakland", "woak")}
           };
       }

       public async Task StationSelected(Station selection)
       {
           selection.AddLineList(await WebHelper.GetPredictions(selection));
            SelectedStation = selection;
       }

        #region INotify Methods
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
