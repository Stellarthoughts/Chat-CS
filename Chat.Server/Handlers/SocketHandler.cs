namespace Chat.Server.Handlers
{
    using System;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using SocketsManager;
    using Newtonsoft.Json;

    public abstract class SocketHandler
    {
        public ConnectionManager ConnectionManager { get; set; }

        public SocketHandler(ConnectionManager connectionManager)
        {
            ConnectionManager = connectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            await Task.Run(() => { ConnectionManager.AddSocket(socket); });
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await ConnectionManager.RemoveSocketAsync(ConnectionManager.GetId(socket));
        }

        public async Task SendMessage(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            Message messageObject = new Message
            {
                Text = message
            };

            var jsonMessage = JsonConvert.SerializeObject(messageObject);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessage(Guid id, string message)
        {
            await SendMessage(ConnectionManager.GetSocketById(id), message);

        }

        public async Task SendMessageToAll(string message)
        {
            foreach (var connection in ConnectionManager.GetAllConnections())
            {
                await SendMessage(connection.Value, message);
            }
        }

        public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
