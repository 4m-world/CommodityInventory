using System.Threading.Tasks;
using ZXing;

namespace MyInventoryApp.Services.BarcodeScanner
{
    /// <summary>
    /// Barcode scanner service.
    /// </summary>
    public interface IBarcodeScannerService
    {
        void Initalize();
        /// <summary>
        /// Reads the barcode.
        /// </summary>
        /// <returns>The barcode string</returns>
        Task<string> ReadBarcodeAsync();

        /// <summary>
        /// Reads the barcode.
        /// </summary>
        /// <returns>The barcode ZXing result</returns>
        Task<Result> ReadBarcodeResultAsync();
    }
}
