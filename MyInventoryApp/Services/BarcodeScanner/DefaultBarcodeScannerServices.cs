using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace MyInventoryApp.Services.BarcodeScanner
{
    /// <summary>
    /// Default barcode scanner services.
    /// </summary>
    public class DefaultBarcodeScannerServices
        : ContentPage, IBarcodeScannerService
    {
        /// <summary>
        /// Gets the scanner view.
        /// </summary>
        protected ZXingScannerView ScannerView { get; private set; }

        bool HasResult { get; set; }

        Result Result { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:MyInventoryApp.Services.BarcodeScanner.DefaultBarcodeScannerServices"/> class.
        /// </summary>
        public DefaultBarcodeScannerServices()
        {
            Initalize();
        }

        /// <summary>
        /// Initalize this instance.
        /// </summary>
        public void Initalize()
        {
            ScannerView = new ZXingScannerView()
            {
                AutomationId = "zxingScannerView",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Options = GetScannerOptions()
            };

            ScannerView.OnScanResult += OnScanResult;

            var scannerOverlay = GetScannerOverlay();

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            grid.Children.Add(ScannerView);

            if (scannerOverlay != null)
                grid.Children.Add(scannerOverlay);

            Content = grid;
        }

        /// <summary>
        /// Reads the barcode.
        /// </summary>
        /// <returns>The barcode string.</returns>
        public async Task<string> ReadBarcodeAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(this);
            await Task.Run(() => { while (!HasResult) { } });
            Application.Current.MainPage.Navigation.RemovePage(this);
            return Result?.Text;
        }

        /// <summary>
        /// Reads the barcode result.
        /// </summary>
        /// <returns>The barcode result.</returns>
        public async Task<Result> ReadBarcodeResultAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(this);
            await Task.Run(() => { while (!HasResult) { } });
            Application.Current.MainPage.Navigation.RemovePage(this);
            return Result;
        }

        /// <summary>
        /// Tops the text of the overlay.
        /// </summary>
        protected virtual string TopText() => string.Empty;

        /// <summary>
        /// Bottoms the text of the overlay.
        /// </summary>
        protected virtual string BottomText() => string.Empty;

        /// <summary>
        /// Gets the scanner options.
        /// </summary>
        /// <returns>The scanner options.</returns>
        protected virtual MobileBarcodeScanningOptions GetScannerOptions()
        {
            return new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                TryHarder = true,
                UseNativeScanning = true,
                UseFrontCameraIfAvailable = false
            };
        }

        /// <summary>
        /// Gets the scanner overlay.
        /// </summary>
        protected virtual View GetScannerOverlay()
        {
            var overlay = new ZXingDefaultOverlay
            {
                TopText = TopText(),
                BottomText = BottomText(),
                ShowFlashButton = ScannerView.HasTorch,
                AutomationId = "zxingDefaultOverlay"
            };

            overlay.FlashButtonClicked += (sender, e) =>
            {
                ScannerView.IsTorchOn = !ScannerView.IsTorchOn;
            };

            return overlay;
        }

        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            HasResult = false;
            Result = null;
            ScannerView.IsScanning = true;
        }

        /// <summary>
        /// Ons the disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            HasResult = true;
            ScannerView.IsScanning = false;
        }

        /// <summary>
        /// Ons the scan result.
        /// </summary>
        /// <param name="result">Result.</param>
        void OnScanResult(Result result)
        {
            Result = result;
            HasResult = true;
        }

    }
}
