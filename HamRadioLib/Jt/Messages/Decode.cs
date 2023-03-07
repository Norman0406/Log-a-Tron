namespace HamRadioLib.Jt.Messages
{
    public class Decode : JtMessage
    {
        public const uint ID = 2;
        public bool New { get; private set; }
        public DateTime TimeUtc { get; private set; }
        public int SignalNoiseRatio { get; private set; }
        public double DeltaTime { get; private set; }
        public uint DeltaFrequency { get; private set; }
        public string? Mode { get; private set; }
        public string? Message { get; private set; }
        public bool LowConfidence { get; private set; }
        public bool OffAir{ get; private set; }

        public override void FromBytes(byte[] bytes)
        {
            int offset = 0;

            ParseHeader(bytes, ref offset);

            New = bytes.UnpackBool(ref offset);
            TimeUtc = DateTime.Today + TimeSpan.FromMilliseconds(bytes.UnpackUInt32(ref offset));
            SignalNoiseRatio = bytes.UnpackInt32(ref offset);
            DeltaTime = bytes.UnpackDouble(ref offset);
            DeltaFrequency = bytes.UnpackUInt32(ref offset);
            Mode = bytes.UnpackString(ref offset);
            Message = bytes.UnpackString(ref offset);
            LowConfidence = bytes.UnpackBool(ref offset);
            OffAir = bytes.UnpackBool(ref offset);
        }
    }
}
