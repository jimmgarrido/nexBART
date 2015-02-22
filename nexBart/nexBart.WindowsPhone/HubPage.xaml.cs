using nexBart.Common;
using nexBart.DataModels;
using nexBart.Models;

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
            
            FavoritesView.CheckFavorites();

            this.DefaultViewModel["Favorites"] = FavoritesView;
            this.DefaultViewModel["Schedules"] = ScheduleView;
        }

        private void StopClicked(object sender, ItemClickEventArgs e)
        {

        }

        private async void ScheduleStationSelected(object sender, SelectionChangedEventArgs e)
        {
            StationData selected = (StationData)(((ComboBox)sender).SelectedItem);

            await ScheduleView.StationSelected(selected);
            //ScheduleView.SetStation(new Stationselected);
            //ScheduleView.SetSelectedStation(await SchedulesModel.StationSelected(selected));
        }

        private async void AddFavorite(object sender, RoutedEventArgs e)
        {
            FavoritesView.FavoriteStations.Clear();
            await FavoritesView.AddFavorite(ScheduleView.SelectedStation[0]);
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

    }
}