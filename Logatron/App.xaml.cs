using Logatron.ViewModels;
using System;
using System.Windows;

namespace Logatron
{
    public partial class App : Application
    {
        private void OnStartUp(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            if (mainWindow.DataContext is MainViewModel viewModel)
            {
                viewModel.LoadState();
            }

            mainWindow.Show();
        }
    }
}
