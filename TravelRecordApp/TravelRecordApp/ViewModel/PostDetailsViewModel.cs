using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;
using TravelRecordApp.ViewModel.Interfaces;

namespace TravelRecordApp.ViewModel
{
    public class PostDetailsViewModel : IModifyDataViewModel
    {
        public Modification Update { get { return Modification.UPDATE; } }
        public Modification Delete { get { return Modification.DELETE; } }

        public Post Post
        {
            get;
            set;
        }

        #region Commands
        public ModificationCommand ModCommand { get; set; }
        #endregion

        public PostDetailsViewModel(Post post)
        {
            Post = post;
            ModCommand = new ModificationCommand(this);
        }

        private void UpdatePost()
        {
            Post.Update(Post);
            App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        private void DeletePost()
        {
            Post.Delete(Post);
            App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        public void Modify(Modification modification)
        {
            switch (modification)
            {
                case Modification.UPDATE:
                {
                    UpdatePost();
                    break;
                }
                case Modification.DELETE:
                {
                    DeletePost();
                    break;
                }
            }
        }
    }
}
