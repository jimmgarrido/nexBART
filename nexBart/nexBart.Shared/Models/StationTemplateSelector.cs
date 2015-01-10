using nexBart.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace nexBart.Models
{
    class StationTemplateSelector : DataTemplateSelector
    {
        public DataTemplate oneLineTemplate { get; set; }
        public DataTemplate twoLineTemplate { get; set; }
        public DataTemplate threeLineTemplate { get; set; }
        public DataTemplate fourLineTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            Station stationItem = item as Station;

            if (stationItem.LinesList.Count == 1) return oneLineTemplate;
            else if (stationItem.LinesList.Count == 2) return twoLineTemplate;
            else if (stationItem.LinesList.Count == 3) return threeLineTemplate;
            else if (stationItem.LinesList.Count == 4) return fourLineTemplate;
            else return base.SelectTemplateCore(item, container);
        }
    }
}
