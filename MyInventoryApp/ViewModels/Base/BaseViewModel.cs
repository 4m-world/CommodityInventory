using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MyInventoryApp.ViewModels.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

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

        protected void UpdateAndNotifyOnChange<T>(ref T store, T newValue, [CallerMemberName]string propertyName = "")
        {
            if(!EqualityComparer<T>.Default.Equals(store, newValue))
            {
                store = newValue;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
