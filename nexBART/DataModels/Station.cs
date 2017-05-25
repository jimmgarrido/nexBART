using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;


namespace nexBart.DataModels
{
    [Table("Favorites")]
    public class Station : BaseObject
    {
        private string _address;
        private string _bikes;
        private string _parking;
        private string _lockers;
        private string _about;
		private List<Line> _lines;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbrv { get; set; }

        [Ignore]
        public List<Line> Lines {
			get {
				return _lines;
			}
			set
			{
				if (_lines == value)
					return;

				_lines = value;
				NotifyPropertyChanged();
			}
		}
        [Ignore]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                NotifyPropertyChanged();
            }
        }
        [Ignore]
        public string Bikes
        {
            get
            {
                return _bikes;
            }
            set
            {
                _bikes = value;
                NotifyPropertyChanged();
            }
        }
        [Ignore]
        public string Parking
        {
            get
            {
                return _parking;
            }
            set
            {
                _parking = value;
                NotifyPropertyChanged();
            }
        }
        [Ignore]
        public string Lockers
        {
            get
            {
                return _lockers;
            }
            set
            {
                _lockers = value;
                NotifyPropertyChanged();
            }
        }
        [Ignore]
        public string Info
        {
            get
            {
                return _about;
            }
            set
            {
                _about = value;
                NotifyPropertyChanged();
            }
        }

		public Station() { }

        public Station(string name, string abbrv)
        {
            Name = name;
            Abbrv = abbrv;
			Lines = new List<Line>();
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
