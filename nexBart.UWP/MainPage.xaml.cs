using nexBart.Helpers;
using nexBart.UWP.Views;
using nexBart.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace nexBart.UWP
{
    public sealed partial class MainPage : Page
    {
		RealTimeView realTime;
		ScheduleView schedules;

        public MainPage()
        {
            this.InitializeComponent();

			//MenuList.DataContext = new MenuViewModel();
			FavoritesManager.Init();
        }

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			//MenuList.SelectedItem = "Real Time";
		}

		private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UserControl newView;
			var selection = (string)((ListView)sender).SelectedItem;

			if (selection == "Real Time")
			{
				if (realTime == null)
					realTime = new RealTimeView();

				newView = realTime;
			}
			else if (selection == "Schedule")
				newView = new ScheduleView();
			else
				newView = new UserControl();

			ContentGrid.Children.Clear();
			ContentGrid.Children.Add(newView);
		}
	}
}
