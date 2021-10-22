using System.Collections.Generic;

namespace Server.CommandPM
{
	public class CommandInterpreter
	{
		private List<Command> commandList;

		public CommandInterpreter(List<Command> commandList)
        {
			this.commandList = commandList;
        }

		public Command InterpretMessage(ChatPM.Message msg)
		{
            foreach(Command command in commandList)
            {
				if(command.Validate(msg) == -1) continue;
				if(command.Recognize(msg) == -1) continue;
				return command;
            }

			return new UnrecognizedCommand();
		}
	}
}
