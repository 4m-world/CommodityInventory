using Xamarin.Forms;

using MyInventoryApp.ViewModels.Base;
using MyInventoryApp.Pages;

namespace MyInventoryApp
{
    public partial class App : Application
    {
        static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get => _locator = _locator ?? new ViewModelLocator();
        }


        public bool IsNetworkReachable { get; set; }

        public App()
        {
            InitializeComponent();

            InitApp();
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

        void InitApp()
        {
            MainPage = new NavigationPage(new MainView());   
        }
    }
}
