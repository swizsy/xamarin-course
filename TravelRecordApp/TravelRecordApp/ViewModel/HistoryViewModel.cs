using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        private Post selectedPost;
        public Post SelectedPost
        {
            get { return selectedPost; }
            set
            {
                selectedPost = value;
                OnPostSelected();
            }
        }

        private List<Post> posts;
        public List<Post> Posts
        {
            get { return posts; }
            set
            {
                posts = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(Posts), PropertyChanged);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void UpdateViewData()
        {
            Posts = await Post.GetUserPosts();
        }

        private void OnPostSelected()
        {
            App.Current.MainPage.Navigation.PushAsync(new PostDetailsPage(SelectedPost));
        }
    }
}
