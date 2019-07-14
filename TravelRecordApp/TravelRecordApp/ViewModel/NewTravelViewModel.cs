using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;
using TravelRecordApp.ViewModel.Interfaces;

namespace TravelRecordApp.ViewModel
{
    public class NewTravelViewModel : IModifyDataViewModel
    {
        public Modification Insert { get { return Modification.INSERT; } }

        public Venue SelectedVenue { get; set; }

        public Post Post { get; set; }

        #region Commands
        public ModificationCommand ModCommand { get; set; }
        #endregion

        public NewTravelViewModel()
        {
            ModCommand = new ModificationCommand(this);
            Post = new Post();
        }

        public void Modify(Modification modification)
        {
            SavePost();
        }

        private void SavePost()
        {
            try
            {
                Category firstCategory = SelectedVenue.categories.FirstOrDefault();

                Post.UserId = App.user.Id;
                Post.VenueName = SelectedVenue.name;
                Post.CategoryId = firstCategory.id;
                Post.CategoryName = firstCategory.name;
                Post.Address = SelectedVenue.location.address;
                Post.Latitude = SelectedVenue.location.lat;
                Post.Longitude = SelectedVenue.location.lng;
                Post.Distance = SelectedVenue.location.distance;

                Post.Insert(Post);

                App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Error", "Failed to save experience!", "OK");
            }
        }
    }
}
