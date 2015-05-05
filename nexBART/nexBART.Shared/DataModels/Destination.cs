using System;
using System.Collections.Generic;
using System.Text;

namespace nexBart.DataModels
{
    class Destination
    {
        public string Name { get; set; }
        public Train[] Trains { get; set; }

        public Destination() 
        {
            Trains = new Train[3];
        }

        public Destination(string dest)
        {
            Trains = new Train[3];
            Name = dest;
        }
    }
}
