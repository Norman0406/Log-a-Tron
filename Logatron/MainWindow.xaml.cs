using AvalonDock.Layout.Serialization;
using System;
using System.IO;
using System.Windows;

namespace Logatron
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Left = Properties.Settings.Default.WindowPositionLeft;
            Top = Properties.Settings.Default.WindowPositionTop;
            Width = Properties.Settings.Default.WindowWidth;
            Height = Properties.Settings.Default.WindowHeight;
            try
            {
                StringReader stringReader = new(Properties.Settings.Default.Layout);
                XmlLayoutSerializer layoutSerializer = new(dockManager);
                layoutSerializer.Deserialize(stringReader);
            }
            catch (Exception)
            {
                // NOP
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WindowPositionLeft = Left;
            Properties.Settings.Default.WindowPositionTop = Top;
            Properties.Settings.Default.WindowWidth = Width;
            Properties.Settings.Default.WindowHeight = Height;

            XmlLayoutSerializer layoutSerializer = new(dockManager);
            StringWriter stringWriter = new();
            layoutSerializer.Serialize(stringWriter);
            Properties.Settings.Default.Layout = stringWriter.ToString();

            Properties.Settings.Default.Save();
        }
    }
}
