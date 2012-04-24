using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Bing.Maps;
using Windows.Devices.Geolocation;
using MiniFoursquare.API;
using System.Net.Http;

namespace MiniFoursquare
{
    /// <summary>
    /// A simple page with a map and a button on it.
    /// </summary>
    public sealed partial class BlankPage : Page
    {
        Geolocator geo = new Geolocator();
        Location actualLocation;

        public BlankPage()
        {
            this.InitializeComponent();
             geo.DesiredAccuracy = PositionAccuracy.High;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MajPosition();
        }

        private async void MajPosition()
        {
            progressBar.Visibility = Visibility.Visible;
            var currentPosition = await geo.GetGeopositionAsync();

            actualLocation = new Location()
            {
                Latitude = currentPosition.Coordinate.Latitude,
                Longitude = currentPosition.Coordinate.Longitude
            };

            map.SetView(actualLocation, 20, true);
            LoadVenues();
        }

        public async void LoadVenues()
        {
            string v = String.Format("{0:yyyyMMdd}", DateTime.Now);

            string url = FoursquareManager.URLAPI + "ll=" + actualLocation.Latitude.ToString().Replace(",", ".") + "," + 
                actualLocation.Longitude.ToString().Replace(",", ".") + "&client_id=" + 
                FoursquareManager.CLIENTID + "&client_secret=" + FoursquareManager.CLIENTSECRET + "&v=" + v;

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);
            List<Venue> listVenues = FoursquareManager.LoadVenuesFromJson(response);

            listViewVenues.ItemsSource = listVenues;

            progressBar.Visibility = Visibility.Collapsed;
        }
    }
}
