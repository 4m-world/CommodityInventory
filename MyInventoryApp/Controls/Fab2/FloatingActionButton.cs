using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyInventoryApp.Controls
{
    public class FloatingActionButton :View
    {
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(
            nameof(HasShadow),
            typeof(bool),
            typeof(FloatingActionButton),
            true
        );

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size),
            typeof(FloatingActionButtonSize),
            typeof(FloatingActionButton),
            FloatingActionButtonSize.Normal
        );

        public static readonly BindableProperty RippleColorProperty = BindableProperty.Create(
            nameof(RippleColor),
            typeof(Color),
            typeof(FloatingActionButton),
            Color.White
        );

        public static readonly BindableProperty PressedColorProperty = BindableProperty.Create(
            nameof(PressedColor),
            typeof(Color),
            typeof(FloatingActionButton),
            Color.White
        );

        public static readonly BindableProperty NormalColorProperty = BindableProperty.Create(
            nameof(NormalColor),
            typeof(Color),
            typeof(FloatingActionButton),
            Color.White
        );

        public static readonly BindableProperty DisabledColorProperty = BindableProperty.Create(
            nameof(DisabledColor),
            typeof(Color),
            typeof(FloatingActionButton),
            Color.White
        );

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(ImageSource),
            typeof(FloatingActionButton),
            null
        );

        public static readonly BindableProperty ClickedIdProperty = BindableProperty.Create(
            nameof(ClickedId),
            typeof(int),
            typeof(FloatingActionButton),
            -1
        );

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(FloatingActionButton),
            null,
            propertyChanged: HandleBindingPropertyChangedDelegate
        );

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameterProperty),
            typeof(object),
            typeof(FloatingActionButton),
            null
        );

        public static readonly BindableProperty DetailProperty = BindableProperty.Create(
            nameof(Detail),
            typeof(string),
            typeof(FloatingActionButton),
            string.Empty
        );

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public FloatingActionButtonSize Size
        {
            get => (FloatingActionButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public Color RippleColor
        {
            get => (Color)GetValue(RippleColorProperty);
            set => SetValue(RippleColorProperty, value);
        }

        public Color PressedColor
        {
            get => (Color)GetValue(PressedColorProperty);
            set => SetValue(PressedColorProperty, value);
        }

        public Color NormalColor
        {
            get => (Color)GetValue(NormalColorProperty);
            set => SetValue(NormalColorProperty, value);
        }

        public Color DisabledColor
        {
            get => (Color)GetValue(DisabledColorProperty);
            set => SetValue(DisabledColorProperty, value);
        }

        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource Source 
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public int ClickedId
        {
            get => (int)GetValue(ClickedIdProperty);
            set => SetValue(ClickedIdProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public string Detail
        {
            get => (string)GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }

        static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is FloatingActionButton fab && (fab.Command?.CanExecute(newValue)??false) == true)
            {
                fab.Command?.Execute(newValue);
            }
        }

    }
}
