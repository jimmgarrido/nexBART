using nexBart.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace nexBart.UWP.Views
{
	public sealed partial class AlertsView : UserControl
	{
		AlertsModel ViewModel;
		Task Initialization;


		public AlertsView()
		{
			this.InitializeComponent();

			ViewModel = new AlertsModel();
			Initialization = InitializationAsync();
		}

		async Task InitializationAsync()
		{
			alertsGroup.Source = await ViewModel.RefreshAlerts();
		}
	}
}
