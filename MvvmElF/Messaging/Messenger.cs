using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace MvvmElF.Messaging
{
    /// <summary>
    /// Реализует шину обмена сообщениями между объектами.
    /// </summary>
    public class Messenger : IMessenger
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса Messenger.
        /// </summary>
        public Messenger() { }

        /// <summary>
        /// Словарь, содержищий зарегистрированные в шине сообщения.
        /// Key - экземпляр структуры RecipientAndToken.
        /// Value - ссылка на делегат, ассоциированный с ключём.
        /// </summary>
        protected readonly ConcurrentDictionary<RecipientAndToken, object> registeredMessages = new();

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
        public virtual bool Register<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            ArgumentNullException.ThrowIfNull(recipient, nameof(recipient));
            ArgumentNullException.ThrowIfNull(token, nameof(token));
            ArgumentNullException.ThrowIfNull(action, nameof(action));
            var key = new RecipientAndToken(recipient, token);
            if (registeredMessages.Any(randt => randt.Key.Equals(key)))
            {
                throw new ArgumentException("Сообщение от этого получателя с таким токеном уже зарегистрировано.");
            }
            return registeredMessages.TryAdd(key, action);
        }

        /// <summary>
        /// Отправляет сообщение всем зарагистрированным получателям, 
        /// при условии совпадения типа сообщения и токена.
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="token">Токен сообщения.</param>
        /// <returns>true - если хотябы одно сообщение было отправлено, false - если нет.</returns>
        public virtual bool Send<TMessage>(TMessage message, object token)
        {
            ArgumentNullException.ThrowIfNull(message, nameof(message));
            ArgumentNullException.ThrowIfNull(token, nameof(token));
            bool wasSended = false;
            var neededMessages = registeredMessages.Where(r => r.Key.Token.Equals(token));
            foreach (var action in neededMessages.Select(x => x.Value).OfType<Action<TMessage>>())
            {
                action(message);
                wasSended = true;
            }
            return wasSended;
        }

        /// <summary>
        /// Выполняет отправку сообщения всем зарагистрированным получателям, 
        /// при условии совпадения типа сообщения и токена, в отдельном потоке.
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения.</typeparam>
        /// <param name="message">Сообщение.</param>
        /// <param name="token">Токен сообщения.</param>
        public virtual void BeginSend<TMessage>(TMessage message, object token)
        {
            Task.Factory.StartNew(() => Send(message, token));
        }

        /// <summary>
        /// Отменяет регистрацию сообщения в шине.
        /// </summary>
        /// <param name="recipient">Объект-получатель сообщения.</param>
        /// <param name="token">Токен сообщения.</param>
        /// <returns>true - если регистрация сообщения успешно отменена, false - если нет.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается если один из аргументов равен null.</exception>
        public virtual bool Unregister(object recipient, object token)
        {
            ArgumentNullException.ThrowIfNull(recipient, nameof(recipient));
            ArgumentNullException.ThrowIfNull(token, nameof(token));
            var key = new RecipientAndToken(recipient, token);
            return registeredMessages.TryRemove(key, out _);
        }

        /// <summary>
        /// Отменяет регистрацию всех сообщений объекта в шине.
        /// </summary>
        /// <param name="recipient">Объект-получатель сообщения.</param>
        /// <returns>true - если регистрация сообщений объекта успешно отменена, false - если нет.</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается если один из аргументов равен null.</exception>
        public virtual bool Unregister(object recipient)
        {
            ArgumentNullException.ThrowIfNull(recipient, nameof(recipient));
            bool result = false;
            var removedObj = registeredMessages.Keys.Where(randt => randt.Recipient.Equals(recipient));
            foreach (var obj in removedObj)
            {
                result = registeredMessages.TryRemove(obj, out _);
            }
            return result;
        }
    }
}