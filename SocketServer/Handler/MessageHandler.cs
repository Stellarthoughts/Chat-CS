using SocketServer.SocketsManager;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Handlers
{
    public class MessageHandler : SocketHandler
    {
        public MessageHandler(ConnectionManager connectionManager) : base(connectionManager)
        {

        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketId = ConnectionManager.GetId(socket);
            await SendMessageToAll($"{socketId} connected");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = ConnectionManager.GetId(socket);
            var message = $"{socketId} said {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendMessageToAll(message);
        }
    }
}
