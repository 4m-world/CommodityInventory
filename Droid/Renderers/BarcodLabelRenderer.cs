using Android.Content;
using Android.Graphics;
using MyInventoryApp.Controls;
using MyInventoryApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BarcodeLabel), typeof(BarcodLabelRenderer))]
namespace MyInventoryApp.Droid.Renderers
{
    public class BarcodLabelRenderer : LabelRenderer
    {
        public BarcodLabelRenderer(Context context)
            :base(context)
        {
            
        }


		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
            base.OnElementChanged(e);

            if(!string.IsNullOrEmpty(e.NewElement?.FontFamily))
            {
                var font = Typeface.CreateFromAsset(Context.Assets, e.NewElement?.FontFamily + ".ttf");

                Control.Typeface = font;
            }
		}
	}
}
