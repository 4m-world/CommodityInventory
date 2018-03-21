using ReactiveUI;
using Splat;

namespace MyInventoryApp.ViewModels
{
    public class BaseViewModel 
        : ReactiveObject
        , IRoutableViewModel
        , ISupportsActivation
    {
        protected readonly ViewModelActivator _viewModelActivator;

        public BaseViewModel(IScreen hostScreen = null)
        {
            _viewModelActivator = new ViewModelActivator();
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }

        public string UrlPathSegment { get; protected set; }

        public IScreen HostScreen { get; protected set; }

        public ViewModelActivator Activator { get; protected set; }
    }
}
