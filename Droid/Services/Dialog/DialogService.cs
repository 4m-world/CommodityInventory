using System;
using System.Threading.Tasks;
using MyInventoryApp.Droid.Services.Dialog;
using MyInventoryApp.Services.Dialog;
using Xamarin.Forms;

[assembly: Dependency(typeof(DialogService))]
namespace MyInventoryApp.Droid.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public Task Show(string message)
        {
            return Task.FromResult(false);
        }
    }
}

