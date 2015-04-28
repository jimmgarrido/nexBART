using nexBart.Common;
using nexBart.DataModels;
using nexBart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Station item = (Station) e.NavigationParameter;
            DetailModel = new StationDetailModel(item);

            //StopHeader.Text = DetailModel.Selection[0].Name;
            StopHeader.Text = DetailModel.Selection.Name;

            //stationList.ItemsSource = DetailModel.Selection[0].Lines;
            DataTemplate sectionTemplate = new DataTemplate();

            if (item.Id == 0)
            {
                FavoriteBtn.Label = "favorite";
                FavoriteBtn.Icon = new SymbolIcon(Symbol.Add);
                FavoriteBtn.Click += AddFavorite;
            }
            else
            {
                FavoriteBtn.Label = "unfavorite";
                FavoriteBtn.Icon = new SymbolIcon(Symbol.Remove);
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
        }

        private async void AddFavorite(object sender, RoutedEventArgs e)
        {
            MainPage.FavoritesView.FavoriteStations.Clear();
            await MainPage.FavoritesView.AddFavorite(DetailModel.Selection);
            await MainPage.FavoritesView.RefreshFavorites();
            FavoriteBtn.Label = "unfavorite";
            FavoriteBtn.Icon = new SymbolIcon(Symbol.Remove);
            FavoriteBtn.Click -= AddFavorite;
            FavoriteBtn.Click += RemoveFavorite;
        }

        private async void RemoveFavorite(object sender, RoutedEventArgs e)
        {
            MainPage.FavoritesView.FavoriteStations.Clear();
            await MainPage.FavoritesView.RemoveFavorite(DetailModel.Selection);
            await MainPage.FavoritesView.RefreshFavorites();
            FavoriteBtn.Label = "favorite";
            FavoriteBtn.Icon = new SymbolIcon(Symbol.Add);
            FavoriteBtn.Click -= RemoveFavorite;
            FavoriteBtn.Click += AddFavorite;
        }

        private async void RefreshTimes(object sender, RoutedEventArgs e)
        {
            MainPage.FavoritesView.FavoriteStations.Clear();
            await MainPage.FavoritesView.RemoveFavorite(DetailModel.Selection);
            await MainPage.FavoritesView.RefreshFavorites();
            FavoriteBtn.Label = "favorite";
            FavoriteBtn.Icon = new SymbolIcon(Symbol.Add);
            FavoriteBtn.Click -= RemoveFavorite;
            FavoriteBtn.Click += AddFavorite;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

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
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
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
