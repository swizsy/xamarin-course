using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DatabasePath = string.Empty;

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
