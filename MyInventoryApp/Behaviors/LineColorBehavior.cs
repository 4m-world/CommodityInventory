using System.Linq;
using MyInventoryApp.Effects;
using Xamarin.Forms;

namespace MyInventoryApp.Behaviors
{
    public static class LineColorBehavior
    {
        public static readonly BindableProperty ApplyLineColorProperty = BindableProperty.CreateAttached(
            "ApplyLineColor",
            typeof(bool),
            typeof(LineColorBehavior),
            false,
            propertyChanged: HandleBindingPropertyChangedDelegate
        );

        public static readonly BindableProperty LineColorProperty = BindableProperty.CreateAttached(
            "LineColor",
            typeof(Color),
            typeof(LineColorBehavior),
            Color.Default
        );

        public static bool GetApplyLineColor(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ApplyLineColorProperty);
        }

        public static void SetApplyLineColor(BindableObject bindable, bool value)
        {
            bindable.SetValue(ApplyLineColorProperty, value);
        }

        public static Color GetLineColor(BindableObject bindable)
        {
            return (Color)bindable.GetValue(LineColorProperty);
        }

        public static void SetLineColor(BindableObject bindable, Color value)
        {
            bindable.SetValue(LineColorProperty, value);
        }


        static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is View view)
            {
                var hasLine = (bool)newValue;

                if (hasLine)
                {
                    view.Effects.Add(new EntryLineColorEffect());
                }
                else
                {
                    var effectToRemove = view.Effects.FirstOrDefault(e => e is EntryLineColorEffect);
                    if (effectToRemove != null)
                        view.Effects.Remove(effectToRemove);
                }
            }
        }

    }
}
