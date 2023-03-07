using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Subjects;

namespace HamRadioLib.Jt
{
    public class Client
    {
        private readonly ISubject<JtMessage> _alerts = new Subject<JtMessage>();
        public IObservable<JtMessage> Alerts => _alerts;

        private readonly UdpClient _client;

        public Client(string ipAddress, int port)
        {
            IPAddress multicastIpAddress = IPAddress.Parse(ipAddress);
            IPAddress localIpAddress = IPAddress.Any;

            _client = new()
            {
                ExclusiveAddressUse = false
            };
            _client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _client.Client.Bind(new IPEndPoint(localIpAddress, port));
            _client.JoinMulticastGroup(multicastIpAddress, localIpAddress);

            _client.BeginReceive(MessageReceived, null);
        }

        private void MessageReceived(IAsyncResult result)
        {
            byte[] bytes;

            try
            {
                IPEndPoint? remoteEP = null;
                bytes = _client.EndReceive(result, ref remoteEP);
            }
            catch (SocketException ex)
            {
                Debug.WriteLine(ex);
                return;
            }
            catch (ObjectDisposedException ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            try
            {
                JtMessage? message = MessageFactory.Decode(bytes);

                if (message != null)
                {
                    _alerts.OnNext(message);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }

            _client.BeginReceive(MessageReceived, null);
        }
    }
}
