using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

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
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Id), PropertyChanged);
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
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
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Password), PropertyChanged);
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
    }
}
