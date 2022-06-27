using System;

namespace MvvmElF.Messaging
{
    /// <summary>
    /// Представляет пару получатель-токен, харатеризующую сообщение в шине.
    /// </summary>
    public sealed class RecipientAndToken
    {
        /// <summary>
        /// Инициализирует новый экземпляр структуры RecipientAndToken.
        /// </summary>
        /// <param name="recipient">Объект-получатель сообщения.</param>
        /// <param name="token">Токен (идентификатор) сообщения.</param>
        public RecipientAndToken(object recipient, object token)
        {
            Recipient = recipient;
            Token = token;
        }

        /// <summary>
        /// Объект-получатель сообщения.
        /// </summary>
        public object Recipient;

        /// <summary>
        /// Токен (идентификатор) сообщения.
        /// </summary>
        public object Token;

        /// <summary>
        /// Определяет, равен ли указанный объект текущкму объекту.
        /// </summary>
        /// <param name="obj">Объект, который требуется сравнить с текущим объектом.</param>
        /// <returns>Значение true, если указанный объект равен текущему объекту; в противном случае — значение false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is RecipientAndToken recipientAndToken)
            {
                return Recipient.Equals(recipientAndToken.Recipient) && Token.Equals(recipientAndToken.Token);
            }
            return false;
        }

        /// <summary>
        /// Служит хэш-функцией по умолчанию.
        /// </summary>
        /// <returns>Хэш-код для текущего объекта.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Recipient.GetHashCode(), Token.GetHashCode());
        }

        /// <summary>
        /// Возвращает строку, представляющую текущий объект.
        /// </summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return $"{Recipient} - {Token}";
        }
    }
}
