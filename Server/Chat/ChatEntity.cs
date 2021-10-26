

namespace Server.ChatPM
{
	public abstract class ChatEntity : ISendReceive
	{
		protected Chat connectedTo;

		public ChatEntity()
		{
		}

		public void CreateMessage(string msgString)
		{
			Message msg = new Message()
			{
				Content = msgString,
				Origin = this,
				Destination = connectedTo
			};
			SendMessage(msg);
		}

		public abstract void SendMessage(Message msg);

		public abstract void ReceiveMessage(Message msg);

		public Chat ConnectedTo { get => connectedTo; set => connectedTo = value; }
	}
}
