using System;
using System.Collections.Generic;
using MyInventoryApp.ViewModels;
using MyInventoryApp.ViewModels.Base;
using Xamarin.Forms;

namespace MyInventoryApp.Pages
{
    public partial class SettingsView : TabbedPage
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<SettingsViewModel, int>(this, Messages.ChangeTab, (sender, arg) =>
            {
                switch (arg)
                {
                    case 0:
                        CurrentPage = UnitsView;
                        break;
                    case 1:
                        CurrentPage = SyncView;
                        break;
                }
            });

            try
            {
                await ((UnitsViewModel)UnitsView.BindingContext)?.Initalize(null);
                await ((SyncViewModel)SyncView.BindingContext)?.Initalize(null);
            }catch{}
        }

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            try
            {
                if (CurrentPage is UnitsView)
                {
                    await ((UnitsViewModel)UnitsView.BindingContext).Initalize(null);
                }
                else if (CurrentPage is SyncView)
                {
                    //await ((SyncViewModel)SyncView.BindingContext).Initalize(null);
                }
            }catch{}
        }
    }
}
