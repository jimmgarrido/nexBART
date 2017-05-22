using nexBart.DataModels;
using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace nexBart.Helpers
{
    public class LineColorConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var color = (RGBColor)value;
			return new SolidColorBrush(Color.FromArgb(color.colorBytes[0], color.colorBytes[1], color.colorBytes[2], color.colorBytes[3]));
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
