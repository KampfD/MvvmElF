using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace MvvmElF.Services
{
    /// <summary>
    /// Реализует диалоговый сревис по умолчанию м WPF и WindowsForms диалогами.
    /// </summary>
    public class DefaultDialogService : IDialogService
    {
        /// <summary>
        /// Полный путь к выбранному файлу.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Массив полных путей к выбраным файлам.
        /// </summary>
        public string? FolderPath { get; set; }

        /// <summary>
        /// Полный путь к выбранной папке.
        /// </summary>
        public string[]? FileNames { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса DefaultDialogService.
        /// </summary>
        public DefaultDialogService() { }

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю выбрать файл для открытия.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="filter">Строка фильрации файлов.</param>
        /// <returns>Результат выбора файла. True - файл был выбран, false - файл не был выбран.</returns>
        public virtual bool OpenFileDialog(string title = "Открыть файл", string filter = "Все файлы (*.*)|*.*" )
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = title,
                InitialDirectory = FileName,
                Filter = filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю выбрать файлы для открытия.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="filter">Строка фильрации файлов.</param>
        /// <returns>Результат выбора файлов. True - файлы были выбраны, false - файлы не были выбраны.</returns>
        public virtual bool OpenFilesDialog(string title = "Открыть файл", string filter = "Все файлы (*.*)|*.*")
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = title,
                Multiselect = true,
                InitialDirectory = FileName,
                Filter = filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FileNames = openFileDialog.FileNames;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю сохранить файл.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="filter">Строка фильрации файлов.</param>
        /// <returns>Результат выбора. True - был выбран путь для схранения файла, false - не был выбран путь для схранения файла.</returns>
        public virtual bool SaveFileDialog(string title = "Сохранить файл как", string filter = "Все файлы (*.*)|*.*")
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Title = title,
                InitialDirectory = FileName,
                Filter = filter
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                FileName = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю выбрать папку.
        /// </summary>
        /// <param name="description">Текст описания в окне выбора папки.</param>
        /// <returns>Результат выбора папки. True - папка была выбрана, false - папка не была выбрана.</returns>
        public virtual bool FolderSelectionDialog(string description = "Выбор директории.")
        {
            var fbd = new System.Windows.Forms.FolderBrowserDialog()
            {
                Description = description,
                SelectedPath = FolderPath
            };
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPath = fbd.SelectedPath;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Вызывает окно сообщения.
        /// </summary>
        /// <param name="type">Тип окна.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="caption">Заголовок окна.</param>
        /// <returns>Ответ пользователя на сообщение.</returns>
        public virtual UserResponse ShowMessage(MessageType type, string message, string caption)
        {
            MessageBoxResult result;
            switch (type)
            {
                case MessageType.Error:
                    result = MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case MessageType.Information:
                    result = MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case MessageType.Question:
                    result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    break;
                case MessageType.Warning:
                    result = MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case MessageType.WarningQuestion:
                    result = MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    break;
                default:
                    throw new ArgumentException("Такого типа сообщения не существует");
            }
            return (UserResponse)result;
        }
    }
}