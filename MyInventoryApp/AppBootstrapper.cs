using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using ReactiveUI;
using MyInventoryApp.Services;
using MyInventoryApp.ViewModels;
using MyInventoryApp.Views;

using Splat;
using ReactiveUI.XamForms;

namespace MyInventoryApp
{
    public class AppBootstrapper 
        : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        public AppBootstrapper()
        {
            Router = new RoutingState();

            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            Locator.CurrentMutable.Register(() => new LoginService(), typeof(ILoginService));
            Locator.CurrentMutable.Register(() => new ItemService(), typeof(IItemService));

            Locator.CurrentMutable.Register(() => new LoginPage(), typeof(IViewFor<LoginViewModel>));
            Locator.CurrentMutable.Register(() => new ItemsPage(), typeof(IViewFor<ItemsViewModel>));

            this.Router
                .NavigateAndReset
                .Execute(new LoginViewModel(Locator.CurrentMutable.GetService<ILoginService>()))
                .Subscribe();
        }

        public Page CreatePage()
        {
            return new ReactiveUI.XamForms.RoutedViewHost();
        }

    }
}
