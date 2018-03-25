using Xamarin.Forms;

using MyInventoryApp.ViewModels.Base;
using MyInventoryApp.Pages;
using MyInventoryApp.Services.Navigation;

namespace MyInventoryApp
{
    public partial class App : Application
    {
        public bool IsNetworkReachable { get; set; }

        public App()
        {
            InitializeComponent();

            InitApp();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
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
            //MainPage = new NavigationPage(new MainView());   
        }
    }
}
