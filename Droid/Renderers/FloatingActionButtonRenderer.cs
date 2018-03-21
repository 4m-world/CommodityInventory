using System;
using System.IO;
using Android.Content;
using Android.Views;
using Android.Widget;
using MyInventoryApp.Controls.Fab;
using MyInventoryApp.Droid.Controls.Fab;
using MyInventoryApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

/*
 * https://github.com/jamesmontemagno/FloatingActionButton-for-Xamarin.Android/
 */

[assembly: ExportRenderer(typeof(FloatingActionButtonView), typeof(FloatingActionButtonRenderer))]
namespace MyInventoryApp.Droid.Renderers
{
    public class FloatingActionButtonRenderer : ViewRenderer<FloatingActionButtonView, FrameLayout>
    {
        const int MARGIN_DIPS = 16;
        const int FAB_HEIGHT_NORMAL = 56;
        const int FAB_HEIGHT_MINI = 40;
        const int FAB_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        const int FAB_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_NORMAL;
        const int FAB_MINI_FRAME_HEIGHT_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;
        const int FAB_MINI_FRAME_WIDTH_WITH_PADDING = (MARGIN_DIPS * 2) + FAB_HEIGHT_MINI;

        readonly FloatingActionButton fab;

        public FloatingActionButtonRenderer(Context context)
            : base(context)
        {
            float d = context.Resources.DisplayMetrics.Density;
            var margin = (int)(MARGIN_DIPS * d); // margin in pixels

            fab = new FloatingActionButton(context);
            var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            {
                Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal,
                LeftMargin = margin,
                TopMargin = margin,
                BottomMargin = margin,
                RightMargin = margin
            };
            fab.LayoutParameters = lp;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButtonView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= HandlePropertyChanged;

            if (this.Element != null)
            {
                //UpdateContent ();
                this.Element.PropertyChanged += HandlePropertyChanged;
            }

            Element.Show = Show;
            Element.Hide = Hide;

            SetFabImage(Element.ImageName);
            SetFabSize(Element.Size);

            fab.ColorNormal = Element.ColorNormal.ToAndroid();
            fab.ColorPressed = Element.ColorPressed.ToAndroid();
            fab.ColorRipple = Element.ColorRipple.ToAndroid();
            fab.HasShadow = Element.HasShadow;
            fab.Click += Fab_Click;

            var frame = new FrameLayout(Context);
            frame.RemoveAllViews();
            frame.AddView(fab);

            SetNativeControl(frame);
        }

        public void Show(bool animate = true) =>
            fab?.Show(animate);

        public void Hide(bool animate = true) =>
            fab?.Hide(animate);

        void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
            {
                Tracker.UpdateLayout();
            }
            else if (e.PropertyName == FloatingActionButtonView.ColorNormalProperty.PropertyName)
            {
                fab.ColorNormal = Element.ColorNormal.ToAndroid();
            }
            else if (e.PropertyName == FloatingActionButtonView.ColorPressedProperty.PropertyName)
            {
                fab.ColorPressed = Element.ColorPressed.ToAndroid();
            }
            else if (e.PropertyName == FloatingActionButtonView.ColorRippleProperty.PropertyName)
            {
                fab.ColorRipple = Element.ColorRipple.ToAndroid();
            }
            else if (e.PropertyName == FloatingActionButtonView.ImageNameProperty.PropertyName)
            {
                SetFabImage(Element.ImageName);
            }
            else if (e.PropertyName == FloatingActionButtonView.SizeProperty.PropertyName)
            {
                SetFabSize(Element.Size);
            }
            else if (e.PropertyName == FloatingActionButtonView.HasShadowProperty.PropertyName)
            {
                fab.HasShadow = Element.HasShadow;
            }
        }

        void SetFabImage(string imageName)
        {
            if (!string.IsNullOrWhiteSpace(imageName))
            {
                try
                {
                    var drawableNameWithoutExtension = Path.GetFileNameWithoutExtension(imageName).ToLower();
                    var resources = Context.Resources;
                    var imageResourceName = resources.GetIdentifier(drawableNameWithoutExtension, "drawable", Context.PackageName);
                    fab.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeResource(Context.Resources, imageResourceName));
                }
                catch (Exception ex)
                {
                    throw new FileNotFoundException("There was no Android Drawable by that name.", ex);
                }
            }
        }

        void SetFabSize(FloatingActionButtonSize size)
        {
            if (size == FloatingActionButtonSize.Mini)
            {
                fab.Size = FabSize.Mini;
                Element.WidthRequest = FAB_MINI_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_MINI_FRAME_HEIGHT_WITH_PADDING;
            }
            else
            {
                fab.Size = FabSize.Normal;
                Element.WidthRequest = FAB_FRAME_WIDTH_WITH_PADDING;
                Element.HeightRequest = FAB_FRAME_HEIGHT_WITH_PADDING;
            }
        }

        void Fab_Click(object sender, EventArgs e)
        {
            Element?.Clicked?.Invoke(sender, e);

            if (Element?.Command?.CanExecute(Element?.CommandParameter) ?? false)
            {
                Element.Command.Execute(Element?.CommandParameter);
            }
        }
    }
}
