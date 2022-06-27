using System;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmElF
{
    /// <summary>
    /// Реализует сервис уведомлений объектов-подписчиков об изменении свойства.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Происходит при изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает уведомление подписанных объектов об изменении свойства.
        /// </summary>
        /// <typeparam name="T">Тип изменённого свойства.</typeparam>
        /// <param name="changedProperty">Делегат, в который передаётся изменённое свойство.</param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            if (PropertyChanged != null)
            {
                string name = ((MemberExpression)changedProperty.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Вызывает уведомление подписанных объектов об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя изменённого свойства.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        /// <summary>
        /// Изменяет значение инкапсулированного свойством поля и вызывает
        /// уведомление подписанных объектов об изменении свойства.
        /// </summary>
        /// <typeparam name="T">Тип изменённого свойства (поля).</typeparam>
        /// <param name="field">Ссылка на инкапсулированное свойством поле.</param>
        /// <param name="value">Новое значение свойства.</param>
        /// <param name="changedProperty">Делегат, в который передаётся изменённое свойство.</param>
        /// <returns>true, если значение было изменено, false если нет.</returns>
        protected virtual bool SetAndNotify<T>(ref T field, T value, Expression<Func<T>> changedProperty)
        {
            if (!object.ReferenceEquals(field, value))
            {
                field = value;
                OnPropertyChanged(changedProperty);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Изменяет значение инкапсулированного свойством поля и вызывает
        /// уведомление подписанных объектов об изменении свойства.
        /// </summary>
        /// <typeparam name="T">Тип изменённого свойства (поля).</typeparam>
        /// <param name="field">Ссылка на инкапсулированное свойством поле.</param>
        /// <param name="value">Новое значение свойства.</param>
        /// <param name="propertyName">Имя изменённого свойства.</param>
        /// <returns>true, если значение было изменено, false если нет.</returns>
        protected virtual bool SetAndNotify<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!object.ReferenceEquals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
