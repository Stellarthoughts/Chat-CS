using Server.ChatPM;
using Server.CommandPM;
using System.Collections.Generic;

namespace Server.BotPM
{
	public class Bot : ChatEntity
	{
		protected CommandInterpreter commandInterpeter;

		public Bot()
		{
			commandInterpeter = new CommandInterpreter(
			   this,
			   new List<Command> {
					new UnrecognizedCommand(),
					new HelloCommand()
				   }
		   );
		}

		public override void ReceiveMessage(Message msg)
		{
			Command command = commandInterpeter.InterpretMessage(msg);
			command.Execute();
		}
		public override void SendMessage(Message msg)
		{
			connectedTo.ReceiveMessage(msg);
		}
	}
}
