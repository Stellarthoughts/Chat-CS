namespace Server.ChatPM
{
	public class User : ChatEntity
	{	
		private UserInterface ui;

        public User()
		{
			ui = new UserInterface(this);
		}

		public override void ReceiveMessage(Message msg)
		{
			ui.ShowMessage(msg);
		}

        public override void SendMessage(Message msg)
        {
			connectedTo.ReceiveMessage(msg);
        }

        public UserInterface UI { get => ui; set => ui = value; }
	}
}
