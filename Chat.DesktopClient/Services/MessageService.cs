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
            _connectionManager.ReceivedEvent += ReceiveMessage;
            _ = _connectionManager.StartConnection();
        }

        public void SendMessage(string message, string origin, string target)
        {
            Message messageObject = new Message
            {
                Text = message,
                Origin = origin,
                Target = target
            };

            var jsonMessage = JsonConvert.SerializeObject(messageObject);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            _connectionManager.Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public void ReceiveMessage(object sender, string message)
        {
            Message casted = JsonConvert.DeserializeObject<Message>(message);
            _mainViewModel.ReceiveMessage(casted);
        }
    }
}
