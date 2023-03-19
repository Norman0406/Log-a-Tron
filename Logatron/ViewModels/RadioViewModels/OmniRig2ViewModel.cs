using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Logatron.ViewModels.RadioViewModels
{
    public class OmniRig2ViewModel : RadioViewModel
    {
        public const string Name = "OmniRig v2";

        private readonly OmniRig2.OmniRig OmniRig = new();
        private readonly CompositeDisposable _subscriptions = new();

        public OmniRig2ViewModel()
        {
            _subscriptions.Add(OmniRig.Status
                .ObserveOnDispatcher()
                .Subscribe(status =>
                {
                    Disabled = status != OmniRig2.Types.Status.Online;
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

        private static string ModeToString(OmniRig2.Types.Mode mode)
        {
            return mode switch
            {
                OmniRig2.Types.Mode.CwUsb => "CW-R",
                OmniRig2.Types.Mode.CwLsb => "CW",
                OmniRig2.Types.Mode.SsbUsb => "USB",
                OmniRig2.Types.Mode.SsbLsb => "LSB",
                OmniRig2.Types.Mode.DigitalUsb => "USB-D",
                OmniRig2.Types.Mode.DigitalLsb => "LSB-D",
                OmniRig2.Types.Mode.AM => "AM",
                OmniRig2.Types.Mode.FM => "FM",
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
