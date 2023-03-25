using Logatron.ViewModels;
using System;
using System.IO;
using System.Windows;

namespace Logatron
{
    public partial class App : Application
    {
        public static string AppName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name!;
        public static string ConfigDir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);

        private void OnStartUp(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(ConfigDir))
            {
                Directory.CreateDirectory(ConfigDir);
            }

            var mainWindow = new MainWindow();
            if (mainWindow.DataContext is MainViewModel viewModel)
            {
                viewModel.LoadState();
            }

            mainWindow.Show();
        }
    }
}
