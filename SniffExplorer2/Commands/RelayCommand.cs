using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SniffExplorer.UI.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;

        public RelayCommand(Func<bool> canExecute, Action execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public RelayCommand(Action execute)
            : this(() => true, execute)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public RelayCommand(Action<T> execute)
            : this(_ => true, execute)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute((T) parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T) parameter);
        }
    }
}
