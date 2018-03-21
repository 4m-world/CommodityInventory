using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace MyInventoryApp.Controls.Fab
{
    public class FloatingActionButtonView : View
    {
        public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(
            nameof(ImageName),
            typeof(string),
            typeof(FloatingActionButtonView),
            string.Empty
        );

        public static readonly BindableProperty ColorNormalProperty = BindableProperty.Create(
            nameof(ColorNormal),
            typeof(Color),
            typeof(FloatingActionButtonView),
            Color.White
        );

        public static readonly BindableProperty ColorPressedProperty = BindableProperty.Create(
            nameof(ColorPressed),
            typeof(Color),
            typeof(FloatingActionButtonView),
            Color.White
        );

        public static readonly BindableProperty ColorRippleProperty = BindableProperty.Create(
            nameof(ColorRipple),
            typeof(Color),
            typeof(FloatingActionButtonView),
            Color.White
        );

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size),
            typeof(FloatingActionButtonSize),
            typeof(FloatingActionButtonView),
            FloatingActionButtonSize.Normal
        );

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(
            nameof(HasShadow),
            typeof(bool),
            typeof(FloatingActionButtonView),
            true
        );

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(FloatingActionButtonView),
            null,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((FloatingActionButtonView)bindable).OnCommandChanged()
        );

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(FloatingActionButtonView),
            null,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((FloatingActionButtonView)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty)
        );

        public string ImageName
        {
            get => (string)GetValue(ImageNameProperty);
            set => SetValue(ImageNameProperty, value);
        }

        public Color ColorNormal
        {
            get => (Color)GetValue(ColorNormalProperty);
            set => SetValue(ColorNormalProperty, value);
        }

        public Color ColorPressed
        {
            get => (Color)GetValue(ColorPressedProperty);
            set => SetValue(ColorPressedProperty, value);
        }

        public Color ColorRipple
        {
            get => (Color)GetValue(ColorPressedProperty);
            set => SetValue(ColorPressedProperty, value);
        }

        public FloatingActionButtonSize Size
        {
            get => (FloatingActionButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
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

        void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            var cmd = Command;
            if (cmd != null)
                IsEnabled = cmd.CanExecute(CommandParameter);
        }

        void OnCommandChanged()
        {
            if (Command != null)
            {
                Command.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
            {
                IsEnabled = true;
            }
        }

        public delegate void ShowHideDelegate(bool animate = true);

        public delegate void AttachToListViewDelegate(ListView listView);

        public ShowHideDelegate Show { get; set; }

        public ShowHideDelegate Hide { get; set; }

        public Action<object, EventArgs> Clicked { get; set; }
    }
}
