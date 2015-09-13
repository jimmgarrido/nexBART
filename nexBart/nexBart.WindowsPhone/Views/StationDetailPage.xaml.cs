﻿using nexBart.Common;
using nexBart.DataModels;
using nexBart.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace nexBart.Views
{
    public sealed partial class StationDetailPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private StationDetailModel DetailModel;

        public StationDetailPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Station item = (Station) e.NavigationParameter;
            var sectionTemplate = new DataTemplate();
            DetailModel = new StationDetailModel(item);
            DataContext = DetailModel.Selection;

            StopHeader.Text = DetailModel.Selection.Name;

            if (item.Id == 0)
            {
                FavoriteBtn.Label = "favorite";
                FavoriteBtn.Icon = new SymbolIcon(Symbol.Favorite);
                FavoriteBtn.Click += AddFavorite;
            }
            else
            {
                FavoriteBtn.Label = "unfavorite";
                FavoriteBtn.Icon = new SymbolIcon(Symbol.UnFavorite);
                FavoriteBtn.Click += RemoveFavorite; 
            }

            foreach (Line l in DetailModel.Selection.Lines)
            {
                DepartHub.Sections.Add(new HubSection
                {
                    ContentTemplate = HubSectionTemplate,
                    DataContext = l,
                    Style = HubSectionStyle
                });
            }

            await DetailModel.LoadStationInfo();
        }

        private async void AddFavorite(object sender, RoutedEventArgs e)
        {
            FavoriteBtn.IsEnabled = false;
            await DetailModel.FavoriteStation();
            FavoriteBtn.Label = "unfavorite";
            FavoriteBtn.Icon = new SymbolIcon(Symbol.UnFavorite);
            FavoriteBtn.Click -= AddFavorite;
            FavoriteBtn.Click += RemoveFavorite;
            FavoriteBtn.IsEnabled = true;
        }

        private async void RemoveFavorite(object sender, RoutedEventArgs e)
        {
            FavoriteBtn.IsEnabled = false;
            await DetailModel.UnfavoriteStation();
            FavoriteBtn.Label = "favorite";
            FavoriteBtn.Icon = new SymbolIcon(Symbol.Favorite);
            FavoriteBtn.Click -= RemoveFavorite;
            FavoriteBtn.Click += AddFavorite;
            FavoriteBtn.IsEnabled = true;
        }

        private async void RefreshTimes(object sender, RoutedEventArgs e)
        {
            //MainPage.FavoritesView.FavoriteStations.Clear();
            //await MainPage.FavoritesView.RemoveFavorite(DetailModel.Selection);
            //await MainPage.FavoritesView.RefreshFavorites();
            //FavoriteBtn.Label = "favorite";
            //FavoriteBtn.Icon = new SymbolIcon(Symbol.Add);
            //FavoriteBtn.Click -= RemoveFavorite;
            //FavoriteBtn.Click += AddFavorite;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
    }
}
