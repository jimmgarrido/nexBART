using nexBart.DataModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace nexBart.Helpers
{
    public class ScheduleTemplateSelector : DataTemplateSelector
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

	public class FavoritesTemplateSelector : DataTemplateSelector
	{
		public DataTemplate OneLineTemplate { get; set; }
		public DataTemplate TwoLineTemplate { get; set; }
		public DataTemplate ThreeLineTemplate { get; set; }
		public DataTemplate FourLineTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
		{
			Station stationItem = item as Station;

			if (stationItem.Lines.Count == 1) return OneLineTemplate;
			else if (stationItem.Lines.Count == 2) return TwoLineTemplate;
			else if (stationItem.Lines.Count == 3) return ThreeLineTemplate;
			else if (stationItem.Lines.Count == 4) return FourLineTemplate;
			else return OneLineTemplate;
		}
	}
}
