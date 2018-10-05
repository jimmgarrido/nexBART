using nexBart.DataModels;
using SQLite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace nexBart.Helpers
{
    public static class DatabaseHelper
    {
        const string databaseName = "favorites.sqlite";

        static string databasePath = null;
        static List<Station> _favorites;

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
            var file = await ApplicationData.Current.LocalFolder.TryGetItemAsync(databaseName);

            if(file != null)
                databasePath = file.Path;
            else
                await CreateDatabaseAync();
        }

        private static async Task CreateDatabaseAync()
        {
            var newFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName);
            databasePath = newFile.Path;

            SQLiteAsyncConnection favDB = new SQLiteAsyncConnection(databasePath);
            await favDB.CreateTableAsync<Station>();
        }

        public static async Task<List<Station>> GetFavoritesAsync()
        {
            SQLiteAsyncConnection favDB = new SQLiteAsyncConnection(databasePath);
            return await favDB.QueryAsync<Station>("SELECT * FROM Favorites");
        }

        public static async Task AddFavoriteAsync(Station s)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(databasePath);
            await conn.InsertAsync(s);
        }

        public static async Task RemoveFavorite(Station s)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(databasePath);
            await conn.DeleteAsync(s);
        }
    }
}
