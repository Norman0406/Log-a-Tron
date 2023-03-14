namespace Logatron.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private LogbookViewModel _logbookViewModel;
        public LogbookViewModel LogbookViewModel
        {
            get { return _logbookViewModel; }
            set { SetProperty(ref _logbookViewModel, value); }
        }

        private RadioViewModel _radioViewModel;
        public RadioViewModel RadioViewModel
        {
            get { return _radioViewModel; }
            set { SetProperty(ref _radioViewModel, value); }
        }

        private MapViewModel _mapViewModel;
        public MapViewModel MapViewModel
        {
            get { return _mapViewModel; }
            set { SetProperty(ref _mapViewModel, value); }
        }

        //private readonly HamRadioLib.Jt.Client _jtClient;

        public MainViewModel()
        {
            var database = new Models.Logbook.Database("logbook.db");
            _logbookViewModel = new LogbookViewModel(database);

            _radioViewModel = new RadioViewModels.OmniRig1ViewModel();

            _mapViewModel = new MapViewModel();

            //// initialize jt client
            //_jtClient = new HamRadioLib.Jt.Client("239.255.255.0", 2237);
            //_jtClient.Alerts.Subscribe((alert) =>
            //{
            //});

            //// initialize lotw client
            //var credential = Util.CredentialManager.LoadCredential("LotW");
            //if (credential != null)
            //{
            //    HamRadioLib.LotW.Client lotwClient = new(credential.UserName, credential.Password);
            //    var adifFile = lotwClient.Query().Result;
            //}
        }
    }
}
