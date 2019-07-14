using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;
using TravelRecordApp.ViewModel.Interfaces;

namespace TravelRecordApp.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged, INavigateViewModel
    {
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                RaisePropertyChangedEvent("User");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;

                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password
                };

                RaisePropertyChangedEvent("Email");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;

                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password
                };

                RaisePropertyChangedEvent("Password");
            }
        }

        #region Commands
        public LoginCommand LoginCommand { get; set; }

        public NavigationCommand NavCommand { get; set; } 
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
            NavCommand = new NavigationCommand(this);
        }

        public async void Login()
        {
            bool success = await User.AttemptUserLogin(User.Email, User.Password);

            if (success)
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Incorrect email or password!", "OK");
            }
        }

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
