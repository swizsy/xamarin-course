using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel
{
    public class HistoryViewModel
    {
        private List<Post> posts;
        public List<Post> Posts
        {
            get { return posts; }
            set
            {
                posts = value;
                RaisePostsAcquiredEvent();
            }
        }

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

        public event EventHandler<PostsAcquiredEventArgs> PostsAcquiredEvent;

        public class PostsAcquiredEventArgs : EventArgs
        {
            public List<Post> Posts;
            public PostsAcquiredEventArgs(List<Post> posts)
            {
                Posts = posts;
            }
        }

        public HistoryViewModel()
        {
            GetPosts();
        }

        private async void GetPosts()
        {
            Posts = await Post.GetUserPosts();
        }

        private void OnPostSelected()
        {
            App.Current.MainPage.Navigation.PushAsync(new PostDetailsPage(SelectedPost));
        }

        private void RaisePostsAcquiredEvent()
        {
            PostsAcquiredEvent?.Invoke(this, new PostsAcquiredEventArgs(Posts));
        }
    }
}
