﻿using System;
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
        }

        ObservableCollection<CommodityItem> _commodityItems;

        ICommand _addCommand;
        ICommand _viewCommand;
        ICommand _deleteCommand;
        ICommand _loadMoreCommand;

        public ObservableCollection<CommodityItem> CommodityItems
        {
            get => _commodityItems;
            set => UpdateAndNotifyOnChange(ref _commodityItems, value);
        }

        public ICommand AddCommand 
        {
            get => _addCommand = _addCommand ?? new DelegateCommand(AddCommandExecute);
        }

        public ICommand ViewCommand
        {
            get => _viewCommand = _viewCommand ?? new DelegateCommand<CommodityItem>(PreviewCommandExecute);
        }

        public ICommand DeleteCommand
        {
            get => _deleteCommand = _deleteCommand ?? new DelegateCommandAsync<CommodityItem>(DeleteCommandExecute);
        }

        public ICommand LoadMoreCommand
        {
            get => _loadMoreCommand = _loadMoreCommand ?? new DelegateCommandAsync(LoadMoreCommandExecute);
        }

        void AddCommandExecute()
        {
            //_navigationService.NavigateTo<NewCommodityViewModel>(new CommodityItem());
        }

        void PreviewCommandExecute(CommodityItem commodity)
        {
            //_navigationService.NavigateTo<CommodityDetailsViewModel>(new CommodityItem());
        }

        Task DeleteCommandExecute(CommodityItem commodity)
        {
            return Task.FromResult(false);
        }

        Task LoadMoreCommandExecute()
        {
            return Task.FromResult(false);
        }
    }
}