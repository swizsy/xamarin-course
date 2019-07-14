using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class User : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {   get { return id; }
            set
            {
                id = value;
                RaisePropertyChangedEvent("Id");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
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
                RaisePropertyChangedEvent("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static async Task<bool> AttemptUserLogin(string email, string password)
        {
            User user = (await App.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();

            if (user != null)
            {
                if (user.Password == password)
                {
                    App.user = user;
                    return true;
                }
            }
            return false;
        }

        public static async Task<bool> AttemptUserRegister(string email, string password)
        {
            bool emailAlreadyExists = (await App.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).Any();

            if (!emailAlreadyExists)
            {
                User user = new User
                {
                    Email = email,
                    Password = password
                };

                await App.MobileService.GetTable<User>().InsertAsync(user);
                return true;
            }

            return false;
        }

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
