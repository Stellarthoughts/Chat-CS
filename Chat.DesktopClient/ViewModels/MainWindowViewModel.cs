namespace Chat.DesktopClient.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Services;

    public class MainWindowViewModel : BindableBase
    {
        private readonly MessageService _messageService;

        private string _message;
        private string _output;

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

        public DelegateCommand SendMessageCommand { get; private set; }

        public MainWindowViewModel()
        {
            _messageService = new MessageService(this);
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _messageService.SendMessage(_message);
        }

        public void ReceiveMessage(string message)
        {
            Output = Output + "\n" + message;
        }
    }
}
