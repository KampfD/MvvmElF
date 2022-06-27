using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmElF.Services
{
    /// <summary>
    /// Предоставляет сервис диалоговых окон.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Полный путь к выбранному файлу.
        /// </summary>
        string? FileName { get; set; }

        /// <summary>
        /// Массив полных путей к выбраным файлам.
        /// </summary>
        string[]? FileNames { get; set; }

        /// <summary>
        /// Полный путь к выбранной папке.
        /// </summary>
        string? FolderPath { get; set; }

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю выбрать папку.
        /// </summary>
        /// <param name="description">Текст описания в окне выбора папки.</param>
        /// <returns>Результат выбора папки. True - папка была выбрана, false - папка не была выбрана.</returns>
        bool FolderSelectionDialog(string description);

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю выбрать файл для открытия.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="filter">Строка фильрации файлов.</param>
        /// <returns>Результат выбора файла. True - файл был выбран, false - файл не был выбран.</returns>
        bool OpenFileDialog(string title = "Открыть файл", string filter = "Все файлы (*.*)|*.*");

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю выбрать файлы для открытия.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="filter">Строка фильрации файлов.</param>
        /// <returns>Результат выбора файлов. True - файлы были выбраны, false - файлы не были выбраны.</returns>
        bool OpenFilesDialog(string title = "Открыть файл", string filter = "Все файлы (*.*)|*.*");

        /// <summary>
        /// Вызывает диалоговое окно, позволяющее пользователю сохранить файл.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="filter">Строка фильрации файлов.</param>
        /// <returns>Результат выбора. True - был выбран путь для схранения файла, false - не был выбран путь для схранения файла.</returns>
        bool SaveFileDialog(string title = "Сохранить файл как", string filter = "Все файлы (*.*)|*.*");

        /// <summary>
        /// Вызывает окно сообщения.
        /// </summary>
        /// <param name="type">Тип окна.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="caption">Заголовок окна.</param>
        /// <returns>Ответ пользователя на сообщение.</returns>
        UserResponse ShowMessage(MessageType type, string message, string caption);
    }
}
