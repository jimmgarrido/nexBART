using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace nexBart.DataModels
{
    public class Alert
    {
        public string Time { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public Alert(string time, string type, string des) 
        {
            Time = time;
            Type = type;
            Description = des;
        }
    }
}
