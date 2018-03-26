using System;
using System.Globalization;
using System.Reflection;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.Api.Services.RequestProvider;
using MyInventoryApp.Api.Services.Unit;
using MyInventoryApp.Services.BarcodeScanner;
using MyInventoryApp.Services.Camera;
using MyInventoryApp.Services.Dialog;
using MyInventoryApp.Services.FileHelper;
using MyInventoryApp.Services.Internet;
using MyInventoryApp.Services.Navigation;
using MyInventoryApp.Services.Settings;
using MyInventoryApp.Services.Share;
using MyInventoryApp.Services.Storage;
using Unity;
using Xamarin.Forms;

namespace MyInventoryApp.ViewModels.Base
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", 
                                            typeof(bool), 
                                            typeof(ViewModelLocator), 
                                            default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Pages.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }

        static IUnityContainer _container;

        static ViewModelLocator()
        {
            _container = new UnityContainer();

            // view models
            _container.RegisterType<MainViewModel>();
            _container.RegisterType<CommodityViewModel>();
            _container.RegisterType<SettingsViewModel>();
            _container.RegisterType<UnitsViewModel>();
            _container.RegisterType<SyncViewModel>();

            _container.RegisterInstance(DependencyService.Get<IDialogService>());
            _container.RegisterInstance(DependencyService.Get<IInternetService>());
            _container.RegisterInstance(DependencyService.Get<IFileHelper>());
            _container.RegisterInstance(DependencyService.Get<IShareService>());
            // _container.RegisterInstance(DependencyService.Get<IStorageService>());

            _container.RegisterType<IBarcodeScannerService, DefaultBarcodeScannerServices>();
            // Todo: Implemnt camera capture using services
            //_container.RegisterType<ICameraService, CameraService>();
            _container.RegisterType<ISettingsService, SettingsService>();
            _container.RegisterType<Services.Dependency.IDependencyService, Services.Dependency.DependencyService>();

            // services
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<IRequestProvider, RequestProvider>();
            _container.RegisterType<ICommodityService, LocalCommodityService>();
            _container.RegisterType<IUnitService, LocalUnitService>();
        }

        public static MainViewModel MainViewModel
        {
            get => _container.Resolve<MainViewModel>();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
