using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MyInventoryApp.Controls
{
    public class FloatingActionMenu : View
    {
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(
            nameof(Children),
            typeof(ObservableCollection<FloatingActionButton>),
            typeof(FloatingActionMenu)
        );

        public static readonly BindableProperty DetailProperty = BindableProperty.Create(
            nameof(Detail),
            typeof(string),
            typeof(FloatingActionMenu),
            string.Empty
        );

        public delegate void ShowHideDelegate(bool animate = true);

        public delegate bool GetOpen();

        public event EventHandler<FloatingActionEventArgs> SelectedIndexChanged;

        bool _isopen;

        public FloatingActionMenu()
        {
            _isopen = false;
            Children = new ObservableCollection<FloatingActionButton>();
        }

        public ObservableCollection<FloatingActionButton> Children
        {
            get => (ObservableCollection<FloatingActionButton>)GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        public string Detail
        {
            get => (string)GetValue(DetailProperty);
            set => SetValue(DetailProperty, value);
        }

        public bool IsOpen
        {
            get => _isopen;
            set 
            {
                _isopen = value;
                OnPropertyChanged();
            }                
        }

        public ShowHideDelegate Show { get; set; }

        public ShowHideDelegate Hide { get; set; }

        public GetOpen GetFabIsOpen { get; set; }

        public void RaiseSelectIndexChanged(int index)
        {
            SelectedIndexChanged?.Invoke(this, new FloatingActionEventArgs("RaiseSelectIndexChanged", index));
        }

    }
}
