using System;

namespace Server.ChatPM
{
	public class UserInterface
	{
		private User usr;

		public UserInterface(User usr)
		{
			this.usr = usr;
		}

		public void WaitForInput()
		{
			Console.Write("> ");
			string s = Console.ReadLine();
			if(s.Length > 0)
			{
				usr.CreateMessage(s);
			}
		}

		public void ShowMessage(Message msg)
		{
			Console.WriteLine(msg.Content);
		}

		public User Usr => usr;
	}
}
