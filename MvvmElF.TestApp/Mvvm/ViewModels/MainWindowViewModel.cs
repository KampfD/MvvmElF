using System.Linq;
using System.Windows.Input;
using MvvmElF.Services;
using MvvmElF.TestApp.Mvvm.Services;

namespace MvvmElF.TestApp.Mvvm.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ServiceLocator csl = CustomServiceLocator.Instance; // Для теста CustomServiceLocator

        private int counter = 0;

        private ICommand? _sendMessageToSecondWindow;
        private ICommand? _openSecondWindow;
        private ICommand? _openSecondWindowModal;
        private ICommand? _openSecondWindowWithActiveOwner;
        private ICommand? _changeProperty;
        private ICommand? _dialogTestApp;
        private ICommand? _openFile;
        private ICommand? _openFiles;
        private ICommand? _saveFile;
        private ICommand? _openFolder;
        private string _buttonText = "Кнопка со счётчиком";

        public ICommand? SendMessageToSecondWindow => _sendMessageToSecondWindow ??= new RelayCommand(obj =>
        {
            if (!services.Messenger.Send("Сообщение доставлено", "token1"))
            {
                services.DialogService.ShowMessage(
                    MessageType.Warning,
                    "Указанное сообщение не зарегистрировано в шине.",
                    "Сообщение не доставлено");
            }
        });


        public ICommand? OpenSecondWindow => _openSecondWindow ??= new RelayCommand(obj =>
        {
            if (!services.WindowService.CheckWindowExistence("SecondWindow"))
            {
                services.WindowService.ShowWindow(
                    Modality.Parallel,
                    "SecondWindow",
                    new SecondWindowViewModel());
            }
            else
            {
                services.DialogService.ShowMessage(
                    MessageType.Warning,
                    "Это окно уже открыто и будет закрыто.",
                    "Предупреждение");
                services.WindowService.CloseWindow("SecondWindow");
            }
        });


        public ICommand? OpenSecondWindowModal => _openSecondWindowModal ?? (_openSecondWindowModal = new RelayCommand(obj =>
        {
            services.WindowService.ShowWindow(
                Modality.Modal,
                "SecondWindow",
                "MainWindow",
                new SecondWindowViewModel());
        }));

        public ICommand? OpenSecondWindowWithActiveOwner => _openSecondWindowWithActiveOwner ?? (_openSecondWindowWithActiveOwner = new RelayCommand(obj =>
        {
            services.WindowService.ShowWindowWithActiveOwner(
                Modality.Parallel,
                "SecondWindow",
                new SecondWindowViewModel());
        }));


        public ICommand? ChangeProperty =>  _changeProperty ?? (_changeProperty = new RelayCommand(obj =>
        {
            ButtonText = $"Кнопка нажата {++counter} раз(а)";
        }));

        public ICommand? DialogTestApp => _dialogTestApp ?? (_dialogTestApp = new RelayCommand(obj =>
        {
            var result = services.DialogService.ShowMessage(
                MessageType.Question,
                $"Конткнт в кнопке - {obj}?",
                "Тест диалогового окна");
            if (result == UserResponse.Yes)
            {
                services.DialogService.ShowMessage(
                    MessageType.Information,
                    @"Пользователь нажал кнопку ""Да""",
                    "Результат вопроса пользователю");
            }
            else
            {
                services.DialogService.ShowMessage(
                    MessageType.Information,
                    @"Пользователь нажал кнопку ""Нет""",
                    "Результат вопроса пользователю");
            }
        }));

        public ICommand? OpenFile => _openFile ?? (_openFile = new RelayCommand(obj =>
        {
            bool result = services.DialogService.OpenFileDialog();
            if (result)
            {
                services.DialogService.ShowMessage(
                    MessageType.Information,
                    $"Был выбран файл {services.DialogService.FileName}",
                    "Результат выбора файла");
            }
        }));

        public ICommand? OpenFiles => _openFiles ?? (_openFiles = new RelayCommand(obj =>
        {
            bool result = services.DialogService.OpenFilesDialog();
            if (result)
            {
                string outStr = services.DialogService.FileNames!.Aggregate((current, next) => current + "\n" + next);
                services.DialogService.ShowMessage(
                    MessageType.Information,
                    $"Были выбраны файлы\n{outStr}",
                    "Результат выбора файла");
            }
        }));

        public ICommand? OpenFolder => _openFolder ?? (_openFolder = new RelayCommand(obj =>
        {
            bool result = services.DialogService.FolderSelectionDialog("Выберите паку для теста.");
            if (result)
            {
                services.DialogService.ShowMessage(
                    MessageType.Information,
                    $"Была выбрана папка {services.DialogService.FolderPath}",
                    "Результат выбора файла");
            }
        }));

        public ICommand? SaveFile => _saveFile ?? (_saveFile = new RelayCommand(obj =>
        {
            bool result = services.DialogService.SaveFileDialog();
            if (result)
            {
                services.DialogService.ShowMessage(
                    MessageType.Information,
                    $"Был выбран файл {services.DialogService.FileName}",
                    "Результат выбора файла");
            }
        }));


        public string ButtonText
        {
            get { return _buttonText; }
            set { _buttonText = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        { 
            
        }

    }
}
