using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using Core;
using Chat.DesktopClient.Managers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Chat.DesktopClient.ViewModels;

namespace Chat.DesktopClient.Services
{
    class MessageService
    {
        private const string API = "message";

        private readonly ConnectionManager _connectionManager;
        private readonly MainWindowViewModel _mainViewModel;

        public MessageService(MainWindowViewModel vm)
        {
            _mainViewModel = vm;
            _connectionManager = new ConnectionManager(API);
            Init();
        }

        public async void Init()
        {
            await _connectionManager.StartConnection();
            var receive = ReceiveMessage();
        }

        public void SendMessage(string message)
        {
            Message messageObject = new Message
            {
                Text = message
            };

            var jsonMessage = JsonConvert.SerializeObject(messageObject);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            _connectionManager.Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task ReceiveMessage()
        {
            var buffer = new byte[1024 * 4];

            while (true)
            {
                var result = await _connectionManager.Client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string jsonMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Message message = JsonConvert.DeserializeObject<Message>(jsonMessage);
                _mainViewModel.ReceiveMessage(message.Text);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _connectionManager.Client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
            }
        }
    }
}
