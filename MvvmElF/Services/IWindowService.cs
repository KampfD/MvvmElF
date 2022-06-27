using System;
using System.Text;
using System.Windows;

namespace MvvmElF.Services
{
    /// <summary>
    /// Предосталяет сервис управления окнами в приложении.
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Открывает новое окно.
        /// </summary>
        /// <param name="modality">Модальность окна.</param>
        /// <param name="windowName">Имя окна.</param>
        /// <param name="viewModel">Модель представления.</param>
        void ShowWindow(Modality modality, string windowName, object viewModel);        
        
        /// <summary>
        /// Открывает новое окно с указанным окном-владельцем.
        /// </summary>
        /// <param name="modality">Модальность окна.</param>
        /// <param name="windowName">Имя окна.</param>
        /// <param name="windowOwnerName">Имя окна-владельца.</param>
        /// <param name="viewModel">Модель представления.</param>
        void ShowWindow(Modality modality, string windowName, string windowOwnerName, object viewModel);

        /// <summary>
        /// Открывает новое модальное окно, владельцем которого является активное окно в приложении.
        /// </summary>
        /// <param name="modality">Модальность окна.</param>
        /// <param name="windowName">Имя окна.</param>
        /// <param name="viewModel">Модель представления.</param>
        void ShowWindowWithActiveOwner(Modality modality, string windowName, object viewModel);

        /// <summary>
        /// Закрывает окно.
        /// </summary>
        /// <param name="windowName">Имя окна.</param>
        void CloseWindow(string windowName);

        /// <summary>
        /// Проверяет открыто ли указанное окно в данный момент.
        /// </summary>
        /// <param name="windowName">Имя окна.</param>
        /// <returns>Открыто ли окно.</returns>
        bool CheckWindowExistence(string windowName);
    }
}
