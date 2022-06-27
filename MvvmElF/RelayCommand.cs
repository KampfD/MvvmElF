using System;
using System.Windows.Input;

namespace MvvmElF
{
    /// <summary>
    /// Реализует команду.
    /// </summary>
    public class RelayCommand : ICommand
    {
        // Хранит метод, выполняемый командой.
        private readonly Action<object?>? execute;

        // Хранит метод, проверяющий возможность выполения команды.
        private readonly Predicate<object?>? canExecute;

        /// <summary>
        /// Происходит при изменении состояния возможности выполнения команды.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса RelayCommand.
        /// </summary>
        /// <param name="execute">Метод, выполняемый командой.</param>
        /// <param name="canExecute">Метод, проверяющий возможность выполения команды.</param>
        public RelayCommand(Action<object?>? execute, Predicate<object?>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Определяет может ли в данный момент выполнится команда.
        /// </summary>
        /// <param name="parameter">Параметр команды (может быть null).</param>
        /// <returns>true - команда может быть выполнена, false - команда не может быть выполнена.</returns>
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter); 
        }

        /// <summary>
        /// Метод, выполняемый командой.
        /// </summary>
        /// <param name="parameter">Параметр команды (может быть null).</param>
        public void Execute(object? parameter)
        {
            execute?.Invoke(parameter);
        }
    }
}
