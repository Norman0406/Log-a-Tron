using OmniRig;
using System;
using System.Reactive.Subjects;

namespace HamRadioLib.OmniRig1.Handlers
{
    internal class OmniRigModeHandler
    {
        public static void Register(IDictionary<int, Action<IRigX>> handlers, ISubject<Types.Mode> mode)
        {
            void handler(IRigX rig)
            {
                switch (rig.Mode)
                {
                    case RigParamX.PM_CW_U:
                        mode.OnNext(Types.Mode.CwUsb);
                        break;
                    case RigParamX.PM_CW_L:
                        mode.OnNext(Types.Mode.CwLsb);
                        break;
                    case RigParamX.PM_SSB_U:
                        mode.OnNext(Types.Mode.SsbUsb);
                        break;
                    case RigParamX.PM_SSB_L:
                        mode.OnNext(Types.Mode.SsbLsb);
                        break;
                    case RigParamX.PM_DIG_U:
                        mode.OnNext(Types.Mode.DigitalUsb);
                        break;
                    case RigParamX.PM_DIG_L:
                        mode.OnNext(Types.Mode.DigitalLsb);
                        break;
                    case RigParamX.PM_AM:
                        mode.OnNext(Types.Mode.AM);
                        break;
                    case RigParamX.PM_FM:
                        mode.OnNext(Types.Mode.FM);
                        break;
                }
            }

            handlers[(int)RigParamX.PM_CW_U] = handler;
            handlers[(int)RigParamX.PM_CW_L] = handler;
            handlers[(int)RigParamX.PM_SSB_U] = handler;
            handlers[(int)RigParamX.PM_SSB_L] = handler;
            handlers[(int)RigParamX.PM_DIG_U] = handler;
            handlers[(int)RigParamX.PM_DIG_L] = handler;
            handlers[(int)RigParamX.PM_AM] = handler;
            handlers[(int)RigParamX.PM_FM] = handler;
        }
    }
}
