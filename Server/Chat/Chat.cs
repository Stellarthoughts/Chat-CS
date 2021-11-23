using System;
using System.Collections.Generic;
using Server.BotPM;
using Server.CommandPM;

namespace Server.ChatPM
{
	public class Chat : ISendReceive
	{
		protected int id;

		protected List<ChatEntity> connected;
		protected List<Message> savedMessages;
		protected CommandInterpreter commandInterpeter;     
		
		public Chat()
		{
			id = Program.IdGlobal;
			connected = new List<ChatEntity>();
			savedMessages = new List<Message>();
			commandInterpeter = new CommandInterpreter(
				this,
				new List<Command> {
					new UnrecognizedCommand(),
					new HelloCommand()
					}
			);
		}

		//public delegate void delegateMsg(object sender, Message msg);     

		//public event delegateMsg NewMessage;

		public void SendMessage(Message msg)
		{
			foreach(ChatEntity con in connected)
			{
				con.ReceiveMessage(msg);
			}    
		}

		public void ReceiveMessage(Message msg)
		{
			Command command = commandInterpeter.InterpretMessage(msg);
			command.Execute();
			OnMessage(msg);
		}

		public void OnMessage(Message msg)
		{
			savedMessages.Add(msg);
			foreach(ChatEntity entity in connected)
			{
				if(entity != msg.Origin) entity.ReceiveMessage(msg);
			}
			//NewMessage(this, msg);
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
