using System;
using System.Collections.Generic;
using MyInventoryApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MyInventoryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BaseContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
