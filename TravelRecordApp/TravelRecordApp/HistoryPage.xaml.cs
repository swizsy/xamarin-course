using SQLite;
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

            List<Post> posts;
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Post>();
                posts = connection.Table<Post>().ToList();
            }

            postsListView.ItemsSource = posts;
        }

        private void PostsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Post selectedPost = postsListView.SelectedItem as Post;
            Navigation.PushAsync(new PostDetailsPage(selectedPost));
        }
    }
}