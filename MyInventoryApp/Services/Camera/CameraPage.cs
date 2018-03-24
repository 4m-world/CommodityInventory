using Xamarin.Forms;

namespace MyInventoryApp.Services.Camera
{
    public class CameraPage : ContentPage
    {
        public delegate void PhotoResultEventHandler(PhotoResultEventArgs result);

        public event PhotoResultEventHandler OnPhotoResult;

        public void SetPhotoResult(byte[] image, int width = -1, int height = -1)
        {
            OnPhotoResult?.Invoke(new PhotoResultEventArgs(image, width, height));
        }

        public void Cancel()
        {
            OnPhotoResult?.Invoke(new PhotoResultEventArgs());
        }
    }
}
