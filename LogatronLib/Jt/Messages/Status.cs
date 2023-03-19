namespace Logatron.Jt.Messages
{
    public class Status : JtMessage
    {
        public const uint ID = 1;
        public ulong DialFrequency { get; private set; }
        public string? Mode { get; private set; }
        public string? DxCall { get; private set; }
        public string? Report { get; private set; }
        public string? TxMode { get; private set; }
        public bool TxEnabled { get; private set; }
        public bool Transmitting { get; private set; }
        public bool Decoding { get; private set; }
        public uint RxFrequency { get; private set; }
        public uint TxFrequency { get; private set; }
        public string? MyCall { get; private set; }
        public string? MyGrid { get; private set; }
        public string? DxGrid { get; private set; }
        public bool TxWatchdog { get; private set; }
        public string? SubMode { get; private set; }
        public bool FastMode { get; private set; }

        // TODO: WSJT parameters
        // TODO: JTDX parameters

        public override void FromBytes(byte[] bytes)
        {
            int offset = 0;

            ParseHeader(bytes, ref offset);

            DialFrequency = bytes.UnpackUInt64(ref offset);
            Mode = bytes.UnpackString(ref offset);
            DxCall = bytes.UnpackString(ref offset);
            Report = bytes.UnpackString(ref offset);
            TxMode = bytes.UnpackString(ref offset);
            TxEnabled = bytes.UnpackBool(ref offset);
            Transmitting = bytes.UnpackBool(ref offset);
            Decoding = bytes.UnpackBool(ref offset);
            RxFrequency = bytes.UnpackUInt32(ref offset);
            TxFrequency = bytes.UnpackUInt32(ref offset);
            MyCall = bytes.UnpackString(ref offset);
            MyGrid = bytes.UnpackString(ref offset);
            DxGrid = bytes.UnpackString(ref offset);
            TxWatchdog = bytes.UnpackBool(ref offset);
            SubMode = bytes.UnpackString(ref offset);
            FastMode = bytes.UnpackBool(ref offset);
        }
    }
}
