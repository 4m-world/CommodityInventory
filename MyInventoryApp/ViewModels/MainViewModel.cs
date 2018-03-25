using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.Services.Dialog;
using MyInventoryApp.Services.Internet;
using MyInventoryApp.Services.Navigation;
using MyInventoryApp.ViewModels.Base;

namespace MyInventoryApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly IInternetService _internetService;
        readonly INavigationService _navigationService;
        readonly ICommodityService _commodityService;
        readonly IDialogService _dialogService;

        int _nextPageIndex;
        bool _isReloading;
        ObservableCollection<CommodityItem> _commodityItems;


        ICommand _viewCommand;
        ICommand _deleteCommand;

        ICommand _reloadCommand;
        ICommand _settingsCommand;
        ICommand _addCommand;
        ICommand _loadMoreCommand;

        public MainViewModel(
            ICommodityService commodityService,
            INavigationService navigationService,
            IInternetService internetService,
            IDialogService dialogService)
        {
            _dialogService = dialogService;
            _internetService = internetService;
            _navigationService = navigationService;
            _commodityService = commodityService;

            CommodityItems = new ObservableCollection<CommodityItem>();
        }

        public ObservableCollection<CommodityItem> CommodityItems
        {
            get => _commodityItems;
            set => UpdateAndNotifyOnChange(ref _commodityItems, value);
        }

        public bool IsReloading
        {
            get => _isReloading;
            set => UpdateAndNotifyOnChange(ref _isReloading, value);
        }

        public ICommand ReloadCommand
        {
            get => _reloadCommand = _reloadCommand ?? new DelegateCommandAsync(ReloadExecute);
        }

        public ICommand SettingsCommad
        {
            get => _settingsCommand = _settingsCommand ?? new DelegateCommandAsync(SettingsCommandExecute);
        }

        public ICommand AddCommand 
        {
            get => _addCommand = _addCommand ?? new DelegateCommand(AddCommandExecute);
        }

        public ICommand LoadMoreCommand
        {
            get => _loadMoreCommand = _loadMoreCommand ?? new DelegateCommandAsync(LoadMoreExecute);
        }

        public ICommand ViewCommand
        {
            get => _viewCommand = _viewCommand ?? new DelegateCommand<CommodityItem>(PreviewCommandExecute);
        }

        public ICommand DeleteCommand
        {
            get => _deleteCommand = _deleteCommand ?? new DelegateCommandAsync<CommodityItem>(DeleteCommandExecute);
        }



        async Task SettingsCommandExecute()
        {
            await _navigationService.NavigateToAsync<SettingsViewModel>();
        }

        void AddCommandExecute()
        {
            _navigationService.NavigateTo<CommodityViewModel>();
        }

        void PreviewCommandExecute(CommodityItem commodity)
        {
            //_navigationService.NavigateTo<CommodityDetailsViewModel>(new CommodityItem());
        }

        Task DeleteCommandExecute(CommodityItem commodity)
        {
            return Task.FromResult(false);
        }

        async Task LoadMoreExecute()
        {
            if (IsBusy) return;
            IsBusy = true;

            var result = await _commodityService.GetCommoditiesAsync(pageIndex: _nextPageIndex);
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {

                    CommodityItems.Add(item);
                }

                RaisePropertyChanged(() => CommodityItems);
            }
            _nextPageIndex++;
            IsBusy = false;
        }

		public override async Task Initalize(object navigationContext)
		{
            base.Initalize(navigationContext).Wait();
            await ReloadExecute();
		}

		public override async void OnAppearing(object navigationContext)
		{
            base.OnAppearing(navigationContext);
            await Initalize(navigationContext);
		}

		async Task ReloadExecute()
        {
            if (IsBusy) return;
            CommodityItems.Clear();
            _nextPageIndex = 0;
            await LoadMoreExecute();
            IsReloading = false;
        }
	}
}
