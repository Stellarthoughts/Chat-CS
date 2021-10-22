using Server.ChatPM;
using System;
using System.Collections.Generic;

namespace Server
{
	public class Server
	{
		private static int idGlobal = 0;
		private List<Chat> chatList;
		private List<User> userList;

		private static void Main(string[] args)
		{
			Server srv = new Server();
			Chat mainchat = new Chat(srv);
			User localUser = new User(srv);

			while(true)
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

		public void ConnectUserToServer(User usr)
        {
			if(!userList.Contains(usr))
            {
				userList.Add(usr);
            }
			else
            {
				throw new Exception();
            }
        }

		public void AddChat(Chat chat)
        {
			if(!chatList.Contains(chat))
            {
				chatList.Add(chat);
            }
			else
            {
				throw new Exception();
            }
        }

		public void ReceiveMessage(Message msg)
        {

        }

		private void ConnectUserToChat(User usr, Chat cht)
		{

		}

		public static int IdGlobal { get => idGlobal++; }
		public static int IdGlobalCheckout { get => idGlobal; }
	}
}
