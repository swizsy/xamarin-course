using System;
using System.Collections.Generic;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TravelRecordApp.ViewModel.HistoryViewModel;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryViewModel viewModel;

        public HistoryPage()
        {
            InitializeComponent();

            viewModel = new HistoryViewModel();
            BindingContext = viewModel;
            viewModel.PostsAcquiredEvent += OnPostsAcquired;
        }

        private void OnPostsAcquired(object sender, PostsAcquiredEventArgs e)
        {
            postsListView.ItemsSource = e.Posts;
        }
    }
}