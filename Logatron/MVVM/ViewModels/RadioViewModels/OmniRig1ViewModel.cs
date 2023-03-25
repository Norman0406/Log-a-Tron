using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Logatron.MVVM.ViewModels.RadioViewModels
{
    public class OmniRig1ViewModel : RadioViewModel
    {
        public const string Name = "OmniRig v1";

        private readonly OmniRig1.OmniRig OmniRig = new();
        private readonly CompositeDisposable _subscriptions = new();

        public OmniRig1ViewModel()
        {
            _subscriptions.Add(OmniRig.Status
                .ObserveOnDispatcher()
                .Subscribe(status =>
                {
                    Disabled = status != OmniRig1.Types.Status.Online;
                }));

            _subscriptions.Add(OmniRig.Frequency
                .ObserveOnDispatcher()
                .Subscribe((value) =>
                {
                    Frequency = FrequencyStringFromInteger(value / 1000.0);
                }));

            _subscriptions.Add(OmniRig.Mode
                .ObserveOnDispatcher()
                .Subscribe((value) =>
                {
                    Mode = ModeToString(value);
                }));

            _subscriptions.Add(OmniRig.Transmitting
                .ObserveOnDispatcher()
                .Subscribe((value) =>
                {
                    Tx = value;
                }));
        }

        private static string ModeToString(OmniRig1.Types.Mode mode)
        {
            return mode switch
            {
                OmniRig1.Types.Mode.CwUsb => "CW-R",
                OmniRig1.Types.Mode.CwLsb => "CW",
                OmniRig1.Types.Mode.SsbUsb => "USB",
                OmniRig1.Types.Mode.SsbLsb => "LSB",
                OmniRig1.Types.Mode.DigitalUsb => "USB-D",
                OmniRig1.Types.Mode.DigitalLsb => "LSB-D",
                OmniRig1.Types.Mode.AM => "AM",
                OmniRig1.Types.Mode.FM => "FM",
                _ => "Unknown",
            }; ;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _subscriptions.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
