using System.Windows.Input;

namespace MvvmElF.TestApp.Mvvm.ViewModels
{
    class SecondWindowViewModel : ViewModelBase
    {
        private ICommand? _windowLoaded;
        private ICommand? _windowClosed;
        private string _text = "Текст по умолчанию";

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(() => Text); }
        }

        public ICommand? WindowLoaded => _windowLoaded ?? (_windowLoaded = new RelayCommand(obj =>
        {
            services.Messenger.Register<string>(this, "token1", m => Text = m);
            services.Messenger.Register<string>(this, "token2", m => Text = m);
        }));

        public ICommand? WindowClosed => _windowClosed ?? (_windowClosed = new RelayCommand(obj =>
        {
            Cleanup();
        }));

        public SecondWindowViewModel()
        {

        }
    }
}
