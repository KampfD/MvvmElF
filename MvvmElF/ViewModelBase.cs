using MvvmElF.Services;

namespace MvvmElF
{
    /// <summary>
    /// Реализует базовый функционал модели представления.
    /// </summary>
    public class ViewModelBase : ObservableObject, ICleanupable
    {
        /// <summary>
        /// Сервисы, поддреживающие реализацию MVVM.
        /// </summary>
        protected readonly ServiceLocator services = ServiceLocator.Instance;

        /// <summary>
        /// Проводит очистку шины сообщений от сообщений этого объекта.
        /// </summary>
        public virtual void Cleanup()
        {
            services.Messenger.Unregister(this);
        }
    }
}
