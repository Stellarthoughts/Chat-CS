using Server.ChatPM;
using System;
using System.Collections.Generic;

namespace Server.CommandPM
{
	public class CommandInterpreter
	{
		private List<Command> commandList;
		private ISendReceive obj;

		public CommandInterpreter(ISendReceive obj, List<Command> commandList)
		{
			this.obj = obj;
			this.commandList = commandList;
		}

		public Command InterpretMessage(ChatPM.Message msg)
		{
			Command standardReturn = new UnrecognizedCommand();
			Command resultCommand = standardReturn;

			foreach (Command command in commandList)
			{
				if(command.Validate(msg) == -1) continue;
				resultCommand = command;
			}

			try
			{
				resultCommand.SetArguments(obj, msg);
			}
			catch(Exception ex)
			{
				return standardReturn;
			}

			return resultCommand;
		}
	}
}
