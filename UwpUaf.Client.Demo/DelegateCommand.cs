using System;
using System.Windows.Input;

namespace UwpUaf.Client.Demo
{
    public class DelegateCommand<T> : ICommand
    {
        readonly Action<T> executeAction;
        Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke((T)parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            executeAction((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}