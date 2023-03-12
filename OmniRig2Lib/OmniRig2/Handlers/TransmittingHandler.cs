using OmniRig;
using System.Reactive.Subjects;

namespace HamRadioLib.OmniRig2.Handlers
{
    internal class TransmittingHandler
    {
        public static void Register(IDictionary<int, Action<IRigX>> handlers, ISubject<bool> transmitting)
        {
            void handler(IRigX rig)
            {
                if (rig.Tx == RigParamX.PM_TX)
                {
                    transmitting.OnNext(true);
                }
                else if (rig.Tx == RigParamX.PM_RX)
                {
                    transmitting.OnNext(false);
                }
            }

            handlers[(int)RigParamX.PM_RX] = handler;
            handlers[(int)RigParamX.PM_TX] = handler;
        }
    }
}
