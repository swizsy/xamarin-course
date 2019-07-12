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
            UpdatePost();
            Navigation.PushAsync(new HomePage());
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            DeletePost();
            Navigation.PushAsync(new HomePage());
        }

        private async void UpdatePost()
        {
            post.Experience = experienceEntry.Text;
            await App.MobileService.GetTable<Post>().UpdateAsync(post);

            #region SQLite Local Database Code
            //using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            //{
            //    post.Experience = experienceEntry.Text;
            //    connection.Update(post);
            //} 
            #endregion
        }

        private async void DeletePost()
        {
            await App.MobileService.GetTable<Post>().DeleteAsync(post);

            #region SQLite Local Database Code
            //using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            //{
            //    connection.Delete(post);
            //} 
            #endregion
        }
    }
}