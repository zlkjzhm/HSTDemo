using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FzyMVVM
{
    public class RelayCommand: ICommand
    {
        readonly Func<Boolean> _canExecute;
        readonly Action _execute;

        public RelayCommand(Action execute, Func<Boolean> canExecute)
        {

            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(Object parameter)
        {
            _execute();
        }
    }
}
