using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Api.Services.Unit;
using MyInventoryApp.Services.Navigation;
using MyInventoryApp.ViewModels.Base;

namespace MyInventoryApp.ViewModels
{
    public class UnitsViewModel : BaseViewModel
    {
        int pageIndex = 0;
        bool _isRefreshing;
        readonly IUnitService _unitService;
        readonly INavigationService _navigationService;

        ObservableCollection<Unit> _units;
        ICommand _loadMoreCommand;
        ICommand _viewCommand;
        ICommand _addCommand;
        ICommand _reloadCommand;

        public UnitsViewModel(
            IUnitService unitService,
            INavigationService navigationService)
        {
            _unitService = unitService;
            _navigationService = navigationService;
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => UpdateAndNotifyOnChange(ref _isRefreshing, value);
        }

        public ObservableCollection<Unit> Items
        {
            get => _units;
            set => UpdateAndNotifyOnChange(ref _units, value);
        }

        public override async Task Initalize(object navigationContext)
        {
            if (IsBusy) return;

            _units = new ObservableCollection<Unit>();
            pageIndex = 0;
            await LoadMoreExecute();

        }

        public ICommand ReloadCommand
        {
            get => _reloadCommand = _reloadCommand ?? new DelegateCommandAsync(async () =>
            {
                if (IsBusy) return;
                pageIndex = 0;
                Items.Clear();
                await LoadMoreExecute();
                IsRefreshing = false;
            });
        }

        public ICommand LoadMoreCommand
        {
            get => _loadMoreCommand = _loadMoreCommand ?? new DelegateCommandAsync(LoadMoreExecute);
        }

        async Task LoadMoreExecute()
        {
            if (IsBusy) return;
            IsBusy = true;
            var result = await _unitService.GetUnitsAsync(pageIndex: pageIndex);
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {

                    Items.Add(item);
                }
                RaisePropertyChanged(() => Items);
            }
            pageIndex++;
            IsBusy = false;
        }

        public ICommand ViewCommand
        {
            get => _viewCommand = _viewCommand ?? new DelegateCommandAsync(ViewExecute);
        }

        private Task ViewExecute()
        {
            return Task.FromResult(false);
        }

        public ICommand AddCommand
        {
            get => _addCommand = _addCommand ?? new DelegateCommandAsync(AddExecute);
        }

        private Task AddExecute()
        {
            return Task.FromResult(false);
        }
    }
}
