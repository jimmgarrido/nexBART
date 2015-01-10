﻿using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.Models
{
    public class FavoritesModel
    {
        public static ObservableCollection<Station> FavoriteStations;

        public FavoritesModel()
        {
            FavoriteStations = new ObservableCollection<Station>();
            Task.Run(() => LoadFavorites());
        }

        public static async Task LoadFavorites()
        {
            await DatabaseHelper.CheckDB();
            await DatabaseHelper.GetFavorites();
        }

        public static async Task RefreshFavorites()
        {
            await DatabaseHelper.GetFavorites();
        }

        public static async Task AddFavorite(Station favorite)
        {
            await DatabaseHelper.AddFavorite(favorite);
            await RefreshFavorites();
        }
    }
}
