using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyInventoryApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IDictionary<Type, Type> _viewModelRouting;

        public NavigationService()
        {
            _viewModelRouting = new Dictionary<Type, Type>();

            //_viewModelRouting.Add
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void NavigateBackToFirst()
        {
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public void NavigateTo<TViewModel>(object navigationContext = null)
        {
            Type pageType = _viewModelRouting[typeof(TViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
