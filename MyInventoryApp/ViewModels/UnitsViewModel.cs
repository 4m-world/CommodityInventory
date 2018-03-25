using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.Services.Navigation;
using MyInventoryApp.ViewModels.Base;

namespace MyInventoryApp.ViewModels
{
    public class UnitsViewModel : BaseViewModel
    {
        readonly ICommodityService _commodityService;
        readonly INavigationService _navigationService;

        ObservableCollection<Unit> _units;
        ICommand _loadMoreCommand;
        ICommand _viewCommand;
        ICommand _addCommand;

        public UnitsViewModel(
            ICommodityService commodityService,
            INavigationService navigationService)
        {
            _commodityService = commodityService;
            _navigationService = navigationService;
        }

        public ObservableCollection<Unit> Items
        {
            get => _units;
            set => UpdateAndNotifyOnChange(ref _units, value);
        }

		public override async Task Initalize(object navigationContext)
		{
            IsBusy = true;
            await Task.Delay(200);
            Items = new ObservableCollection<Unit>
            {
                new Unit { Id = 1, Name = "Kilogram", Abbr = "Kg", UnitType = UnitType.Mass }
            };

            IsBusy = false;
		}

        public ICommand LoadMoreCommand
        {
            get => _loadMoreCommand = _loadMoreCommand ?? new DelegateCommandAsync(LoadMoreExecute);
        }

        private Task LoadMoreExecute()
        {
            return Task.FromResult(false);
            //throw new NotImplementedException();
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
