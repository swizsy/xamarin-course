using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        private NewTravelViewModel viewModel;
        public NewTravelPage()
        {
            viewModel = new NewTravelViewModel();

            InitializeComponent();

            containerStackLayout.BindingContext = viewModel;
            saveTbi.BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenuesAsync(position.Latitude, position.Longitude);
            venuesListView.ItemsSource = venues.OrderBy(x => x.location.distance);
        }
    }
}