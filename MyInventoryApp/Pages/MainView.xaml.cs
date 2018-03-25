using System;
using System.Collections.Generic;
using MyInventoryApp.Controls.Layout;
using MyInventoryApp.ViewModels;
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
            if (BindingContext is MainViewModel mainViewModel)
                mainViewModel.OnAppearing(null);
		}

		protected override void OnDisappearing()
		{
            if (BindingContext is MainViewModel mainViewModel)
                mainViewModel.OnDisappearing();
		}
	}
}
