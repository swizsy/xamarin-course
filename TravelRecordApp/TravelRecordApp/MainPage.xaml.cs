using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelRecordApp.Model;
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
            AttemptUserLoginAsync();
        }

        private void LoadResources()
        {
            Type assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
        }

        private async void AttemptUserLoginAsync()
        {
            User user = (await App.MobileService.GetTable<User>().Where(u => u.Email == emailEntry.Text).ToListAsync()).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == passwordEntry.Text)
                {
                    App.user = user;
                    await Navigation.PushAsync(new HomePage());
                    return;
                }
            }

            await DisplayAlert("Error", "Incorrect email or password!", "OK");
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
