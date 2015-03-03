using nexBart.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace nexBart.ViewModels
{
    public class AlertsModel
    {
        public static ObservableCollection<Alert> Alerts { get; set; }

        public AlertsModel()
        {
            Alerts = new ObservableCollection<Alert>();

            Alerts.Add(new Alert("10:44 PM", "Advisory", 
                "BART is running round-the-clock service during the labor day weekend bay bridge closure. More info at www.bart.gov or (510) 465-2278."));
        }

        public void GetGroup()
        {
            var sorted = from alert in Alerts group alert by alert.Type into grp orderby grp.Key select grp;
        }
    }
}
