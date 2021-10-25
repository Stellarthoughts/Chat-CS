using Server.BotPM;
using Server.ChatPM;
using System;
using System.Collections.Generic;

namespace Server
{
	public class Programm
	{
		private static int idGlobal = 0;
		private static List<Chat> chatList;
		private static List<User> userList;

		private static void Main(string[] args)
		{
			chatList = new List<Chat>();
			userList = new List<User>();

			Chat mainchat = new Chat();
			User localUser = new User();
			Bot localBot = new Bot();

			mainchat.Connect(localUser);
			mainchat.Connect(localBot);

			while (true)
			{
				try
				{
					localUser.UI.WaitForInput();
				}
				catch(Exception ex)
				{
					break;
				}
			}
		}

		public static int IdGlobal { get => idGlobal++; }
		public static int IdGlobalCheckout { get => idGlobal; }
	}
}
