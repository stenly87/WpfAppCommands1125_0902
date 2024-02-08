using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppCommands
{
    // простой вариант кастомной команды
    // выполняется всегда
    // параметр не используется
    // исполняемый код получаем через конструктор
    public class CustomCommand : ICommand
    {
        Action action;

        public CustomCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true; // самый простой вариант
        }

        public void Execute(object? parameter)
        {
            action(); // самый простой вариант
        }
    }

    // команда посложнее
    // используется параметр
    // используется проверка на запуск команды
    public class CustomCommand<T> : ICommand
    {
        Action<T> action;
        Func<T, bool> canExecute;

        public CustomCommand(Action<T> action, Func<T, bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        { 
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute((T)parameter); // самый простой вариант
        }

        public void Execute(object? parameter)
        {
            action((T)parameter); // самый простой вариант
        }
    }
}
