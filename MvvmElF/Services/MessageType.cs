using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmElF.Services
{
    /// <summary>
    /// Определяет тип сообщения в варианты ответа пользователя.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Информационное сообщение.
        /// </summary>
        Information,

        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        Error,

        /// <summary>
        /// Предупреждение.
        /// </summary>
        Warning,

        /// <summary>
        /// Вопрос пользователю.
        /// </summary>
        Question,

        /// <summary>
        /// Вопрос пользователю с предупреждением.
        /// </summary>
        WarningQuestion
    }
}
