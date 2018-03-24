using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.Services.BarcodeScanner;
using MyInventoryApp.Services.Dialog;
using MyInventoryApp.Services.Internet;
using MyInventoryApp.Services.Navigation;
using MyInventoryApp.Validations;
using MyInventoryApp.ViewModels.Base;

namespace MyInventoryApp.ViewModels
{
    public class AddCommodityViewModel : BaseViewModel
    {
        //CommodityItem _commodiy;

        ValidatableObject<string> _name = new ValidatableObject<string>();
        ValidatableObject<string> _altName = new ValidatableObject<string>();
        ValidatableObject<string> _barcode = new ValidatableObject<string>();
        ValidatableObject<Unit> _unit = new ValidatableObject<Unit>();
        ValidatableObject<double> _unitValue = new ValidatableObject<double>();
        ValidatableObject<double> _price = new ValidatableObject<double>();
        ValidatableObject<string> _notes = new ValidatableObject<string>();
        ValidatableObject<string> _image = new ValidatableObject<string>();

        readonly IInternetService _internetService;
        readonly INavigationService _navigationService;
        readonly ICommodityService _commodityService;
        readonly IDialogService _dialogService;
        readonly IBarcodeScannerService _barcodeScannerService;

        public AddCommodityViewModel(
            ICommodityService commodityService,
            IBarcodeScannerService barcodeScannerService,
            INavigationService navigationService,
            IInternetService internetService,
            IDialogService dialogService)
        {
            _dialogService = dialogService;
            _internetService = internetService;
            _navigationService = navigationService;
            _commodityService = commodityService;
            _barcodeScannerService = barcodeScannerService;
            //_barcodeScannerService?.Initalize();
            AddValidations();
        }

        ICommand _scanBarcodeCommand;

        public ICommand ScanBarcodeCommand
        {
            get => _scanBarcodeCommand = _scanBarcodeCommand ?? new DelegateCommandAsync(ScanBarcodeExecute);
        }


        ICommand _validateName;
        public ICommand ValidateNameCommand
        {
            get => _validateName = _validateName ?? new DelegateCommand(() => _name.Validate());
        }

        public ValidatableObject<string> Name
        {
            get => _name;
            set => UpdateAndNotifyOnChange(ref _name, value);
        }

        public ValidatableObject<string> AltName
        {
            get => _altName;
            set => UpdateAndNotifyOnChange(ref _altName, value);
        }

        public ValidatableObject<string> Barcode
        {
            get => _barcode;
            set => UpdateAndNotifyOnChange(ref _barcode, value);
        }

        public ValidatableObject<Unit> Unit
        {
            get => _unit;
            set => UpdateAndNotifyOnChange(ref _unit, value);
        }

        public ValidatableObject<double> UnitValue
        {
            get => _unitValue;
            set => UpdateAndNotifyOnChange(ref _unitValue, value);
        }

        public ValidatableObject<double> Price
        {
            get => _price;
            set => UpdateAndNotifyOnChange(ref _price, value);
        }

        public ValidatableObject<string> Image
        {
            get => _image;
            set => UpdateAndNotifyOnChange(ref _image, value);
        }


        void AddValidations()
        {
            _name.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Name is required." });
            _altName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Atlternative name is required." });
            _barcode.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "The barcode is required." });
            _unit.Validations.Add(new IsNotNullOrEmptyRule<Unit>() { ValidationMessage = "Measurment unit is required." });
            _unitValue.Validations.Add(new NumericValueWithinRange() { ValidationMessage = string.Format("Measurment value must be between {0}  and {1}.", 1, 9999), MinRange = 1, MaxRange = 9999 });
        }

        #region Command Execution Methods

        async Task ScanBarcodeExecute()
        {
           var barcode = await _barcodeScannerService.ReadBarcodeAsync();

            Barcode.Value = barcode;
        }

        #endregion
    }
}
