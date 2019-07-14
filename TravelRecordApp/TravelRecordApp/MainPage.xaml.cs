using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;

namespace TravelRecordApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();

            viewModel = new MainViewModel();
            BindingContext = viewModel;

            LoadResources();
        }

        private void LoadResources()
        {
            Type assembly = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
        }
    }
}
