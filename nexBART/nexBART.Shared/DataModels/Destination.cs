using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.DataModels
{
    public class Destination
    {
        public string Name { get; set; }
        public string Times { get; set; }
        public List<Train> Trains { get; set; }

        public Destination() 
        {
            Trains = new List<Train>();
        }

        public Destination(string dest)
        {
            Trains = new List<Train>();
            Name = dest;
        }
    }
}
