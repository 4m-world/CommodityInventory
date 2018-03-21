using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyInventoryApp.ViewModels.Base
{
    public class DelegateCommandAsync : ICommand
    {
        readonly Func<Task> _execute;
        readonly Func<Task<bool>> _canExecute;

        public DelegateCommandAsync(Func<Task> execute)
            : this(execute, null)
        {
        }

        public DelegateCommandAsync(Func<Task> execute, Func<Task<bool>> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke().Result ?? true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DelegateCommandAsync<T> : ICommand
    {
        readonly Func<T, Task> _execute;
        readonly Func<T, Task<bool>> _canExecute;

        public DelegateCommandAsync(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        public DelegateCommandAsync(Func<T, Task> execute, Func<T, Task<bool>> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter).Result ?? true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
