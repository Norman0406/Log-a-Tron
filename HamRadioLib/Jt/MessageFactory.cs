using System.Diagnostics;

namespace HamRadioLib.Jt
{
    internal class MessageFactory
    {
        public static JtMessage? Decode(byte[] bytes)
        {
            int offset = 8;
            uint messageType = bytes.UnpackUInt32(ref offset);

            switch (messageType)
            {
                case Messages.Heartbeat.ID:
                    return CreateMessage<Messages.Heartbeat>(bytes);
                case Messages.Status.ID:
                    return CreateMessage<Messages.Status>(bytes);
                case Messages.Decode.ID:
                    return CreateMessage<Messages.Decode>(bytes);
                case Messages.Clear.ID:
                    return CreateMessage<Messages.Clear>(bytes);
                case Messages.QsoLogged.ID:
                    return CreateMessage<Messages.QsoLogged>(bytes);
                case Messages.Close.ID:
                    return CreateMessage<Messages.Close>(bytes);
                case Messages.AdifLogged.ID:
                    return CreateMessage<Messages.AdifLogged>(bytes);
                default:
                    Debug.WriteLine($"Unknown message type: {messageType}");
                    break;
            }

            return null;
        }

        private static T CreateMessage<T>(byte[] bytes) where T : JtMessage, new()
        {
            T message = new();
            message.FromBytes(bytes);
            return message;
        }
    }
}
