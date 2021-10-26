using System.Collections.Generic;
using System;
using Server.ChatPM;
using Server.BotPM;

namespace Server.CommandPM
{
	public abstract class Command
	{
		protected List<string> keywords;
		protected string regulatorSymbol;

		protected List<object> args;

		public Command()
		{
			args = new List<object>();
			keywords = new List<string>();
			regulatorSymbol = "";
		}

		public int Validate(Message msg)
		{
			if (!msg.Content.Contains(RegulatorSymbol)) return -1;
			foreach (string keyword in Keywords)
			{
				if (!msg.Content.Contains(keyword)) return -1;
			}
			return 0;
		}

		public abstract int SetArguments(ISendReceive obj, Message msg);

		public abstract void Execute();

		public List<string> Keywords => keywords;
		public string RegulatorSymbol => regulatorSymbol;
	}


	public class UnrecognizedCommand : Command
	{ 
		public UnrecognizedCommand() : base()
		{

		}
		public override int SetArguments(ISendReceive obj, Message msg)
		{
			return 0;
		}
		public override void Execute()
		{
			return;
		}
	}

	public class HelloCommand : Command
	{
		public HelloCommand() : base()
		{
			keywords.Add("hello");
		}
		public override int SetArguments(ISendReceive obj, Message msg)
		{
			args.Add(obj);
			args.Add(msg.Origin);
			args.Add("> " + obj + ": Hello you too!");
			return 0;
		}
		public override void Execute()
		{
			(args[0] as ISendReceive).SendMessage(new Message()
			{
				Origin = args[0] as ISendReceive,
				Destination = args[1] as ISendReceive,
				Content = args[2] as string
			}
			);
		}
	}
}
