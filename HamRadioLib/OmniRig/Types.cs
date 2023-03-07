namespace HamRadioLib.OmniRig
{
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

    public enum Rig
    {
        Rig1,
        Rig2,
    }

    public enum RigStatus
    {
        NotConfigured,
        Disabled,
        PortBusy,
        NotResponding,
        Online,
    }

    public struct RigValue<T>
    {
        public Rig Rig;

        public T Value;
    }
}
