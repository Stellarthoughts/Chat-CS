namespace Server.ChatPM
{
	public class User : ChatEntity
	{	
		private UserInterface ui;
        private Server srv;

        public User(Server srv)
		{
			ui = new UserInterface(this);
			srv.ConnectUserToServer(this);
		}

        public UserInterface UI { get => ui; set => ui = value; }
		public Server Srv { get => srv; set => srv = value; }
	}
}
