using Chat.Server.SocketsManager;
using Moq;
using NUnit.Framework;
using System.Net.WebSockets;

namespace Chat.Tests
{
    public class ConnectionManagerUnitTests
    {
        private Mock<WebSocket>? _socket = null;

        [SetUp]
        public void Setup()
        {
            _socket = new Mock<WebSocket>(); 
        }

        [Test]
        public void Test_Socket_Adds_Correctly()
        {
            ConnectionManager con = new ConnectionManager();
            con.AddSocket(_socket.Object);
            Assert.IsNotEmpty(con.GetAllConnections());
        }

        [Test]
        public void Test_ConnectionManager_Can_Get_ID()
        {
            ConnectionManager con = new ConnectionManager();
            con.AddSocket(_socket.Object);
            var id = con.GetId(_socket.Object);
            Assert.IsNotNull(id);
        }
    }
}