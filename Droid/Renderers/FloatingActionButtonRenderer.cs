//using System;
//using System.ComponentModel;
//using System.IO;
//using System.Threading.Tasks;
//using Android.App;
//using Android.Content;
//using Android.Content.Res;
//using Android.Graphics;
//using Android.Support.V4.View;
//using Android.Views;
//using Android.Widget;
//using MyInventoryApp.Controls;
//using MyInventoryApp.Droid.Controls;
//using MyInventoryApp.Droid.Renderers;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;


//[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]
//namespace MyInventoryApp.Droid.Renderers
//{
//    public class FloatingActionButtonRenderer : ViewRenderer<FloatingActionButton, FrameLayout>
//    {
//        public FloatingActionButtonRenderer(Context context)
//            :base(context)
//        {
            
//        }

//		protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
//		{
//            base.OnElementChanged(e);

//            if (Control == null)
//            {
//                ViewGroup.SetClipChildren(false);
//                ViewGroup.SetClipToPadding(false);
//                UpdateControlForSize();

//                UpdateStyle();
//            }

//            if (e.NewElement != null)
//            {
//                Control.Click += Fab_Click;
//            }
//            else if (e.OldElement != null)
//            {
//                Control.Click -= Fab_Click;
//            }
//		}

//		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//            if (e.PropertyName == FloatingActionButton.SizeProperty.PropertyName)
//            {
//                this.UpdateControlForSize();
//            }
//            else if (e.PropertyName == FloatingActionButton.NormalColorProperty.PropertyName ||
//                     e.PropertyName == FloatingActionButton.RippleColorProperty.PropertyName ||
//                     e.PropertyName == FloatingActionButton.DisabledColorProperty.PropertyName)
//            {
//                this.SetBackgroundColors();
//            }
//            else if (e.PropertyName == FloatingActionButton.HasShadowProperty.PropertyName)
//            {
//                this.SetHasShadow();
//            }
//            else if (e.PropertyName == FloatingActionButton.SourceProperty.PropertyName)
//            {
//                this.SetImage();
//            }
//            else if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
//            {
//                this.UpdateEnabled();
//            }
//            else
//            {
//                base.OnElementPropertyChanged(sender, e);
//            }
//		}

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                this.Control.Click -= this.Fab_Click;
//            }

//            base.Dispose(disposing);
//        }

//        void UpdateControlForSize()
//        {
//            LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);

//            FloatingActionButton fab = null;

//            if (this.Element.Size  ==  FloatingActionButtonSize.Mini)
//            {
//                fab = (FloatingActionButton)inflater.Inflate(FAB.Droid.Resource.Layout.mini_fab, null);
//            }
//            else // then normal
//            {
//                fab = (FloatingActionButton)inflater.Inflate(FAB.Droid.Resource.Layout.normal_fab, null);
//            }

//            this.SetNativeControl(fab);
//            this.UpdateStyle();
//        }

//        void UpdateStyle()
//        {
//            this.SetBackgroundColors();

//            this.SetHasShadow();

//            this.SetImage();

//            this.UpdateEnabled();
//        }

//        void SetBackgroundColors()
//        {
//            this.Control.BackgroundTintList = ColorStateList.ValueOf(this.Element.NormalColor.ToAndroid());
//            try
//            {
//                this.Control.SetRippleColor(this.Element.RippleColor.ToAndroid());
//            }
//            catch (MissingMethodException)
//            {
//                // ignore
//            }
//        }

//        void SetHasShadow()
//        {
//            try
//            {
//                if (this.Element.HasShadow)
//                {
//                    ViewCompat.SetElevation(this.Control, 20);
//                }
//                else
//                {
//                    ViewCompat.SetElevation(this.Control, 0);
//                }
//            }
//            catch { }
//        }

//        void SetImage()
//        {
//            Task.Run(async () =>
//            {
//                var bitmap = await this.GetBitmapAsync(this.Element.Source);

//                (this.Context as Activity).RunOnUiThread(() =>
//                {
//                    this.Control?.SetImageBitmap(bitmap);
//                });
//            });
//        }

//        void UpdateEnabled()
//        {
//            this.Control.Enabled = this.Element.IsEnabled;

//            if (this.Control.Enabled == false)
//            {
//                this.Control.BackgroundTintList = ColorStateList.ValueOf(this.Element.DisabledColor.ToAndroid());
//            }
//            else
//            {
//                this.UpdateBackgroundColor();
//            }
//        }

//        async Task<Bitmap> GetBitmapAsync(ImageSource source)
//        {
//            var handler = GetHandler(source);
//            var returnValue = (Bitmap)null;

//            returnValue = await handler.LoadImageAsync(source, this.Context);

//            return returnValue;
//        }

//        void Fab_Click(object sender, EventArgs e)
//        {
//            Element.Command?.Execute(EventArgs.Empty);
//        }
//	}
//}
