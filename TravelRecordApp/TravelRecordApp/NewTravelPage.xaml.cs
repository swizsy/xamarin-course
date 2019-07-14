using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        private Post post;
        public NewTravelPage()
        {
            InitializeComponent();

            post = new Post();
            containerStackLayout.BindingContext = post;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenuesAsync(position.Latitude, position.Longitude);
            venuesListView.ItemsSource = venues.OrderBy(x => x.location.distance);
        }

        private void SaveTbi_Clicked(object sender, EventArgs e)
        {
            SavePost();
        }

        private void SavePost()
        {
            try
            {
                Venue selectedVenue = venuesListView.SelectedItem as Venue;
                Category firstCategory = selectedVenue.categories.FirstOrDefault();

                post.UserId = App.user.Id;
                post.VenueName = selectedVenue.name;
                post.CategoryId = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Address = selectedVenue.location.address;
                post.Latitude = selectedVenue.location.lat;
                post.Longitude = selectedVenue.location.lng;
                post.Distance = selectedVenue.location.distance;

                Post.Insert(post);

                Navigation.PushAsync(new HomePage());
            }
            catch (Exception)
            {
                DisplayAlert("Error", "Failed to save experience!", "OK");
            }
        }
    }
}