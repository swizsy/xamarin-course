using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;
using TravelRecordApp.ViewModel.Interfaces;

namespace TravelRecordApp.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged, IModifyDataViewModel
    {
        public Modification Delete { get { return Modification.DELETE; } }

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

        public ModificationCommand ModCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public HistoryViewModel()
        {
            ModCommand = new ModificationCommand(this);
        }

        public async void UpdateViewData()
        {
            Posts = await Post.GetUserPosts();
        }

        private void OnPostSelected()
        {
            App.Current.MainPage.Navigation.PushAsync(new PostDetailsPage(SelectedPost));
        }

        public void Modify(Modification modification)
        {
            Post.Delete(SelectedPost);
        }
    }
}
