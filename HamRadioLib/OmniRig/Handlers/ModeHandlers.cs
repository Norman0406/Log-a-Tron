﻿using OmniRig;
using System.Reactive.Subjects;

namespace HamRadioLib.OmniRig.Handlers
{
    internal class OmniRigModeHandler
    {
        public static void Register(IDictionary<int, Action<IRigX>> handlers, ISubject<Mode> mode)
        {
            void handler(IRigX rig)
            {
                switch (rig.Mode)
                {
                    case RigParamX.PM_CW_U:
                        mode.OnNext(Mode.CwUsb);
                        break;
                    case RigParamX.PM_CW_L:
                        mode.OnNext(Mode.CwLsb);
                        break;
                    case RigParamX.PM_SSB_U:
                        mode.OnNext(Mode.SsbUsb);
                        break;
                    case RigParamX.PM_SSB_L:
                        mode.OnNext(Mode.SsbLsb);
                        break;
                    case RigParamX.PM_DIG_U:
                        mode.OnNext(Mode.DigitalUsb);
                        break;
                    case RigParamX.PM_DIG_L:
                        mode.OnNext(Mode.DigitalLsb);
                        break;
                    case RigParamX.PM_AM:
                        mode.OnNext(Mode.AM);
                        break;
                    case RigParamX.PM_FM:
                        mode.OnNext(Mode.FM);
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
