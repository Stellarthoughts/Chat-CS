using System;
using System.Collections.Generic;

using Server.CommandPM;

namespace Server.ChatPM
{
    public class Chat
    {
        protected Server srv;
        protected int id;

        protected List<ChatEntity> connected;
        protected List<Message> savedMessages;
        protected CommandInterpreter commandInterpeter;
        
        
        public Chat(Server srv)
        {
            this.srv = srv;
            id = Server.IdGlobal;
            connected = new List<ChatEntity>();
            savedMessages = new List<Message>();
            commandInterpeter = new CommandInterpreter(
                new List<Command>()
                );

        }

        public void ReceiveMessage(Message msg)
        {
            NewMessage(msg);
        }

        public void NewMessage(Message msg)
        {
            savedMessages.Add(msg);
        }

        public int Connect(ChatEntity entity)
        {
            entity.ConnectedTo = this;
            connected.Add(entity);
            return 0;
        }

        public int Disconnect(ChatEntity entity)
        {
            entity.ConnectedTo = null;
            connected.Remove(entity);
            return 0;
        }
    }
}
