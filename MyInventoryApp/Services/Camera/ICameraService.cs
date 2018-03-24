using System.Threading.Tasks;

namespace MyInventoryApp.Services.Camera
{
    public interface ICameraService
    {
        Task<byte[]> CapturePhotoBytesAsync();

        Task<string> CapturePhotoStringAsync();
    }
}
