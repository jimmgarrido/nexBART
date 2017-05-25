using nexBart.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.Helpers
{
    public static class FavoritesManager
    {
		public static event EventHandler FavoritesChanged;

		public static List<Station> Favorites;

		public static void Init()
		{
			DatabaseHelper.InitDatabase();
			LoadFavorites();
		}

		static async void LoadFavorites()
		{
			Favorites = await DatabaseHelper.GetFavoritesAsync();
			FavoritesChanged?.Invoke(typeof(FavoritesManager), new EventArgs());
		}

		public static void FavoriteStation(Station station)
		{
			DatabaseHelper.AddFavoriteAsync(station);

		}
    }
}
