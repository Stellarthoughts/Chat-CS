using System;

namespace Server.ChatPM
{
	public class Message
	{
		private string content;
		private ChatEntity origin;
		private DateTime time;
		private Chat destination;

		public Message()
		{
			Time = DateTime.Now;
		}

		public string Content { get => content; set => content = value; }
		public ChatEntity Origin { get => origin; set => origin = value; }
		public DateTime Time { get => time; set => time = value; }
		public Chat Destionation { get => destination; set => destination = value; }
	}
}
