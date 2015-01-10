using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace nexBart.DataModels
{
    [Table("Favorites")]
    public class StationData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbrv { get; set; }

        public StationData() { }

        public StationData(string _name, string _abbrv)
        {
            Name = _name;
            Abbrv = _abbrv;
        }
    }
}
