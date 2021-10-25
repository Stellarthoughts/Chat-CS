using System;

namespace Server.ChatPM
{
	public class Message
	{
		private string content;
		private ISendReceive origin;
		private ISendReceive destination;
		private DateTime time;

		public Message()
		{
			Time = DateTime.Now;
		}

		public string Content { get => content; set => content = value; }
		public ISendReceive Origin { get => origin; set => origin = value; }
		public DateTime Time { get => time; set => time = value; }
		public ISendReceive Destination { get => destination; set => destination = value; }
	}
}
