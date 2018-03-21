using Xamarin.Forms;

namespace MyInventoryApp.Controls
{
    public class CircleImage : Image
    {
        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(
                nameof(BorderThickness),
                typeof(int),
                typeof(CircleImage),
                0
            );

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(
                nameof(BorderColor),
                typeof(Color),
                typeof(CircleImage),
                Color.White
            );
    
        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
    }
}
