using System;
using MvvmElF.Messaging;

namespace MvvmElF.Services
{
    /// <summary>
    /// Реализует сервис локатор для сервисов, обеспечивающих поддержку Mvvm.
    /// </summary>
    public class ServiceLocator
    {
        /// <summary>
        /// Хранит экземпляр класса ServiceLocator.
        /// </summary>
        protected static readonly Lazy<ServiceLocator> _instance =
            new Lazy<ServiceLocator>(() => new ServiceLocator());

        private ServiceLocator() 
        {

            WindowService = new WindowService();
            DialogService = new DefaultDialogService();
            Messenger = new Messenger();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса ServiceLocator.
        /// </summary>
        protected ServiceLocator(IWindowService windowService, IDialogService dialogService, IMessenger messenger)
        {
            WindowService = windowService;
            DialogService = dialogService;
            Messenger = messenger;
        }

        /// <summary>
        /// Получает единственный экземпляр класса ServiceLocator.
        /// </summary>
        public static ServiceLocator Instance
        {
            get => _instance.Value;
        }

        /// <summary>
        /// Сервис управления окнами в приложении.
        /// </summary>
        public IWindowService WindowService { get; set; }

        /// <summary>
        /// Сервис, обеспечивающий диалог с пользователем через диалоговые окна.
        /// </summary>
        public IDialogService DialogService { get; set; }

        /// <summary>
        /// Сервис обмена сообщениями между объектами по шине.
        /// </summary>
        public IMessenger Messenger { get; set; }
    }
}
