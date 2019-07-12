﻿using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenuesAsync(position.Latitude, position.Longitude);
            venuesListView.ItemsSource = venues.OrderBy(x => x.location.distance);
        }

        private void SaveTbi_Clicked(object sender, EventArgs e)
        {
            SavePost();
        }

        private async void SavePost()
        {
            try
            {
                Venue selectedVenue = venuesListView.SelectedItem as Venue;
                Category firstCategory = selectedVenue.categories.FirstOrDefault();

                Post post = new Post()
                {
                    UserId = App.user.Id,
                    Experience = experienceEntry.Text,
                    VenueName = selectedVenue.name,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    Distance = selectedVenue.location.distance
                };

                await App.MobileService.GetTable<Post>().InsertAsync(post);

                await Navigation.PushAsync(new HomePage());

                #region SQLite Local Database Code
                //using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                //{
                //    conn.CreateTable<Post>();
                //    rows = conn.Insert(post);
                //} 
                #endregion
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Failed to save experience!", "OK");
            }
        }
    }
}