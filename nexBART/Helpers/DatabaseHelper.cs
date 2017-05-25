using nexBart.DataModels;
using nexBart.Models;
using SQLite;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace nexBart.Helpers
{
    public static class DatabaseHelper
    {
        private static string dbPath;
        private static List<Station> _favorites;

        public static List<Station> Favorites
        {
            get
            {
                return _favorites;
            }
            set
            {
                _favorites = value;
            }
        }

        public static async Task InitDatabase()
        {
            bool dbExsists;
            StorageFile file = null;

            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync("favorites.sqlite");
                dbPath = file.Path;
                dbExsists = true;
            }
            catch (FileNotFoundException)
            {
                dbExsists = false;
            }

            if(!dbExsists)
            {
                await MakeDB(file);
            }
        }

        private static async Task MakeDB(StorageFile file)
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("favorites.sqlite");
            file = await ApplicationData.Current.LocalFolder.GetFileAsync("favorites.sqlite");
            dbPath = file.Path;

            SQLiteAsyncConnection favDB = new SQLiteAsyncConnection(dbPath);
            await favDB.CreateTableAsync<Station>();
        }

        public static async Task<List<Station>> GetFavoritesAsync()
        {
            SQLiteAsyncConnection favDB = new SQLiteAsyncConnection(dbPath);
            return await favDB.QueryAsync<Station>("SELECT * FROM Favorites");
        }

        public static async Task AddFavoriteAsync(Station s)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbPath);
            await conn.InsertAsync(s);
        }

        public static async Task RemoveFavorite(Station s)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbPath);
            await conn.DeleteAsync(s);
        }
    }
}
