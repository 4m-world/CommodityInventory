using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyInventoryApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyInventoryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : BaseContentPage<ItemsViewModel>
    {
        public ObservableCollection<string> Items { get; set; }

        public ItemsPage()
        {
            InitializeComponent();
        }
    }
}
