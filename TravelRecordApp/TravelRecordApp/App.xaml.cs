using Microsoft.WindowsAzure.MobileServices;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DatabasePath = string.Empty;

        // [sic!]
        private const string AZURE_WEB_SERVICE = "https://travaelrecordapp.azurewebsites.net";

        public static MobileServiceClient MobileService = new MobileServiceClient(AZURE_WEB_SERVICE);

        public static User user = new User();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databasePath)
            : this()
        {
            DatabasePath = databasePath;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
