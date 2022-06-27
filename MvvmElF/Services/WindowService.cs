using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;

namespace MvvmElF.Services
{
    /// <summary>
    /// Реализует сервис управления окнами в приложении.
    /// </summary>
    public class WindowService : IWindowService
    {        
        /// <summary>
        /// Инициализирует новый экземпляр класса WindowService.
        /// </summary>
        public WindowService() { }

        /// <summary>
        /// Открывает новое окно.
        /// </summary>
        /// <param name="modality">Модальность окна.</param>
        /// <param name="windowName">Имя окна.</param>
        /// <param name="viewModel">Модель представления.</param>
        public virtual void ShowWindow(Modality modality, string windowName, object viewModel)
        {
            string? callingAssemblyName = Assembly.GetCallingAssembly().FullName?.Split(',').First();
            var window = CreateWindow(callingAssemblyName, windowName);
            window.DataContext = viewModel;
            if (modality == Modality.Modal)
            {
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }

        /// <summary>
        /// Открывает новое окно с указанным окном-владельцем.
        /// </summary>
        /// <param name="modality">Модальность окна.</param>
        /// <param name="windowName">Имя окна.</param>
        /// <param name="windowOwnerName">Имя окна-владельца.</param>
        /// <param name="viewModel">Модель представления.</param>
        public virtual void ShowWindow(Modality modality, string windowName, string windowOwnerName, object viewModel)
        {
            string? callingAssemblyName = Assembly.GetCallingAssembly().FullName?.Split(',').First();
            var window = CreateWindow(callingAssemblyName, windowName);
            window.DataContext = viewModel;
            var ownerWindow = GetWindow(windowOwnerName);
            if (ownerWindow != null)
            {
                window.Owner = ownerWindow;
                if (modality == Modality.Modal)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            }
            else
            {
                throw new ArgumentException(
                    "Окна-владельца с таким именем не существует.", 
                    nameof(windowOwnerName));
            }
        }

        /// <summary>
        /// Открывает новое модальное окно, владельцем которого является активное окно в приложении.
        /// </summary>
        /// <param name="modality">Модальность окна.</param>
        /// <param name="windowName">Имя окна.</param>
        /// <param name="viewModel">Модель представления.</param>
        public virtual void ShowWindowWithActiveOwner(Modality modality, string windowName, object viewModel)
        {
            string? callingAssemblyName = Assembly.GetCallingAssembly().FullName?.Split(',').First();
            var window = CreateWindow(callingAssemblyName, windowName);
            var ownerWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.DataContext = viewModel;
            window.Owner = ownerWindow;
            if (modality == Modality.Modal)
            {
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }

        /// <summary>
        /// Закрывает окно.
        /// </summary>
        /// <param name="windowName">Имя окна.</param>
        public virtual void CloseWindow(string windowName)
        {
            var window = GetWindow(windowName);
            if (window != null)
            {
                window.Close();
            }
            else
            {
                throw new ArgumentException(
                    "Окно с таким именем не было открыто или не существует.",
                    nameof(windowName));
            }
        }

        /// <summary>
        /// Проверяет открыто ли указанное окно в данный момент.
        /// </summary>
        /// <param name="windowName">Имя окна.</param>
        /// <returns>Открыто ли окно.</returns>
        public virtual bool CheckWindowExistence(string windowName)
        {
            string? callingAssemblyName = Assembly.GetCallingAssembly().FullName?.Split(',').First();
            string fullName = $"{callingAssemblyName}.Mvvm.Views.{windowName}";
            return Application.Current.Windows.OfType<Window>().Any(w => w.ToString() == fullName);
        }

        /// <summary>
        /// Возвращает открытое в приложении окно по имени типа и если он существует.
        /// </summary>
        /// <param name="windowName">Имя окна</param>
        /// <exception cref="InvalidOperationException">Найдено более одного окна.</exception>
        /// <returns>Найденное окно</returns>
        protected virtual Window? GetWindow(string windowName)
        {
            try
            {
                return Application.Current.Windows.OfType<Window>().SingleOrDefault(w =>
                {
                    string[] fullNameSegments = w.ToString().Split('.');
                    string name = fullNameSegments[fullNameSegments.Length - 1];
                    if (name == windowName)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            // Найдено более одного окна.
            catch (InvalidOperationException) 
            { 
                throw; 
            }
        }

        /// <summary>
        /// Создаёт экзпмпляр окна.
        /// </summary>
        /// <param name="callingAssemblyName">Имя вызывающей сборки.</param>
        /// <param name="windowName">Имя окна.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected virtual Window CreateWindow(string? callingAssemblyName, string windowName)
        {
            if (CheckWindowExistence(windowName))
            {
                throw new ArgumentException("Такое окно уже открыто.", nameof(windowName));
            }
            string strType = $"{callingAssemblyName}.Mvvm.Views.{windowName}, {callingAssemblyName}";
            var type = Type.GetType(strType);
            if (type == null)
            {
                throw new ArgumentException("Указанное имя не является именем представления.", nameof(windowName));
            }
            return (Window)Activator.CreateInstance(type)!;
        }
    }
}
