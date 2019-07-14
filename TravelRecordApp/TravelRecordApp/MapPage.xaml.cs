using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private MapViewModel viewModel;
        public MapPage()
        {
            InitializeComponent();

            viewModel = new MapViewModel(locationsMap);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.RegisterLocator();
            viewModel.PanToLocation();
            viewModel.DisplayPostsOnMap();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.DeregisterLocator();
        }
    }
}