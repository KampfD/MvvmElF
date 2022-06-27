using System;
using System.Text;
using System.Windows;

namespace MvvmElF.Services
{
    /// <summary>
    /// Определяет ответ пользователя на сообщение.
    /// </summary>
    public enum UserResponse
    {
        /// <summary>
        /// Пользователь нажал кнопку "Да".
        /// </summary>
        Yes = MessageBoxResult.Yes,

        /// <summary>
        /// Пользователь нажал кнопку ОК".
        /// </summary>
        OK = MessageBoxResult.OK,

        /// <summary>
        /// Пользователь нажал кнопку "Нет".
        /// </summary>
        No = MessageBoxResult.No,

        /// <summary>
        /// Пользователь нажал кнопку "Отмена".
        /// </summary>
        Cancel = MessageBoxResult.Cancel,

        /// <summary>
        /// Пользователь нечего не нажал.
        /// </summary>
        None = MessageBoxResult.None
    }
}
