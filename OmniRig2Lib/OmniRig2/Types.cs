﻿namespace Logatron.OmniRig2.Types
{
    public enum Status
    {
        OmniRigUnavailable,
        NotConfigured,
        Disabled,
        PortBusy,
        NotResponding,
        Online,
    }

    public enum Rig
    {
        Rig1,
        Rig2,
        Rig3,
        Rig4,
    }

    public enum Mode
    {
        CwUsb,
        CwLsb,
        SsbUsb,
        SsbLsb,
        DigitalUsb,
        DigitalLsb,
        AM,
        FM,
    }
}
