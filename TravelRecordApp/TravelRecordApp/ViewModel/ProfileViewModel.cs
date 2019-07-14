using System;
using System.Collections.Generic;
using System.ComponentModel;
using TravelRecordApp.Helpers;
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
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(PostCount), PropertyChanged);
            }
        }

        private Dictionary<string, int> postsPerCategory;
        public Dictionary<string, int> PostsPerCategory
        {
            get { return postsPerCategory; }
            set
            {
                postsPerCategory = value;
                PropertyChangedHelper.RaisePropertyChangedEvent(nameof(PostsPerCategory), PropertyChanged);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void UpdateViewData()
        {
            var posts = await Post.GetUserPosts();
            PostCount = posts.Count.ToString();
            PostsPerCategory = Post.GetPostsPerCategory(posts);
        }
    }
}
