using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.ViewModel.Commands;
using TravelRecordApp.ViewModel.Interfaces;

namespace TravelRecordApp.ViewModel
{
    public class HomeViewModel : INavigateViewModel
    {
        public NavigationCommand NavCommand { get; set; }

        public HomeViewModel()
        {
            NavCommand = new NavigationCommand(this);
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTravelPage());
        }
    }
}
