using System;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.Api.Services.RequestProvider;
using MyInventoryApp.Api.Services.Unit;
using MyInventoryApp.Services.Dialog;
using MyInventoryApp.Services.Internet;
using MyInventoryApp.Services.Navigation;
using MyInventoryApp.Services.Settings;
using MyInventoryApp.Services.Storage;
using Unity;
using Xamarin.Forms;

namespace MyInventoryApp.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // view models
            _container.RegisterType<MainViewModel>();

            _container.RegisterInstance(DependencyService.Get<IDialogService>());
            _container.RegisterInstance(DependencyService.Get<IInternetService>());
           // _container.RegisterInstance(DependencyService.Get<IStorageService>());

            _container.RegisterType<ISettingsService, SettingsService>();
            _container.RegisterType<Services.Dependency.IDependencyService, Services.Dependency.DependencyService>();

            // services
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<IRequestProvider, RequestProvider>();
            _container.RegisterType<ICommodityService, CommodityService>();
            _container.RegisterType<IUnitService, UnitService>();
        }

        public MainViewModel MainViewModel
        {
            get => _container.Resolve<MainViewModel>();
        }
    }
}
