namespace Logatron.Jt.Messages
{
    public class Close : JtMessage
    {
        public const uint ID = 6;

        public override void FromBytes(byte[] bytes)
        {
            int offset = 0;

            ParseHeader(bytes, ref offset);
        }
    }
}
