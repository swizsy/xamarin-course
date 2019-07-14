using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public class RegisterViewModelData
        {
            public string Email;
            public string Password;
            public string ConfirmPassword;
        }

        private RegisterViewModelData data;
        public RegisterViewModelData Data
        {
            get { return data; }
            set
            {
                data = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Data), PropertyChanged);
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;

                Data = new RegisterViewModelData
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };

                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Email), PropertyChanged);
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;

                Data = new RegisterViewModelData
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };

                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Password), PropertyChanged);
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;

                Data = new RegisterViewModelData
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };

                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(ConfirmPassword), PropertyChanged);
            }
        }


        #region Commands
        public RegisterCommand RegisterCommand { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public RegisterViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
        }

        public async void Register()
        {
            bool success = false;

            if (Password == ConfirmPassword)
            {
                success = await User.AttemptUserRegister(Email, Password);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords don't match!", "OK");
            }

            if (success)
            {
                AttemptLogin();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to register. User already exists.", "OK");
            }
        }

        private async void AttemptLogin()
        {
            if (await User.AttemptUserLogin(Email, Password))
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                await App.Current.MainPage.DisplayAlert("WELCOME", "Welcome new user! Nice to have you here!", "Thanks!");
            }
        }
    }
}
