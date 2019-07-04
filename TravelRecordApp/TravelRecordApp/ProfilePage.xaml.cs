using SQLite;
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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            List<Post> posts;
            List<string> categories;
            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                posts = conn.Table<Post>().ToList();

                categories = posts.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

                //categories = (from p in posts
                //              orderby p.CategoryId
                //              select p.CategoryName).Distinct().ToList();

                foreach (string category in categories)
                {
                    int count = posts.Where(p => p.CategoryName == category).ToList().Count();

                    //int count = (from post in posts
                    //             where post.CategoryName == category
                    //             select post).ToList().Count();

                    categoriesCount.Add(category, count);
                }
            }

            postCountLabel.Text = posts.Count().ToString();
            categoriesListView.ItemsSource = categoriesCount;
        }
    }
}