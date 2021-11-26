using System;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

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

        public async Task StartConnection()
        {
            Client = new ClientWebSocket();
            await Client.ConnectAsync(new Uri($"ws://localhost:5000/{_api}"), CancellationToken.None);
        }
    }
}
