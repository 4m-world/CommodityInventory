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
            //{
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 },
            //    new CommodityItem{ Id = Guid.NewGuid().ToString("N"), Name ="Commodity Name", AltName = "Commodity AltName", Barcode="1234567890", IsSyncronized=false, Unit = new Unit{ Abbr="g", UnitType= UnitType.Mass}, UnitValue = 200.5, Price = 50.2 }
            //};
        }

        ObservableCollection<CommodityItem> _commodityItems;

        ICommand _addCommand;
        ICommand _viewCommand;
        ICommand _deleteCommand;
        ICommand _loadMoreCommand;
        ICommand _settingsCommand;

        public ObservableCollection<CommodityItem> CommodityItems
        {
            get => _commodityItems;
            set => UpdateAndNotifyOnChange(ref _commodityItems, value);
        }

        public ICommand SettingsCommad
        {
            get => _settingsCommand = _settingsCommand ?? new DelegateCommandAsync(SettingsCommandExecute);
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

        async Task SettingsCommandExecute()
        {
            await _navigationService.NavigateToAsync<SettingsViewModel>();
        }

        void AddCommandExecute()
        {
            _navigationService.NavigateTo<AddCommodityViewModel>();
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
            IsBusy = true;

            return Task.Run(async() =>
            {
                await Task.Delay(2500);
                //CommodityItems.Add(new CommodityItem { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N").Substring(1, 8), AltName = "Commodity AltName", Barcode = "1234567890", IsSyncronized = false, Unit = new Unit { Abbr = "g", UnitType = UnitType.Mass }, UnitValue = 200.5, Price = 50.2 });
                //CommodityItems.Add(new CommodityItem { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N").Substring(1, 8), AltName = "Commodity AltName", Barcode = "1234567890", IsSyncronized = false, Unit = new Unit { Abbr = "g", UnitType = UnitType.Mass }, UnitValue = 200.5, Price = 50.2 });
                //CommodityItems.Add(new CommodityItem { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N").Substring(1, 8), AltName = "Commodity AltName", Barcode = "1234567890", IsSyncronized = false, Unit = new Unit { Abbr = "g", UnitType = UnitType.Mass }, UnitValue = 200.5, Price = 50.2 });
                //CommodityItems.Add(new CommodityItem { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N").Substring(1, 8), AltName = "Commodity AltName", Barcode = "1234567890", IsSyncronized = false, Unit = new Unit { Abbr = "g", UnitType = UnitType.Mass }, UnitValue = 200.5, Price = 50.2 });
                //CommodityItems.Add(new CommodityItem { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N").Substring(1, 8), AltName = "Commodity AltName", Barcode = "1234567890", IsSyncronized = false, Unit = new Unit { Abbr = "g", UnitType = UnitType.Mass }, UnitValue = 200.5, Price = 50.2 });
                //CommodityItems.Add(new CommodityItem { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N").Substring(1, 8), AltName = "Commodity AltName", Barcode = "1234567890", IsSyncronized = false, Unit = new Unit { Abbr = "g", UnitType = UnitType.Mass }, UnitValue = 200.5, Price = 50.2 });
                //CommodityItems.Add(new CommodityItem { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N").Substring(1, 8), AltName = "Commodity AltName", Barcode = "1234567890", IsSyncronized = false, Unit = new Unit { Abbr = "g", UnitType = UnitType.Mass }, UnitValue = 200.5, Price = 50.2 });
                IsBusy = false;
            });
        }
    }
}
