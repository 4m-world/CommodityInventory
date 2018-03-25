using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.Services.BarcodeScanner;
using MyInventoryApp.Services.Camera;
using MyInventoryApp.Services.Dialog;
using MyInventoryApp.Services.Internet;
using MyInventoryApp.Services.Navigation;
using MyInventoryApp.Validations;
using MyInventoryApp.ViewModels.Base;
using System.Linq;
using MyInventoryApp.Api.Services.Unit;

namespace MyInventoryApp.ViewModels
{
    public class CommodityViewModel : BaseViewModel
    {
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
        readonly IDialogService _dialogService;
        readonly IBarcodeScannerService _barcodeScannerService;
        readonly ICommodityService _commodityService;
        readonly IUnitService _unitService;

        ObservableCollection<Unit> _units;
        bool _isValid;

        ICommand _saveCommand;
        ICommand _capturCommand;
        ICommand _scanBarcodeCommand;
        ICommand _validateBarcodeCommand;
        ICommand _validateName;
        ICommand _validateAltNameCommand;
        ICommand _validateUnitCommand;
        ICommand _validateUnitValueCommand;
        ICommand _validatePriceCommand;

        public int UnitSelectedIndex { get; set; }

        public CommodityViewModel(
            ICommodityService commodityService,
            IUnitService unitService,
            IBarcodeScannerService barcodeScannerService,
            INavigationService navigationService,
            IInternetService internetService,
            IDialogService dialogService)
        {
            _dialogService = dialogService;
            _internetService = internetService;
            _navigationService = navigationService;
            _commodityService = commodityService;
            _unitService = unitService;
            _barcodeScannerService = barcodeScannerService;

            AddValidations();
        }

        public ObservableCollection<Unit> Units
        {
            get => _units;
            set => UpdateAndNotifyOnChange(ref _units, value);
        }

        public ICommand ScanBarcodeCommand
        {
            get => _scanBarcodeCommand = _scanBarcodeCommand ?? new DelegateCommandAsync(ScanBarcodeExecute);
        }

        public ICommand CaptureCommand
        {
            get => _capturCommand = _capturCommand ?? new DelegateCommandAsync(CaptureExecute);
        }

        public ICommand ValidateBarcodeCommand
        {
            get => _validateBarcodeCommand = _validateBarcodeCommand ?? new DelegateCommand(() =>
            {
                _barcode.Validate();
            });
        }

        public ICommand ValidateNameCommand
        {
            get => _validateName = _validateName ?? new DelegateCommand(() => _name.Validate());
        }

        public ICommand ValidateAltNameCommand
        {
            get => _validateAltNameCommand = _validateAltNameCommand ?? new DelegateCommand(() => _altName.Validate());
        }

        public ICommand ValidateUnitCommand
        {
            get => _validateUnitCommand = _validateUnitCommand ?? new DelegateCommand(() => _unit.Validate());
        }

        public ICommand ValidateUnitValueCommand
        {
            get => _validateUnitValueCommand = _validateUnitValueCommand ?? new DelegateCommand(() => _unitValue.Validate());
        }

        public ICommand ValidatePriceCommand
        {
            get => _validatePriceCommand = _validatePriceCommand ?? new DelegateCommand(() => _price.Validate());
        }

        public ICommand SaveCommand
        {
            get => _saveCommand = _saveCommand ?? new DelegateCommandAsync(SaveExecute);
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

        public ValidatableObject<string> Notes
        {
            get => _notes;
            set => UpdateAndNotifyOnChange(ref _notes, value);
        }

        public bool IsValid
        {
            get => _isValid;
            set => UpdateAndNotifyOnChange(ref _isValid, value);
        }

        #region Command Execution Methods

        async Task ScanBarcodeExecute()
        {
            var barcode = await _barcodeScannerService.ReadBarcodeAsync();

            Barcode.Value = barcode;
        }

        async Task CaptureExecute()
        {
            var cameraPage = new CameraPage();
            cameraPage.OnPhotoResult += CameraPage_OnPhotoResult;
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(cameraPage);
        }

        async Task SaveExecute()
        {
            if (IsBusy) return;
            IsBusy = true;
            IsValid = ValidateAll();
            if (!IsValid)
            {
                IsBusy = false; return;
            }

            var commodity = new CommodityItem
            {
                Name = Name.Value,
                AltName = AltName.Value,
                Barcode = Barcode.Value,
                Unit = Unit.Value,
                UnitValue = UnitValue.Value,
                UnitId = Unit.Value.Id,
                Price = Price.Value,
                Notes = Notes.Value,
                Image = Image.Value,
                CreateUtcDate = DateTime.UtcNow,
                IsSyncronized = false
            };

            await _commodityService.AddCommodityItemAsync(commodity);
            _navigationService.NavigateBack();
        }

        #endregion

        void AddValidations()
        {
            _name.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Name is required." });
            _altName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Atlternative name is required." });
            _barcode.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "The barcode is required." });
            _unit.Validations.Add(new IsNotNullOrEmptyRule<Unit>() { ValidationMessage = "Measurment unit is required." });
            _unitValue.Validations.Add(new NumericValueWithinRange() { ValidationMessage = string.Format("Measurment value must be between {0}  and {1}.", 1, 9999), MinRange = 1, MaxRange = 9999 });
            _price.Validations.Add(new NumericValueWithinRange() { ValidationMessage = string.Format("Price value must be between {0}  and {1}.", 1, 200), MinRange = 1, MaxRange = 9999 });
        }

        bool ValidateAll()
        {
            return _barcode.Validate() &&
                   _name.Validate() &&
                   _altName.Validate() &&
                   _unit.Validate() &&
                   _unitValue.Validate() &&
                   _price.Validate();
        }

        async void CameraPage_OnPhotoResult(PhotoResultEventArgs result)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopModalAsync();

            if (!result.Success)
                return;

            Image.Value = Convert.ToBase64String(result.Image);
        }


        public override async Task Initalize(object navigationContext)
        {
            if (IsBusy) return;
            IsBusy = true;
            Units = await _unitService.GetUnitsAsync();
            IsBusy = false;
        }

    }
}
