using System;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        private HomeViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();

            viewModel = new HomeViewModel();
            BindingContext = viewModel;
        }
    }
}