using Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Chat.DesktopClient.Repository
{
    public class Repository : DbContext, IRepository
    {
        public DbSet<Message> _dbMessages { get; set; }
        private List<Message> _messages;

        public Repository()
        {
            Database.EnsureCreated();
            _messages = _dbMessages.ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
        }

        public List<Message> GetMessages()
        {
            return _messages;
        }

        public void SaveMessage(Message message)
        {
            _messages.Add(message);
            _dbMessages.Add(message);
            SaveChanges();
        }
    }
}