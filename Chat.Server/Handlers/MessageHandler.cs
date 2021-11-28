using Chat.Server.SocketsManager;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using Core;
using Newtonsoft.Json;
using System.Linq;

namespace Chat.Server.Handlers
{
    public class MessageHandler : SocketHandler
    {
        private readonly ConcurrentDictionary<User, WebSocket> _connections = new();

        public MessageHandler(ConnectionManager connectionManager) : base(connectionManager)
        {

        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = $"User{_connections.Count}"
            };

            _connections.TryAdd(user, socket);

            await SendMessageToAll($"{user.Name} connected");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Message messageObject = JsonConvert.DeserializeObject<Message>(message);
            var keysOrigin = _connections.Where(entry => entry.Value == socket).Select(entry => entry.Key);

            // Update nickname
            foreach(var key in keysOrigin)
            {
                key.Name = messageObject.Origin;
            }

            // Check target
            if (messageObject.Target == "")
            {
                await SendMessageToAll($"{messageObject.Origin} to All: {messageObject.Text}");
            }
            else
            {
                var socketTarget = _connections.Where(entry => entry.Key.Name == messageObject.Target).Select(entry => entry.Value);
                foreach(WebSocket sock in socketTarget)
                {
                    await SendMessage(sock, $"{messageObject.Origin} to {messageObject.Target}: {messageObject.Text}");
                }
            }
        }
    }
}
