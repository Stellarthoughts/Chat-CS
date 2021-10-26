namespace Server.ChatPM
{
	public interface ISendReceive
	{
		public void SendMessage(Message msg);
		public void ReceiveMessage(Message msg);

	}
}