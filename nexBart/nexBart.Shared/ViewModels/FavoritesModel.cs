using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;

namespace nexBart.ViewModels
{
    public class FavoritesModel : INotifyPropertyChanged
    {
        private string _noFavsText;

        public ObservableCollection<Station> FavoriteStations {get; set;}
        public string NoFavsText
        {
            get
            {
                return _noFavsText;
            }
            set
            {
                _noFavsText = value;
                NotifyPropertyChanged("NoFavsText");
            }
        }

        public FavoritesModel()
        {
            DatabaseHelper.FavoritesChanged += LoadFavorites;
            FavoriteStations = new ObservableCollection<Station>();
        }

        public async Task LoadFavorites()
        {
            var favorites = await DatabaseHelper.GetFavorites();

            if (favorites.Any())
            {
                while (FavoriteStations.Any())
                {
                    FavoriteStations.RemoveAt(FavoriteStations.Count - 1);
                }

                foreach (Station s in favorites)
                {
                    var lines = await WebHelper.GetPredictions(s);
                    s.AddLineList(lines);
                    //FavoriteStations.Add(s);
                }

                foreach (Station x in favorites)
                {
                    FavoriteStations.Add(x);
                }

                NoFavsText = "";
            }
            else
            {
                NoFavsText = "No favorites yet! Swipe right to select a station and favorite it.";
            }
        }

        public async Task AddFavorite(Station favorite)
        {
            await DatabaseHelper.AddFavorite(favorite);
        }

        public async Task RemoveFavorite(Station favorite)
        {
            await DatabaseHelper.RemoveFavorite(favorite);
        }

        public async Task CheckFavoritesDB()
        {
            await DatabaseHelper.CheckDB();
        }

        public bool IsFavorite(Station selection)
        {
            Station temp;

            if ((FavoriteStations.Any(s => s.Name == selection.Name)))
            {
                temp = FavoriteStations.ToList().Find(st => st.Name == selection.Name);
                selection.Id = temp.Id;
                return true;
            }
            else return false;
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
