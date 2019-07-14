using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChangedEvent("Id");
            }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                RaisePropertyChangedEvent("UserId");
            }
        }

        private string experience;
        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                RaisePropertyChangedEvent("Experience");
            }
        }

        #region Venue Properties
        private string venueName;
        public string VenueName
        {
            get { return venueName; }
            set
            {
                venueName = value;
                RaisePropertyChangedEvent("VenueName");
            }
        }

        private string categoryId;
        public string CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                RaisePropertyChangedEvent("CategoryId");
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                RaisePropertyChangedEvent("CategoryName");
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                RaisePropertyChangedEvent("Address");
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                RaisePropertyChangedEvent("Latitude");
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                RaisePropertyChangedEvent("Longitude");
            }
        }

        private int distance;
        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                RaisePropertyChangedEvent("Distance");
            }
        }
        #endregion

        public string CoordinatesFormat
        {
            get { return $"Lat: {Latitude}, Lng: {Longitude}"; }
        }

        public string DistanceFormat
        {
            get { return $"{Distance} m"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Database Operations
        public static async Task<List<Post>> GetUserPosts()
        {
            return await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();
        }

        public static async void Insert(Post post)
        {
            await App.MobileService.GetTable<Post>().InsertAsync(post);
        }

        public static async void Update(Post post)
        {
            await App.MobileService.GetTable<Post>().UpdateAsync(post);
        }

        public static async void Delete(Post post)
        {
            await App.MobileService.GetTable<Post>().DeleteAsync(post);
        }
        #endregion

        public static Dictionary<string, int> GetPostsPerCategory(List<Post> posts)
        {
            Dictionary<string, int> postsPerCategory = new Dictionary<string, int>();

            var categories = posts.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

            foreach (string category in categories)
            {
                int count = posts.Where(p => p.CategoryName == category).ToList().Count();
                postsPerCategory.Add(category, count);
            }

            return postsPerCategory;
        }

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
