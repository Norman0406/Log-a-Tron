using OmniRig;
using System.Reactive.Subjects;

namespace HamRadioLib.OmniRig.Handlers
{
    internal class OmniRigFreqHandler
    {
        public static void Register(IDictionary<int, Action<IRigX>> handlers, ISubject<int> frequency)
        {
            void handler(IRigX rig)
            {
                frequency.OnNext(rig.Freq);
            }

            handlers[(int)RigParamX.PM_FREQ] = handler;
        }
    }

    internal class OmniRigFreqAHandler
    {
        public static void Register(IDictionary<int, Action<IRigX>> handlers, ISubject<int> frequency)
        {
            void handler(IRigX rig)
            {
                frequency.OnNext(rig.FreqA);
            }

            handlers[(int)RigParamX.PM_FREQA] = handler;
        }
    }

    internal class OmniRigFreqBHandler
    {
        public static void Register(IDictionary<int, Action<IRigX>> handlers, ISubject<int> frequency)
        {
            void handler(IRigX rig)
            {
                frequency.OnNext(rig.FreqB);
            }

            handlers[(int)RigParamX.PM_FREQB] = handler;
        }
    }
}
