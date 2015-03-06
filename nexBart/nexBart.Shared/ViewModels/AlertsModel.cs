using nexBart.DataModels;
using nexBart.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nexBart.ViewModels
{
    public class AlertsModel
    {
        public ObservableCollection<Alert> Alerts { get; set; }

        public AlertsModel()
        {
            Alerts = new ObservableCollection<Alert>();
        }

        public async Task<IOrderedEnumerable<IGrouping<string, Alert>>> RefreshAlerts()
        {
            List<Alert> alerts = await WebHelper.GetAlerts();

            foreach(Alert a in alerts)
            {
                Alerts.Add(a);
            }

            return from alert in Alerts group alert by alert.Type into grp orderby grp.Key select grp;
        }

        public IOrderedEnumerable<IGrouping<string, Alert>> GetGroup()
        {
            return from alert in Alerts group alert by alert.Type into grp orderby grp.Key select grp;
        }
    }
}
