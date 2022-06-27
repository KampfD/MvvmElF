using System;

namespace MvvmElF.Messaging
{
    /// <summary>
    /// Предоставляет сервис обмена сообщениями между объектами.
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// Регистрирует получателя и токен для сообщения типа TMessage, 
        /// а также делегат получателя, который будет вызван при успешном приёме сообщения из шины.
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения.</typeparam>
        /// <param name="recipient">Объект-получатель сообщения.</param>
        /// <param name="token">Токен сообщения.</param>
        /// <param name="action">Делегат, который будет вызван в объекте-получателе при получении сообщения.</param>
        /// <returns>true - если сообщение успешно зарегистрировано, false - если нет.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается если один из аргументов равен null.</exception>
        bool Register<TMessage>(object recipient, object token, Action<TMessage> action);

        /// <summary>
        /// Отправляет сообщение всем зарагистрированным получателям, 
        /// при условии совпадения типа сообщения и токена.
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="token">Токен сообщения.</param>
        /// <returns>true - если хотябы одно сообщение было отправлено, false - если нет.</returns>
        bool Send<TMessage>(TMessage message, object token);

        /// <summary>
        /// Выполняет отправку сообщения всем зарагистрированным получателям, 
        /// при условии совпадения типа сообщения и токена, в отдельном потоке.
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="token">Токен сообщения.</param>
        void BeginSend<TMessage>(TMessage message, object token);

        /// <summary>
        /// Отменяет регистрацию сообщения в шине.
        /// </summary>
        /// <param name="recipient">Объект-получатель сообщения.</param>
        /// <param name="token">Токен сообщения.</param>
        /// <returns>true - если регистрация сообщения успешно отменена, false - если нет.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается если один из аргументов равен null.</exception>
        bool Unregister(object recipient, object token);

        /// <summary>
        /// Отменяет регистрацию всех сообщений объекта в шине.
        /// </summary>
        /// <param name="recipient">Объект-получатель сообщения.</param>
        /// <returns>true - если регистрация сообщений объекта успешно отменена, false - если нет.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается если один из аргументов равен null.</exception>
        bool Unregister(object recipient);
    }
}
