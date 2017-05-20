using System;
using Windows.UI.Xaml.Controls;

using nexBart.ViewModels;
using nexBart.DataModels;

namespace nexBart.UWP.Views
{
	public sealed partial class RealTimeView : UserControl
	{
		public RealTimeViewModel ViewModel { get; set; }

		public RealTimeView()
		{
			this.InitializeComponent();

			ViewModel = new RealTimeViewModel();
			DataContext = ViewModel;
		}

		private async void StationSelected(object sender, SelectionChangedEventArgs e)
		{
			var selectedStation = ((ComboBox)sender).SelectedItem as Station;
			await ViewModel.StationSelected(selectedStation);
		}
	}
}
