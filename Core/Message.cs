using System;

namespace Core
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Origin { get; set; }
        public string Target { get; set; }
    }
}
