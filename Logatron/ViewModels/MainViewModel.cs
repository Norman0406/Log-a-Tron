using Logatron.Models.Logbook;

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

        public MainViewModel()
        {
            DatabaseManager dbManager = new("logbook.db");

            _logbookViewModel = new LogbookViewModel(dbManager);
            _logbookViewModel.Init();

            _radioViewModel = new RadioViewModels.OmniRig1ViewModel();

            _mapViewModel = new MapViewModel();

            // initialize lotw client
            //var credential = Util.CredentialManager.LoadCredential("LotW");
            //if (credential != null)
            //{
            //    HamRadioLib.LotW.Client lotwClient = new(credential.UserName, credential.Password);
            //    var adifFile = lotwClient.Query().Result;
            //}

            //// initialize qrz.com client
            //var qrzCredential = Util.CredentialManager.LoadCredential("QRZ.com");
            //if (qrzCredential != null)
            //{
            //    var qrzClient = new HamRadioLib.QrzDotCom.Client(qrzCredential.UserName, qrzCredential.Password);
            //    var callsignResult = qrzClient.QueryCallsign("HB9HTX").Result;
            //    var dxccResult = qrzClient.QueryDxcc(291).Result;
            //}

            //// initialize jt client
            //var _jtClient = new HamRadioLib.Jt.Client("239.255.255.0", 2237);
            //_jtClient.Alerts.Subscribe((alert) =>
            //{
            //});
        }
    }
}
