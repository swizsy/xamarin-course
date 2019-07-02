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
