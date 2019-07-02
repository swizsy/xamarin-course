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
    public partial class PostDetailsPage : ContentPage
    {
        private Post post;
        public PostDetailsPage(Post selectedPost)
        {
            InitializeComponent();
            post = selectedPost;
            experienceEntry.Text = post.Experience;
        }

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                post.Experience = experienceEntry.Text;
                connection.Update(post);
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.Delete(post);
            }
        }
    }
}