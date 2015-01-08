﻿using nexBart.Common;
using nexBart.DataModel;
using nexBart.DataModels;
using nexBart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public ScheduleGroup scheduleData;

        public HubPage()
        {
            this.InitializeComponent();

            // Hub is only supported in Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            StationGroup.StationItems.Add(new Station("12th St. Oakland City Center"));
            StationGroup.StationItems.Add(new Station("16th St. Mission"));
            StationGroup.StationItems.Add(new Station("Hayward"));

            StationGroup.StationItems[0].LinesList.Add(new Line("Pittsburg/Bay Point"));
            StationGroup.StationItems[0].LinesList[0].Destinations[0] = "Pittsburg/Bay Point";
            StationGroup.StationItems[0].LinesList[0].Destinations[1] = "SF Int'l Airport";
            

            StationGroup.StationItems[0].LinesList.Add(new Line("Richmond"));
            StationGroup.StationItems[0].LinesList[1].Destinations[0] = "Richmond";
            StationGroup.StationItems[0].LinesList[1].Destinations[1] = "Daly City";

            StationGroup.StationItems[0].LinesList.Add(new Line("Dublin/Pleasaton"));
            StationGroup.StationItems[0].LinesList[2].Destinations[0] = "Dublin/Pleasaton";
            StationGroup.StationItems[0].LinesList[2].Destinations[1] = "Daly City";

            StationGroup.StationItems[1].LinesList.Add(new Line("Richmond"));
            StationGroup.StationItems[1].LinesList[0].Destinations[0] = "Richmond";
            StationGroup.StationItems[1].LinesList[0].Destinations[1] = "Fremont";

            StationGroup.StationItems[2].LinesList.Add(new Line("Richmond"));
            StationGroup.StationItems[2].LinesList[0].Destinations[0] = "Pittsburg/Bay Point";
            StationGroup.StationItems[2].LinesList[0].Destinations[1] = "SF Int'l Airport";

            StationGroup.StationItems[2].LinesList.Add(new Line("Dublin/Pleasaton"));
            StationGroup.StationItems[2].LinesList[1].Destinations[0] = "Fremont";
            StationGroup.StationItems[2].LinesList[1].Destinations[1] = "Daly City";


        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            FavoritesModel.LoadFavorites();
            scheduleData = new ScheduleGroup();
            //scheduleData.Name = "Selecefwf";
            this.DefaultViewModel["Favorites"] = StationGroup.GetStations();
            this.DefaultViewModel["Schedules"] = scheduleData;
            //StationC
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }

        /// <summary>
        /// Shows the details of a clicked group in the <see cref="SectionPage"/>.
        /// </summary>
        /// <param name="sender">The source of the click event.</param>
        /// <param name="e">Details about the click event.</param>
        private void StopClicked(object sender, ItemClickEventArgs e)
        {
            //var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            //if (!Frame.Navigate(typeof(SectionPage), groupId))
            //{
            //    throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            //}
        }

        private void StationSelected(object sender, SelectionChangedEventArgs e)
        {
            StationData selected = (StationData)(((ComboBox)sender).SelectedItem);

            HubPageModel.StationSelected(selected, ref scheduleData);
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