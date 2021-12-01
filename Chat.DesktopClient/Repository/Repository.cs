using Core;
using System.Collections.Generic;

namespace Chat.DesktopClient.Repository
{
    public class Repository : IRepository
    {
        private List<Message> _messages = new();

        public List<Message> GetMessages()
        {
            return _messages;
        }

        public void SaveMessage(Message message)
        {
            _messages.Add(message);
        }
    }
}