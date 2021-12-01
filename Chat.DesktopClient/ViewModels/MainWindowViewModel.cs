namespace Chat.DesktopClient.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Services;
    using System;
    using Repository;
    using Core;

    public class MainWindowViewModel : BindableBase
    {
        private MessageService _messageService;
        private Repository _repository;

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
        public DelegateCommand ClearOutputCommand { get; private set; }
        public DelegateCommand LoadMessagesCommand { get; private set; }

        public MainWindowViewModel()
        {
            SendMessageCommand = new DelegateCommand(SendMessage);
            ClearOutputCommand = new DelegateCommand(ClearOutput);
            LoadMessagesCommand = new DelegateCommand(LoadMessages);
            _messageService = new MessageService(this);
            _repository = new Repository();
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

        public void ReceiveMessage(Message message)
        {
            _repository.SaveMessage(message);
            if (Output.Length == 0) Output = message.Text;
            else Output = $"{Output}\n{message.Text}";
        }

        public void ClearOutput()
        {
            Output = "";
        }

        public void LoadMessages()
        {
            var messages = _repository.GetMessages();
            foreach(var message in messages)
            {
                if (Output.Length == 0) Output = message.Text;
                else Output = $"{Output}\n{message.Text}";
            }
        }
    }
}
