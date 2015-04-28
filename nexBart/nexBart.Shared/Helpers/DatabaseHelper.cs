using nexBart.DataModels;
using nexBart.Models;
using SQLite;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace nexBart.Helpers
{
    class DatabaseHelper
    {
        static string dbPath;
        public static async Task CheckDB()
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

        public static async Task<List<Station>> GetFavorites()
        {
            //List<Station> favorites = new List<Station>();

            SQLiteAsyncConnection favDB = new SQLiteAsyncConnection(dbPath);
            List<Station> favorites = await favDB.QueryAsync<Station>("SELECT * FROM Favorites");

            return favorites;
        }

        public static async Task AddFavorite(Station s)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbPath);
            //await conn.InsertAsync(new StationData
            //{
            //    Name = s.Name,
            //    Abbrv = s.Abbrv
            //});
            await conn.InsertAsync(s);
        }

        public static async Task RemoveFavorite(Station s)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbPath);
            await conn.DeleteAsync(s);
        }
    }
}
