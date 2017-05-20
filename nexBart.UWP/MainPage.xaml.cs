using nexBart.UWP.Views;
using nexBart.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace nexBart.UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

			MenuList.DataContext = new MenuViewModel();
        }

		private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selection = (string)((ListView)sender).SelectedItem;

			if (selection == "Real Time")
				ContentGrid.Children[0] = new RealTimeView();
			else if (selection == "Schedule")
				ContentGrid.Children[0] = new ScheduleView();
			else 
				ContentGrid.Children[0] = new StackPanel();
		}
	}
}
