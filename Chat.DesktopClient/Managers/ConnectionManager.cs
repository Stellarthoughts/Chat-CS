using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using System.Text;

namespace Chat.DesktopClient.Managers
{

    class ConnectionManager
    {
        private readonly string _api;

        public ClientWebSocket Client { get; private set; }

        public ConnectionManager(string api)
        {
            _api = api;
        }

        public event EventHandler<string> ReceivedEvent;

        public async Task StartConnection()
        {
            Client = new ClientWebSocket();
            await Client.ConnectAsync(new Uri($"ws://localhost:5000/{_api}"), CancellationToken.None);
            var receive = ReceiveMessage();
        }

        protected void OnReceivedEvent(string message)
        {
            EventHandler<string> receivedEvent = ReceivedEvent;

            if(receivedEvent != null)
            {
                receivedEvent(this, message);
            }
        }

        private async Task ReceiveMessage()
        {
            var buffer = new byte[1024 * 4];

            while (true)
            {
                var result = await Client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await Client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
                string jsonMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                OnReceivedEvent(jsonMessage);
            }
        }
    }
}
