using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyInventoryApp.Droid.Renderers;
using MyInventoryApp.Services.Camera;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CameraPage), typeof(CameraPageRenderer))]
namespace MyInventoryApp.Droid.Renderers
{
    public class CameraPageRenderer : PageRenderer, TextureView.ISurfaceTextureListener
    {
        RelativeLayout mainLayout;
        TextureView liveView;
        PaintCodeButton capturePhotoButton;

        Android.Hardware.Camera camera;

        Activity Activity => this.Context as Activity;

        public CameraPageRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            SetupUserInterface();
            SetupEventHandlers();
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (!changed)
                return;
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            mainLayout.Measure(msw, msh);
            mainLayout.Layout(0, 0, r - l, b - t);

            capturePhotoButton.SetX(mainLayout.Width / 2 - 60);
            capturePhotoButton.SetY(mainLayout.Height - 200);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                (Element as CameraPage).Cancel();
                return false;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public void SetupEventHandlers()
        {
            capturePhotoButton.Click += async (sender, e) =>
            {
                var bytes = await TakePhoto();
                (Element as CameraPage).SetPhotoResult(bytes, liveView.Bitmap.Width, liveView.Bitmap.Height);
            };
            liveView.SurfaceTextureListener = this;
        }

        public async Task<byte[]> TakePhoto()
        {
            camera.StopPreview();
            var ratio = ((decimal)Height) / Width;
            var image = Bitmap.CreateBitmap(liveView.Bitmap, 0, 0, liveView.Bitmap.Width, (int)(liveView.Bitmap.Width * ratio));
            byte[] imageBytes = null;
            using (var imageStream = new MemoryStream())
            {
                await image.CompressAsync(Bitmap.CompressFormat.Jpeg, 50, imageStream);
                image.Recycle();
                imageBytes = imageStream.ToArray();
            }
            camera.StartPreview();
            return imageBytes;
        }

        void StopCamera()
        {
            camera.StopPreview();
            camera.Release();
        }

        void StartCamera()
        {
            camera.SetDisplayOrientation(90);
            camera.StartPreview();
        }

        void SetupUserInterface()
        {
            mainLayout = new RelativeLayout(Context);

            liveView = new TextureView(Context);

            RelativeLayout.LayoutParams liveViewParams = new RelativeLayout.LayoutParams(
                LayoutParams.MatchParent,
                LayoutParams.MatchParent);
            liveView.LayoutParameters = liveViewParams;
            mainLayout.AddView(liveView);

            capturePhotoButton = new PaintCodeButton(Context);
            RelativeLayout.LayoutParams captureButtonParams = new RelativeLayout.LayoutParams(
                LayoutParams.WrapContent,
                LayoutParams.WrapContent)
            {
                Height = 120,
                Width = 120
            };

            capturePhotoButton.LayoutParameters = captureButtonParams;
            mainLayout.AddView(capturePhotoButton);

            AddView(mainLayout);
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            camera = Android.Hardware.Camera.Open();
            var parameters = camera.GetParameters();
            var aspect = ((decimal)height) / ((decimal)width);

            // Find the preview aspect ratio that is closest to the surface aspect
            var previewSize = parameters.SupportedPreviewSizes
                                        .OrderBy(s => Math.Abs(s.Width / (decimal)s.Height - aspect))
                                        .First();

            System.Diagnostics.Debug.WriteLine($"Preview sizes: {parameters.SupportedPreviewSizes.Count}");

            parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
            camera.SetParameters(parameters);

            camera.SetPreviewTexture(surface);
            StartCamera();
        }

        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            StopCamera();
            return true;
        }

        public void OnSurfaceTextureSizeChanged(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
        }

        public void OnSurfaceTextureUpdated(Android.Graphics.SurfaceTexture surface)
        {
        }
    }
}
