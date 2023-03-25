using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logatron.Database;
using Logatron.Models;
using Logatron.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Logatron.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private double _left;

        [ObservableProperty]
        private double _top;

        [ObservableProperty]
        private double _width;

        [ObservableProperty]
        private double _height;

        [ObservableProperty]
        private WindowState _windowState;

        [ObservableProperty]
        public AvalonDock.DockingManager _dockingManager;

        [ObservableProperty]
        private LogbookViewModel _logbookViewModel;

        [ObservableProperty]
        private RadioViewModel _radioViewModel;

        [ObservableProperty]
        private MapViewModel _mapViewModel;

        public ICommand SaveStateCommand { get; }

        public MainViewModel()
        {
            var logbook = CreateLogbook();

            _logbookViewModel = new LogbookViewModel(logbook);
            _logbookViewModel.Init();

            SaveStateCommand = new RelayCommand(SaveState);

            _radioViewModel = new RadioViewModels.OmniRig1ViewModel();

            _mapViewModel = new MapViewModel();

            // initialize lotw client
            //var credential = Util.CredentialManager.LoadCredential("LotW");
            //if (credential != null)
            //{
            //    Logatron.LotW.Client lotwClient = new(credential.UserName, credential.Password);
            //    var adifFile = lotwClient.Query().Result;
            //}

            //// initialize qrz.com client
            //var qrzCredential = Util.CredentialManager.LoadCredential("QRZ.com");
            //if (qrzCredential != null)
            //{
            //    var qrzClient = new Logatron.QrzDotCom.Client(qrzCredential.UserName, qrzCredential.Password);
            //    var callsignResult = qrzClient.QueryCallsign("HB9HTX").Result;
            //    var dxccResult = qrzClient.QueryDxcc(291).Result;
            //}

            //// initialize jt client
            //var _jtClient = new Logatron.Jt.Client("239.255.255.0", 2237);
            //_jtClient.Alerts.Subscribe((alert) =>
            //{
            //});
        }

        private static Logbook CreateLogbook()
        {
            LogbookContextFactory databaseFactory = new("logbook.db");

            ILogbookProvider logbookProvider = new DatabaseLogbookProvider(databaseFactory);
            ILogbookEntryCreator entryCreator = new DatabaseLogbookEntryCreator(databaseFactory);
            ILogbookEntryUpdater entryUpdater = new DatabaseLogbookEntryUpdater(databaseFactory);
            ILogbookEntryDeleter entryDeleter = new DatabaseLogbookEntryDeleter(databaseFactory);
            return new Logbook(logbookProvider, entryCreator, entryUpdater, entryDeleter);
        }

        public void LoadState()
        {
            // TODO: move settings file to %AppData%/Logatron

            Left = Properties.Settings.Default.WindowPositionLeft;
            Top = Properties.Settings.Default.WindowPositionTop;
            Width = Properties.Settings.Default.WindowWidth;
            Height = Properties.Settings.Default.WindowHeight;
            WindowState = Properties.Settings.Default.Maximized ? WindowState.Maximized : WindowState.Normal;

            try
            {
                StringReader stringReader = new(Properties.Settings.Default.Layout);
                XmlLayoutSerializer layoutSerializer = new(DockingManager);
                layoutSerializer.Deserialize(stringReader);
            }
            catch (Exception)
            {
                // NOP
            }
        }

        public void SaveState()
        {
            Properties.Settings.Default.WindowPositionLeft = Left;
            Properties.Settings.Default.WindowPositionTop = Top;
            Properties.Settings.Default.WindowWidth = Width;
            Properties.Settings.Default.WindowHeight = Height;
            Properties.Settings.Default.Maximized = WindowState == WindowState.Maximized;

            XmlLayoutSerializer layoutSerializer = new(DockingManager);
            StringWriter stringWriter = new();
            layoutSerializer.Serialize(stringWriter);
            Properties.Settings.Default.Layout = stringWriter.ToString();

            Properties.Settings.Default.Save();
        }
    }
}
