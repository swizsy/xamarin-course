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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            RegisterUserIfPossible();
        }

        private async void RegisterUserIfPossible()
        {
            if (passwordEntry.Text == confirmPasswordEntry.Text)
            {
                // register the user
                User user = new User()
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text
                };

                await App.MobileService.GetTable<User>().InsertAsync(user);
            }
            else
            {
                await DisplayAlert("Error", "Passwords don't match!", "OK");
            }
        }
    }
}