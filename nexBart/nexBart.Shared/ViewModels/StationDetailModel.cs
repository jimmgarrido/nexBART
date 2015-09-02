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

        public async Task FavoriteStation()
        {
            await DatabaseHelper.AddFavorite(Selection);
        }
    }
}
