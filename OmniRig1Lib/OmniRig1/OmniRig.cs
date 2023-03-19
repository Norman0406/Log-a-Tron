using OmniRig;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Logatron.OmniRig1
{
    public class OmniRig
    {
        private OmniRigX _omniRig;
        private readonly IObservable<bool> _omniRigInitializer;

        private readonly ISubject<Types.Status> _status = new BehaviorSubject<Types.Status>(Types.Status.OmniRigUnavailable);
        public IObservable<Types.Status> Status => _omniRigInitializer.Select(_ => _status).Switch();

        private readonly ISubject<int> _frequency = new ReplaySubject<int>(1);
        public IObservable<int> Frequency => _omniRigInitializer.Select(_ => _frequency).Switch();

        private readonly ISubject<Types.Mode> _mode = new ReplaySubject<Types.Mode>(1);
        public IObservable<Types.Mode> Mode => _omniRigInitializer.Select(_ => _mode).Switch();

        private readonly ISubject<bool> _transmitting = new ReplaySubject<bool>(1);
        public IObservable<bool> Transmitting => _transmitting.Select(_ => _transmitting).Switch();

        private readonly IDictionary<int, Action<IRigX>> _paramsHandlers = new Dictionary<int, Action<IRigX>>();

        private static readonly IDictionary<int, Types.Rig> _rigNumberToRigMapping = new Dictionary<int, Types.Rig>()
        {
            { 1, Types.Rig.Rig1 },
            { 2, Types.Rig.Rig2 }
        };

        private static readonly IDictionary<Types.Rig, int> _rigToRigNumerMapping = new Dictionary<Types.Rig, int>()
        {
            { Types.Rig.Rig1, 1 },
            { Types.Rig.Rig2, 2 }
        };

        private Types.Rig _rig = Types.Rig.Rig1;
        public Types.Rig Rig
        {
            get => _rig;
            set
            {
                _rig = value;
            }
        }

        public OmniRig()
        {
            _omniRigInitializer = Observable.Defer(() =>
            {
                Initialize();
                return Observable.Return(true);
            })
            .SubscribeOn(NewThreadScheduler.Default)
            .ObserveOn(NewThreadScheduler.Default)
            .Publish()
            .RefCount();
        }

        private void Initialize()
        {
            try
            {
                // register handlers
                Handlers.OmniRigFreqAHandler.Register(_paramsHandlers, _frequency);
                Handlers.OmniRigModeHandler.Register(_paramsHandlers, _mode);
                Handlers.TransmittingHandler.Register(_paramsHandlers, _transmitting);

                var omniRig = new OmniRigX();

                // we want OmniRig interface V.1.1 to 1.99
                // as V2.0 will likely be incompatible  with 1.x
                if (omniRig.InterfaceVersion < 0x101 && omniRig.InterfaceVersion > 0x299)
                {
                    throw new Exception();
                    // exception: omni rig version is unsupported
                }

                _omniRig = omniRig;

                foreach (var handler in _paramsHandlers)
                {
                    handler.Value(_omniRig.Rig1);
                }

                _omniRig.StatusChange += StatusChanged;
                _omniRig.ParamsChange += ParamsChanged;

                StatusChanged(GetRigNumberFromRig(Rig));
            }
            catch (Exception)
            {
                _status.OnNext(Types.Status.OmniRigUnavailable);
            }
        }

        private void StatusChanged(int rigNumber)
        {
            if (GetRigFromRigNumber(rigNumber) != Rig)
            {
                return;
            }

            var rig = GetRigXFromNumber(_omniRig, rigNumber);
            _status.OnNext(StatusFromStatus(rig.Status));

            foreach (var handler in _paramsHandlers)
            {
                ParamsChanged(rigNumber, handler.Key);
            }
        }

        private void ParamsChanged(int rigNumber, int rigParams)
        {
            if (GetRigFromRigNumber(rigNumber) != Rig)
            {
                return;
            }

            var rig = GetRigXFromNumber(_omniRig, rigNumber);
            if (_paramsHandlers.ContainsKey(rigParams))
            {
                _paramsHandlers[rigParams](rig);
            }
        }

        private static IRigX GetRigXFromNumber(OmniRigX omniRig, int rigNumber)
        {
            if (rigNumber == 1)
            {
                return omniRig.Rig1;
            }
            else if (rigNumber == 2)
            {
                return omniRig.Rig2;
            }
            else
            {
                // TODO
                throw new Exception();
            }
        }

        private static Types.Rig GetRigFromRigNumber(int rigNumber)
        {
            return _rigNumberToRigMapping[rigNumber];
        }

        private static int GetRigNumberFromRig(Types.Rig rig)
        {
            return _rigToRigNumerMapping[rig];
        }

        private static Types.Status StatusFromStatus(RigStatusX status)
        {
            return status switch
            {
                RigStatusX.ST_NOTCONFIGURED => Types.Status.NotConfigured,
                RigStatusX.ST_DISABLED => Types.Status.Disabled,
                RigStatusX.ST_PORTBUSY => Types.Status.PortBusy,
                RigStatusX.ST_NOTRESPONDING => Types.Status.NotResponding,
                RigStatusX.ST_ONLINE => Types.Status.Online,
                _ => throw new Exception(),
            };
        }
    }
}