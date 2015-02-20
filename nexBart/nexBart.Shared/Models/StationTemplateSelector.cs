using nexBart.DataModels;
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

            if (stationItem.Lines.Count == 1) return oneLineTemplate;
            else if (stationItem.Lines.Count == 2) return twoLineTemplate;
            else if (stationItem.Lines.Count == 3) return threeLineTemplate;
            else if (stationItem.Lines.Count == 4) return fourLineTemplate;
            else return oneLineTemplate;
        }
    }
}
