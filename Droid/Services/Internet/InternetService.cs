using System;
using Android.App;
using Android.Content;
using Android.Net;
using MyInventoryApp.Droid.Services.Internet;
using MyInventoryApp.Services.Internet;
using Xamarin.Forms;

[assembly:Dependency(typeof(InternetService))]
namespace MyInventoryApp.Droid.Services.Internet
{
    public class InternetService : IInternetService
    {
        public bool HasConnection()
        {
            var connectivityManager = (ConnectivityManager)Android.App.Application
                                                                  .Context
                                                                  .GetSystemService(Context.ConnectivityService);

            var activityConnection = connectivityManager.ActiveNetworkInfo;
            return activityConnection != null && activityConnection.IsConnected;
        }
    }
}

