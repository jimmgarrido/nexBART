using nexBart.Common;
using nexBart.DataModels;
using nexBart.Models;
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
    public sealed partial class HubPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();

        //Represent each hub section as a model
        private static SchedulesModel _scheduleView;
        private static FavoritesModel _favoritesView;

        public static SchedulesModel ScheduleView
        {
            get
            {
                return _scheduleView;
            }
            private set
            {
                _scheduleView = value;
            }
        }
        public static FavoritesModel FavoritesView
        {
            get
            {
                return _favoritesView;
            }
            private set
            {
                _favoritesView = value;
            }
        }

        private Button favBtn, detailBtn;
        public HubPage()
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

            this.DefaultViewModel["Favorites"] = FavoritesView;
            this.DefaultViewModel["Schedules"] = ScheduleView;
        }

        private void StopClicked(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(StationDetailPage), e.ClickedItem);
        }

        private async void ScheduleStationSelected(ListPickerFlyout sender, ItemsPickedEventArgs args)
        {
            StationData selected = sender.SelectedItem as StationData;
            await ScheduleView.StationSelected(selected);

            favBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            detailBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;

            if(FavoritesView.IsFavorite(ScheduleView.SelectedStation[0]))
            {
                favBtn.Content = "Remove Favorite";
            }
            else
            {
                favBtn.Content = "Add Favorite";
            }
        }

        private async void FavButtonClicked(object sender, RoutedEventArgs e)
        {
            FavoritesView.FavoriteStations.Clear();
            await FavoritesView.AddFavorite(ScheduleView.SelectedStation[0]);
            await FavoritesView.RefreshFavorites();
        }

        private async void RefreshTimes(object sender, RoutedEventArgs e)
        {
            FavoritesView.FavoriteStations.Clear();
            await FavoritesView.RefreshFavorites();
        }

        private void MoreDetails(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StationDetailPage), ScheduleView.SelectedStation[0]);
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        #region NavigationHelper Methods

        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            await FavoritesView.CheckFavorites();
            await FavoritesView.RefreshFavorites();
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {

        }

        #endregion

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void FavButtonLoaded(object sender, RoutedEventArgs e)
        {
            favBtn = sender as Button;
        }

        private void DetailButtonLoaded(object sender, RoutedEventArgs e)
        {
            detailBtn = sender as Button;
        }
    }
}