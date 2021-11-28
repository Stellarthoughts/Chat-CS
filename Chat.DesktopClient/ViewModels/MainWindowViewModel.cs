namespace Chat.DesktopClient.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Services;
    using System;

    public class MainWindowViewModel : BindableBase
    {
        private MessageService _messageService;

        private string _message = "";
        private string _output = "";
        private string _nickname = "Enter your nickname here.";
        private string _recepient = "Enter your target here.";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public string Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }
        public string Recepient
        {
            get => _recepient;
            set => SetProperty(ref _recepient, value);
        }

        public DelegateCommand SendMessageCommand { get; private set; }

        public MainWindowViewModel()
        {
            SendMessageCommand = new DelegateCommand(SendMessage);
            _messageService = new MessageService(this);
        }

        private void SendMessage()
        {
            try
            {
                _messageService.SendMessage(_message, Nickname, Recepient);
            }
            catch (NullReferenceException ex)
            {
                Output = ex.Message;
            }
        }

        public void ReceiveMessage(string message)
        {
            if(Output == "") Output = $"{message}";
            else Output = $"{Output}\n{message}";
        }
    }
}
