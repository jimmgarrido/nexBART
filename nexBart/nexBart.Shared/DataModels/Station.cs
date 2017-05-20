using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;


namespace nexBart.DataModels
{
    [Table("Favorites")]
    public class Station : INotifyPropertyChanged
    {
        private string _address;
        private string _bikes;
        private string _parking;
        private string _lockers;
        private string _about;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbrv { get; set; }

        [Ignore]
        public ObservableCollection<Line> Lines { get; set; }
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
                NotifyPropertyChanged("Address");
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
                NotifyPropertyChanged("Bikes");
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
                NotifyPropertyChanged("Parking");
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
                NotifyPropertyChanged("Lockers");
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
                NotifyPropertyChanged("Info");
            }
        }

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

        #region INotify Methods
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
