using System;
using Windows.UI.Xaml.Controls;

using nexBart.ViewModels;
using nexBart.DataModels;
using nexBart.Helpers;

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
			ViewModel.SelectedStation = selectedStation;
			await ViewModel.UpdateStationData();
		}

		private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			var currentStation = ViewModel.SelectedStation;
			FavoritesManager.FavoriteStation(currentStation);
		}
	}
}
