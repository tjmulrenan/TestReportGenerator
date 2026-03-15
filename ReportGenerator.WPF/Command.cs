using System;
using System.Windows.Input;

namespace ReportGeneratorWPF
{
    public class Command : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if(_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
