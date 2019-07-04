using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace TravelRecordApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadResources();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (VerifyEmailAddress(emailEntry.Text) && VerifyPassword(passwordEntry.Text))
            {
                Navigation.PushAsync(new HomePage());
            }
            else
            {

            }
        }

        private void LoadResources()
        {
            Type assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
        }

        private bool VerifyEmailAddress(string emailAddress)
        {
            return !string.IsNullOrEmpty(emailAddress);
        }

        private bool VerifyPassword(string password)
        {
            return !string.IsNullOrEmpty(password);
        }
    }
}
