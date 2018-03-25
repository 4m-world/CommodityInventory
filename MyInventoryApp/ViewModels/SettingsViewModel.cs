using System;
using System.Threading.Tasks;
using MyInventoryApp.Models.Navigation;
using MyInventoryApp.ViewModels.Base;
using Xamarin.Forms;

namespace MyInventoryApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public override Task Initalize(object navigationContext)
        {
            IsBusy = true;
            if (navigationContext is TabParameter tab)
            {
                MessagingCenter.Send(this, Messages.ChangeTab, tab.TabIndex);
            }

            return base.Initalize(navigationContext);
        }
    }
}
