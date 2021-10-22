

namespace Server.ChatPM
{
	public abstract class ChatEntity
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
			};
			SendMessage(msg);
		}
		public void SendMessage(Message msg)
		{
			ConnectedTo.ReceiveMessage(msg);
		}

		public Chat ConnectedTo { get => connectedTo; set => connectedTo = value; }
    }
}
