using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MvvmElF.TestApp.Mvvm.ViewModels;
using MvvmElF.Services;

namespace MvvmElF.TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = ServiceLocator.Instance;

            services.WindowService.ShowWindow(
                Modality.Parallel,
                "MainWindow",
                new MainWindowViewModel());
        }
    }
}
