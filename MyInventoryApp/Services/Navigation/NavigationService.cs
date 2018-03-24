using System;
using System.Globalization;
using System.Reflection;
using MyInventoryApp.ViewModels.Base;
using Xamarin.Forms;

namespace MyInventoryApp.Services.Navigation
{
    /// <summary>
    /// Navigation service.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Navigates to the back.
        /// </summary>
        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Navigates back to the first view.
        /// </summary>
        public void NavigateBackToFirst()
        {
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Navigates to a view.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        /// <typeparam name="TViewModel">The view model of target view</typeparam>
        public void NavigateTo<TViewModel>(object navigationContext = null)
        {
            var page = CreatePage(typeof(TViewModel), navigationContext);
            page.BindingContext = App.Locator.Resolve<TViewModel>();
            (page.BindingContext as BaseViewModel)?.Initalize(navigationContext);
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        /// <summary>
        /// Creates a page based on the view model
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="viewModelType">View model type.</param>
        /// <param name="paramter">Page context paramters</param>
        Page CreatePage(Type viewModelType, object paramter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);
            if(pageType == null){
                throw new Exception($"Cannot locate page for {viewModelType}");
            }

            var page = paramter == null
                ?Activator.CreateInstance(pageType) as Page
                :Activator.CreateInstance(pageType, paramter) as Page;
            return page;
        }

        /// <summary>
        /// Gets the page type for view model.
        /// </summary>
        /// <returns>The page type for view model.</returns>
        /// <param name="viewModelType">View model type.</param>
        Type GetPageTypeForViewModel(Type viewModelType){
            var viewName = viewModelType.FullName
                                        .Replace("ViewModels", "Pages")
                                        .Replace("Model", "");
            var viewModelAssemblyName = viewModelType.GetTypeInfo()
                                                     .Assembly
                                                     .FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture,
                                                 "{0}, {1}",
                                                 viewName,
                                                 viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
    }
}
