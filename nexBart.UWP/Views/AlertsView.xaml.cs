using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

using nexBart.ViewModels;

namespace nexBart.UWP.Views
{
	public sealed partial class AlertsView : UserControl
	{
		AlertsModel ViewModel;

		public AlertsView()
		{
			this.InitializeComponent();

			ViewModel = new AlertsModel();
			this.Loaded += AlertsView_Loaded;

			//Initialization = InitializationAsync();
		}

		async void AlertsView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			alertsGroup.Source = await ViewModel.RefreshAlerts();
		}

		async Task InitializationAsync()
		{
			alertsGroup.Source = await ViewModel.RefreshAlerts();
		}		
	}
}