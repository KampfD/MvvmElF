using System;
using MvvmElF.Services;

namespace MvvmElF.TestApp.Mvvm.Services
{
    // Пример наследования ServiceLocator для кастомных сервисов
    public class CustomServiceLocator : ServiceLocator
    {
        protected static readonly new Lazy<ServiceLocator> _instance =
            new Lazy<ServiceLocator>(() => new CustomServiceLocator());

        protected CustomServiceLocator()
            : base(new WindowService(), new DefaultDialogService(), new CustomMessanger())
        { }

        /// <summary>
        /// Получает единственный экземпляр класса ServiceLocator с камтомными сервисами.
        /// </summary>
        public static new ServiceLocator Instance
        {
            get => _instance.Value;
        }
    }
}
