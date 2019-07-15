using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

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
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Id), PropertyChanged);
            }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(UserId), PropertyChanged);

            }
        }

        private string experience;
        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Experience), PropertyChanged);
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
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(VenueName), PropertyChanged);
            }
        }

        private string categoryId;
        public string CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(CategoryId), PropertyChanged);
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(CategoryName), PropertyChanged);
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Address), PropertyChanged);
            }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Latitude), PropertyChanged);
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Longitude), PropertyChanged);
            }
        }

        private int distance;
        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Distance), PropertyChanged);
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

        private DateTimeOffset createDat;
        public DateTimeOffset CREATEDAT
        {
            get { return createDat; }
            set
            {
                createDat = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(CREATEDAT), PropertyChanged);
            }
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
    }
}
