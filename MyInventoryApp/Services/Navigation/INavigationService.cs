
using System.Threading.Tasks;

namespace MyInventoryApp.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(object navigationContext = null);

        void NavigateBack();

        void NavigateBackToFirst();

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>(object navigationContext = null);
    }
}
