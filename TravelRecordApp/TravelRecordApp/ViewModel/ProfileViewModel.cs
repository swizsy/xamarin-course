using System;
using System.Collections.Generic;
using System.ComponentModel;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string postCount;
        public string PostCount
        {
            get { return postCount; }
            set
            {
                postCount = value;
                RaisePropertyChangedEvent("PostCount");
            }
        }

        private Dictionary<string, int> postsPerCategory;
        public Dictionary<string, int> PostsPerCategory
        {
            get { return postsPerCategory; }
            set
            {
                postsPerCategory = value;
                RaisePropertyChangedEvent("PostsPerCategory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void UpdateViewData()
        {
            var posts = await Post.GetUserPosts();
            PostCount = posts.Count.ToString();
            PostsPerCategory = Post.GetPostsPerCategory(posts);
        }

        private void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
