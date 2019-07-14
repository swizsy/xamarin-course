using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using TravelRecordApp.Model;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace TravelRecordApp.ViewModel
{
    public class MapViewModel
    {
        private bool hasLocationPermission;

        private Map locationsMap;

        public MapViewModel(Map map)
        {
            locationsMap = map;
            GetPermissions();
        }

        public async void RegisterLocator()
        {
            if (hasLocationPermission)
            {
                CrossGeolocator.Current.PositionChanged += OnLocatorPositionChanged;
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.Zero, 100);
            }
        }

        public async void DeregisterLocator()
        {
            CrossGeolocator.Current.PositionChanged -= OnLocatorPositionChanged;
            await CrossGeolocator.Current.StopListeningAsync();
        }

        private void OnLocatorPositionChanged(object sender, PositionEventArgs e)
        {
            PanToLocation();
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
                        await App.Current.MainPage.DisplayAlert("Need your location", "Access to your location is requested", "OK");
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
                    locationsMap.IsShowingUser = true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Location access denied", "Your location will not be shown on the map", "OK");
                }
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
        }

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);

            var span = new MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);
        }

        public async void PanToLocation()
        {
            if (!hasLocationPermission)
            {
                return;
            }

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            MoveMap(position);
        }

        public async void DisplayPostsOnMap()
        {
            List<Post> posts = await Post.GetUserPosts();

            locationsMap.Pins.Clear();
            foreach (Post post in posts)
            {
                try
                {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);
                    var pin = new Pin()
                    {
                        Type = PinType.SavedPin,
                        Label = post.VenueName,
                        Position = position,
                        Address = post.Address
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
