using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace nexBart.ViewModels
{
    public class FavoritesModel
    {
        public ObservableCollection<Station> FavoriteStations {get; set;}

        public FavoritesModel()
        {
            FavoriteStations = new ObservableCollection<Station>();
        }

        public async Task RefreshFavorites()
        {
            List<Station> favorites = await DatabaseHelper.GetFavorites();

            foreach(Station s in favorites)
            {
                var lines = await WebHelper.GetPredictions(s);
                s.AddLineList(lines);
            }

            foreach(Station x in favorites)
            {
                FavoriteStations.Add(x);
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
    }
}
