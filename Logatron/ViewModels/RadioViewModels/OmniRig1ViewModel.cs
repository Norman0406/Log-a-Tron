using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Logatron.ViewModels.RadioViewModels;

public class OmniRig1ViewModel : RadioViewModel
{
    public const string Name = "OmniRig v1";

    private readonly OmniRig1.OmniRig OmniRig = new();
    private readonly CompositeDisposable _subscriptions = new();

    public OmniRig1ViewModel()
    {
        _subscriptions.Add(OmniRig.Status
            .Subscribe(status =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    Disabled = status != OmniRig1.Types.Status.Online;
                });
            }));

        _subscriptions.Add(OmniRig.Frequency
            .Subscribe((value) =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    Frequency = FrequencyStringFromInteger(value / 1000.0);
                });
            }));

        _subscriptions.Add(OmniRig.Mode
            .Subscribe((value) =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    Mode = ModeToString(value);
                });
            }));

        _subscriptions.Add(OmniRig.Transmitting
            .Subscribe((value) =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    Tx = value;
                });
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
