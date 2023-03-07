namespace HamRadioLib.Jt.Messages
{
    public class Clear : JtMessage
    {
        public const uint ID = 3;

        public override void FromBytes(byte[] bytes)
        {
            int offset = 0;

            ParseHeader(bytes, ref offset);
        }
    }
}
