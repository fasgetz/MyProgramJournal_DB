using System;
using System.Windows.Input;

namespace MyProgramJournal_DB.MVVM
{
    public class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // CanExecute: определяет, может ли команда выполняться
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        // Execute: собственно выполняет логику команды
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

    }
}


/*
 * 
 * 
 * 
 * 
 * using System;
using System.Windows.Input;

namespace MyProgramJournal_DB.MVVM
{

    // Неполная версия команд
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;
        private Func<object, bool> canExecute;

        public DelegateCommand(Action action)
        {
            _action = action;
        }


        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public event EventHandler  CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}


    */