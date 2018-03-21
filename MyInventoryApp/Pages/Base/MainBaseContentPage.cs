using System;
using Xamarin.Forms;

namespace MyInventoryApp.Pages.Base
{
    public class MainBaseContentPage : ContentPage
    {
        public bool HasInitialized { get; private set; }

        public MainBaseContentPage()
        {
            SubscribeToAuthentication();
            SubscribeToIncomingPayload();
            HasInitialized = true;


        }

		protected override void OnAppearing()
		{
            base.OnAppearing();
		}

        void SubscribeToAuthentication()
        {
            MessagingCenter.Subscribe<App>(this, Messages.UserAuthenticated, HandleAuthenticated);
        }

        void SubscribeToIncomingPayload()
        {
            
        }

        void HandleAuthenticated(App obj)
        {
        }

	}
}
