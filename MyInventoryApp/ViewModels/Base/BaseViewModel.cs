using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MyInventoryApp.ViewModels.Base
{
    public abstract class BaseViewModel : ExtendedBindableObject//: INotifyPropertyChanged
    {
        bool _isBusy;

        //public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get => _isBusy;
            set => UpdateAndNotifyOnChange(ref _isBusy, value);
        }

        public virtual void OnAppearing(object navigationContext)
        {

        }

        public virtual void OnDisappearing()
        {

        }

        public virtual Task Initalize(object navigationContext)
        {
            return Task.FromResult(true);
        }
    }
}
