using HamRadioLib.OmniRig.Handlers;
using OmniRig;
using System.Reactive.Subjects;

namespace HamRadioLib.OmniRig
{
    public class OmniRig
    {
        private readonly OmniRigX _omniRigEngine;

        private readonly ISubject<RigStatus> _status = new ReplaySubject<RigStatus>(1);
        public IObservable<RigStatus> Status => _status;

        private readonly ISubject<int> _frequency = new ReplaySubject<int>(1);
        public IObservable<int> Frequency => _frequency;

        private readonly ISubject<Mode> _mode = new ReplaySubject<Mode>(1);
        public IObservable<Mode> Mode => _mode;

        private readonly ISubject<bool> _transmitting = new ReplaySubject<bool>(1);
        public IObservable<bool> Transmitting => _transmitting;

        private readonly IDictionary<int, Action<IRigX>> _paramsHandlers = new Dictionary<int, Action<IRigX>>();

        private readonly IDictionary<IRigX, Rig> _rigToEnumMapper = new Dictionary<IRigX, Rig>();

        private Rig _rig = Rig.Rig1;
        public Rig Rig
        {
            get => _rig;
            set
            {
                _rig = value;
            }
        }

        public OmniRig()
        {
            // register handlers
            OmniRigFreqAHandler.Register(_paramsHandlers, _frequency);
            OmniRigModeHandler.Register(_paramsHandlers, _mode);
            TransmittingHandler.Register(_paramsHandlers, _transmitting);

            var type = Type.GetTypeFromProgID("OmniRig.OmniRigX") ?? throw new Exception();
            if (Activator.CreateInstance(type) is not OmniRigX omniRigEngine)
            {
                // TODO: exception
                throw new Exception();
            }

            _omniRigEngine = omniRigEngine;

            _rigToEnumMapper[_omniRigEngine.Rig1] = Rig.Rig1;
            _rigToEnumMapper[_omniRigEngine.Rig2] = Rig.Rig2;

            // we want OmniRig interface V.1.1 to 1.99
            // as V2.0 will likely be incompatible  with 1.x
            if (_omniRigEngine.InterfaceVersion < 0x101 && _omniRigEngine.InterfaceVersion > 0x299)
            {
                // exception: omni rig version is unsupported
            }

            OmniRigStatusChanged(1);
            OmniRigStatusChanged(2);

            foreach (var handler in _paramsHandlers)
            {
                handler.Value(_omniRigEngine.Rig1);
            }

            _omniRigEngine.ParamsChange += OmniRigParamsChanged;
            _omniRigEngine.StatusChange += OmniRigStatusChanged;
        }

        //private IObservable<RigValue<T>> MapRigToEnumValue<T>(IObservable<Tuple<IRigX, T>> observable)
        //{
        //    return observable.Select(x => new RigValue<T> { Rig = RigValueFromRig(x.Item1), Value = x.Item2 });
        //}

        private Rig RigValueFromRig(IRigX rig)
        {
            if (rig == _omniRigEngine.Rig1)
            {
                return Rig.Rig1;
            }
            else if (rig == _omniRigEngine.Rig2)
            {
                return Rig.Rig2;
            }
            else
            {
                // TODO
                throw new Exception();
            }
        }

        private IRigX GetRigFromNumber(int rigNumber)
        {
            if (rigNumber == 1)
            {
                return _omniRigEngine.Rig1;
            }
            else if (rigNumber == 2)
            {
                return _omniRigEngine.Rig2;
            }
            else
            {
                // TODO
                throw new Exception();
            }
        }

        private static RigStatus StatusFromStatus(RigStatusX status)
        {
            return status switch
            {
                RigStatusX.ST_NOTCONFIGURED => RigStatus.NotConfigured,
                RigStatusX.ST_DISABLED => RigStatus.Disabled,
                RigStatusX.ST_PORTBUSY => RigStatus.PortBusy,
                RigStatusX.ST_NOTRESPONDING => RigStatus.NotResponding,
                RigStatusX.ST_ONLINE => RigStatus.Online,
                _ => throw new Exception(),
            };
        }

        private void OmniRigStatusChanged(int rigNumber)
        {
            // TODO: only forward for Rig
            if (rigNumber != 1)
            {
                return;
            }

            var rig = GetRigFromNumber(rigNumber);
            _status.OnNext(StatusFromStatus(rig.Status));

            foreach (var handler in _paramsHandlers)
            {
                OmniRigParamsChanged(rigNumber, handler.Key);
            }
        }

        private void OmniRigParamsChanged(int rigNumber, int rigParams)
        {
            // TODO: only forward for Rig
            if (rigNumber != 1)
            {
                return;
            }

            var rig = GetRigFromNumber(rigNumber);
            if (_paramsHandlers.ContainsKey(rigParams))
            {
                _paramsHandlers[rigParams](rig);
            }
        }
    }
}