namespace Chat.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string api = "message";
            ConnectionManager connectionManager = new ConnectionManager(api);
            connectionManager.StartConnection().GetAwaiter().GetResult();
        }
    }
}
