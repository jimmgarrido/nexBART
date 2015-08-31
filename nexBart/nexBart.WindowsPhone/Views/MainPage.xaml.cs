using nexBart.Common;
using nexBart.DataModels;
using nexBart.ViewModels;
using nexBart.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace nexBart
{
    public sealed partial class MainPage : Page
    {
        #region Navigation and ViewModel Properties
        private readonly NavigationHelper navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        public ObservableDictionary DefaultViewModel { get; set; }
        #endregion

        //Represent each hub section as a model
        private Button refreshBtn, detailBtn;

        public SchedulesModel ScheduleView { get; set; }
        public FavoritesModel FavoritesView { get; set; }
        public AlertsModel AlertsView { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            // Hub is only supported in Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            //Initialize the views and bind to the hub control
            FavoritesView = new FavoritesModel();
            ScheduleView = new SchedulesModel();
            AlertsView = new AlertsModel();

            //this.DefaultViewModel["Favorites"] = FavoritesView;
            //this.DefaultViewModel["Schedules"] = ScheduleView;
            //this.DefaultViewModel["Alerts"] = AlertsView;

            FavoritesSection.DataContext = FavoritesView;
            SchedulesSection.DataContext = ScheduleView;
            AlertsSection.DataContext = AlertsView;
        }


        /**
        ** Lifecycle methods
        **/
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            await LoadData();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }


        /**
        ** Event Handlers
        **/
        private async Task LoadData()
        {
            await FavoritesView.CheckFavoritesDB();
            await FavoritesView.RefreshFavorites();
            alertsGroup.Source = await AlertsView.RefreshAlerts();
            //alertsGroup.Source = AlertsView.GetGroup();
        }

        private void StopClicked(object sender, ItemClickEventArgs e)
        {
            Station item = (Station) e.ClickedItem;
            FavoritesView.IsFavorite(item);
            Frame.Navigate(typeof(StationDetailPage), item);
        }

        private async void ScheduleStationSelected(object sender, SelectionChangedEventArgs args)
        {
            Station selected = ((ComboBox)sender).SelectedItem as Station;
            await ScheduleView.StationSelected(selected);

            refreshBtn.Visibility = Visibility.Visible;
            detailBtn.Visibility = Visibility.Visible;
        }

        //private void FavButtonClicked(object sender, RoutedEventArgs e)
        //{
        //    if(((Button)sender).Content.Equals("Add Favorite"))
        //    {
        //        AddFavorite();
        //    }
        //    else if(((Button)sender).Content.Equals("Remove Favorite"))
        //    {
        //        RemoveFavorite();
        //    }
        //}

        //private async void AddFavorite()
        //{
        //    FavoritesView.FavoriteStations.Clear();
        //    await FavoritesView.AddFavorite(ScheduleView.SelectedStation[0]);
        //    await FavoritesView.RefreshFavorites();
        //    favBtn.Content = "Remove Favorite";
        //}

        //private async void RemoveFavorite()
        //{
        //    FavoritesView.FavoriteStations.Clear();
        //    await FavoritesView.RemoveFavorite(ScheduleView.SelectedStation[0]);
        //    await FavoritesView.RefreshFavorites();
        //    favBtn.Content = "Add Favorite";
        //}

        private async void RefreshTimes(object sender, RoutedEventArgs e)
        {
            FavoritesView.FavoriteStations.Clear();
            AlertsView.Alerts.Clear();

            await FavoritesView.RefreshFavorites();
            alertsGroup.Source = await AlertsView.RefreshAlerts();
        }

        private void MoreDetails(object sender, RoutedEventArgs e)
        {
            FavoritesView.IsFavorite(ScheduleView.SelectedStation[0]);
            Frame.Navigate(typeof(StationDetailPage), ScheduleView.SelectedStation[0]);
        }
    
        private void RefreshButtonLoaded(object sender, RoutedEventArgs e)
        {
            refreshBtn = sender as Button;
        }

        private void DetailButtonLoaded(object sender, RoutedEventArgs e)
        {
            detailBtn = sender as Button;
        }

        #region NavigationHelper Methods
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {

        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }
        #endregion
    }
}