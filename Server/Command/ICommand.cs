using System.Collections.Generic;
using System;
using Server.ChatPM;

namespace Server.CommandPM
{
	public abstract class Command
	{
		protected List<string> keywords;
		protected string regulatorSymbol;
		protected bool hasArgs;

		protected List<string> arguments;
		public void setArguments(List<string> arguments) => this.arguments = arguments;

		public Command()
		{
			keywords = new List<string>();
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

		public abstract int Recognize(Message msg);
		public abstract void Execute();

		public List<string> Keywords => keywords;
		public string RegulatorSymbol => regulatorSymbol;
	}


	public class UnrecognizedCommand : Command
	{ 
		public UnrecognizedCommand() : base()
        {

        }
		public override void Execute()
		{

		}
		public override int Recognize(Message msg)
		{
			return -1;
		}
	}

	public class ConnectCommand : Command
    {
		public ConnectCommand() : base()
        {
            keywords.Add("connect");
			hasArgs = true;
        }
		public override void Execute()
		{
			if(hasArgs == true && arguments.Count == 0) throw new Exception();
			
		}
		public override int Recognize(Message msg)
        {
			return -1;
        }
	}
}
