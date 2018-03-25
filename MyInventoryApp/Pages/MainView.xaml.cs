using System;
using System.Collections.Generic;
using MyInventoryApp.Controls.Layout;
using Xamarin.Forms;

namespace MyInventoryApp.Pages
{
    public partial class MainView : ContentPage //StyledTabbedPage
    {
        public MainView()
        {
            InitializeComponent();

            //BindingContext = ViewApp.Locator.MainViewModel;
        }

		protected override void OnAppearing()
		{
            if (BindingContext is MainView mainViewModel)
                mainViewModel.OnAppearing();
		}

		protected override void OnDisappearing()
		{
            if (BindingContext is MainView mainViewModel)
                mainViewModel.OnDisappearing();
		}
	}
}
