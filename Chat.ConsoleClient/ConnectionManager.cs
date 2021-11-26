using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.ConsoleClient
{
    using System.Net.WebSockets;
    using System.Threading;

    class ConnectionManager
    {
        private readonly string _api;

        public ConnectionManager(string api)
        {
            _api = api;
        }

        public async Task StartConnection()
        {
            var client = new ClientWebSocket();
            await client.ConnectAsync(new Uri($"ws://localhost:5000/{_api}"), CancellationToken.None);
            Console.WriteLine("Connected to server");

            var send = Task.Run(async () =>
            {
                string message;

                while ((message = Console.ReadLine()) != null && message != string.Empty)
                {
                    var bytes = Encoding.UTF8.GetBytes(message);
                    await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            });

            var receive = ReciveAsync(client);
            await Task.WhenAll(send, receive);
        }

        private async Task ReciveAsync(ClientWebSocket client)
        {
            var buffer = new byte[1024 * 4];

            while (true)
            {
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
            }
        }


    }
}
