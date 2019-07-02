using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void AddTravelPageTbi_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTravelPage());
        }
    }
}