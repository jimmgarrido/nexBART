using nexBart.Common;
using nexBart.DataModels;
using nexBart.ViewModels;
using nexBart.Views;
using System;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        private bool loaded = false;

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

            if(!loaded) await LoadData();
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
            var statusBar = StatusBar.GetForCurrentView().ProgressIndicator;
            await statusBar.ShowAsync();
            statusBar.ProgressValue = null;

            await FavoritesView.CheckFavoritesDB();
            alertsGroup.Source = await AlertsView.RefreshAlerts();

            loaded = true;

            statusBar.ProgressValue = 0;
            await statusBar.HideAsync();
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

        private async void RefreshTimes(object sender, RoutedEventArgs e)
        {
            var statusBar = StatusBar.GetForCurrentView().ProgressIndicator;
            await statusBar.ShowAsync();
            statusBar.Text = "Getting Departure Times";
            statusBar.ProgressValue = null;

            AlertsView.Alerts.Clear();

            refreshBtn.IsEnabled = false;
            await FavoritesView.LoadFavorites();
            alertsGroup.Source = await AlertsView.RefreshAlerts();
            refreshBtn.IsEnabled = true;

            statusBar.ProgressValue = 0;
            await statusBar.HideAsync();
        }

        private void MoreDetails(object sender, RoutedEventArgs e)
        {
            FavoritesView.IsFavorite(ScheduleView.SelectedStation);
            Frame.Navigate(typeof(StationDetailPage), ScheduleView.SelectedStation);
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