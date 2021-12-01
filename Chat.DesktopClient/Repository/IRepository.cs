using Core;
using System.Collections.Generic;

namespace Chat.DesktopClient.Repository
{
    interface IRepository
    {
        public void SaveMessage(Message message);
        public List<Message> GetMessages();
    }
}
