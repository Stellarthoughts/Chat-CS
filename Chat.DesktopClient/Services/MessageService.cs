using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using Core;
using Chat.DesktopClient.Managers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Chat.DesktopClient.ViewModels;
using NLog;

namespace Chat.DesktopClient.Services
{
    class MessageService
    {
        private const string API = "message";

        private readonly ConnectionManager _connectionManager;
        private readonly MainWindowViewModel _mainViewModel;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public MessageService(MainWindowViewModel vm)
        {
            _mainViewModel = vm;
            _connectionManager = new ConnectionManager(API);
            _connectionManager.ReceivedEvent += ReceiveMessage;
            try
            {
                _ = _connectionManager.StartConnection();
            }
            catch (Exception ex)
            {
                _logger.Error($"Cannot connect to server! Exception: {ex.Message}");
            }
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
            try
            {
                _connectionManager.Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.Error($"Cannot send message! Exception: {ex.Message}");
            }
        }

        public void ReceiveMessage(object sender, string message)
        {
            Message casted = JsonConvert.DeserializeObject<Message>(message);
            _mainViewModel.ReceiveMessage(casted);
        }
    }
}
