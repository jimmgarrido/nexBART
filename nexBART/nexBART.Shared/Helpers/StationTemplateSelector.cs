using nexBart.DataModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace nexBart.Helpers
{
    public class StationTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OneDestinationTemplate { get; set; }
        public DataTemplate TwoDestinationTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var line = item as Line;

            //if (stationItem.Lines.Count == 1) return OneDestinationTemplate;
            //else if (stationItem.Lines.Count == 2) return TwoDestinationTemplate;
            return TwoDestinationTemplate;
        }
    }
}
