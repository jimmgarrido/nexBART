using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.DataModels
{
    [Table("Favorites")]
    public class Station
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbrv { get; set; }
        [Ignore]
        public ObservableCollection<Line> Lines { get; set; }

        public Station() 
        {
            Lines = new ObservableCollection<Line>();
        }

        public Station(string _name, string _abbrv)
        {
            Lines = new ObservableCollection<Line>();
            Name = _name;
            Abbrv = _abbrv;
        }

        public void AddLineList(List<Line> lines)
        {
            foreach(Line l in lines)
            {
                Lines.Add(l);
            }
        }
    }
}
