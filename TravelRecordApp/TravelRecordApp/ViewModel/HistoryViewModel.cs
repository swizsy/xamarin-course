using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
                RaisePropertyChangedEvent("Posts");
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

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
