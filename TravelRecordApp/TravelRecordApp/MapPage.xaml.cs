using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool hasLocationPermission;

        public MapPage()
        {
            InitializeComponent();

            GetPermissions();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            RegisterLocator();
            GetLocation();

            List<Post> posts;
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Post>();
                posts = connection.Table<Post>().ToList();
            }

            DisplayOnMap(posts);
        }

        private void DisplayOnMap(List<Post> posts)
        {
            locationMap.Pins.Clear();
            foreach (Post post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);
                    var pin = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Label = post.VenueName,
                        Position = position,
                        Address = post.Address
                    };

                    locationMap.Pins.Add(pin);
                }
                catch (NullReferenceException nullRefEx)
                {
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeregisterLocator();
        }

        private async void RegisterLocator()
        {
            if (hasLocationPermission)
            {
                CrossGeolocator.Current.PositionChanged += OnLocatorPositionChanged;
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.Zero, 100);
            }
        }
        private async void DeregisterLocator()
        {
            CrossGeolocator.Current.PositionChanged -= OnLocatorPositionChanged;
            await CrossGeolocator.Current.StopListeningAsync();
        }


        private void OnLocatorPositionChanged(object sender, PositionEventArgs e)
        {
            GetLocation();
        }

        private async void GetPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need your location", "Access to your location is requested", "OK");
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (results.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = results[Permission.LocationWhenInUse];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    locationMap.IsShowingUser = true;
                }
                else
                {
                    await DisplayAlert("Location access denied", "Your location will not be shown on the map", "OK");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }
        }

        private async void GetLocation()
        {
            if (!hasLocationPermission)
            {
                return;
            }

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            MoveMap(position);
        }

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);

            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationMap.MoveToRegion(span);
        }
    }
}