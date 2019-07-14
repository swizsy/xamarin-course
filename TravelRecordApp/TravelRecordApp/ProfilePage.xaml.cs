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

            DisplayData();
        }

        private async void DisplayData()
        {
            List<Post> posts = await Post.GetUserPosts();
            Dictionary<string, int> postsPerCategory = Post.GetPostsPerCategory(posts);

            #region SQLite Local Database Code
            /*using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
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
            }*/
            #endregion

            postCountLabel.Text = posts.Count().ToString();
            categoriesListView.ItemsSource = postsPerCategory;
        }
    }
}