using System.Collections.Generic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetPostsDataAsync();
        }

        private void PostsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Post selectedPost = postsListView.SelectedItem as Post;
            Navigation.PushAsync(new PostDetailsPage(selectedPost));
        }

        private async void GetPostsDataAsync()
        {
            List<Post> posts;

            posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();

            #region SQLite Local Database Code
            //using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            //{
            //    connection.CreateTable<Post>();
            //    posts = connection.Table<Post>().ToList();
            //} 
            #endregion

            postsListView.ItemsSource = posts;
        }
    }
}