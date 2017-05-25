using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.Models
{
    public class StationDetailModel
    {
        public Station Selection;

        public StationDetailModel() { }

        public StationDetailModel(Station station)
        {
            Selection = station;
        }

        public async Task LoadStationInfo()
        {
            var details = await WebHelper.GetStationInfo(Selection);
            
            Selection.Address = details.address;
            Selection.Bikes = details.bikes;
            Selection.Parking = details.parking;
            Selection.Lockers = details.lockers;
            Selection.Info = details.info;
        }

        public async Task FavoriteStation()
        {
            await DatabaseHelper.AddFavoriteAsync(Selection);
        }

        public async Task UnfavoriteStation()
        {
            await DatabaseHelper.RemoveFavorite(Selection);
        }
    }
}
