using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer.SocketsManager
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<Guid, WebSocket> _connections = new ConcurrentDictionary<Guid, WebSocket>();

        public WebSocket GetSocketById(Guid id)
        {
            return _connections.FirstOrDefault(x => x.Key == id).Value;
        }

        public ConcurrentDictionary<Guid, WebSocket> GetAllConnections()
        {
            return _connections;
        }

        public Guid GetId(WebSocket socket)
        {
            return _connections.FirstOrDefault(x => x.Value == socket).Key;
        }

        public async void RemoveConnection(Guid id)
        {
            _connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, $"Connection closed", CancellationToken.None);
        }

        public async Task RemoveSocketAsync(Guid id)
        {
            _connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, $"Connection closed", CancellationToken.None);
        }

        public void AddSocket(WebSocket socket)
        {
            _connections.TryAdd(GetConnectionId(), socket);
        }

        private Guid GetConnectionId()
        {
            return Guid.NewGuid();
        }
    }
}
