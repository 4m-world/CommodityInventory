using System;
using MyInventoryApp.ViewModels;
using ReactiveUI.XamForms;

namespace MyInventoryApp.Views
{
    public class BaseContentPage<TViewModel>
        : ReactiveContentPage<TViewModel> where TViewModel : BaseViewModel
    {
        public BaseContentPage()
        {
        }
    }
}
