using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Entity;

namespace GuestBook.Datalayer
{
    public class GuestBookSqLiteRepository : IGuestBookRepository
    {
        private readonly string _connectionString;

        public GuestBookSqLiteRepository(string dbFilePath)
        {
            _connectionString = string.Format("Data Source={0};Version=3", dbFilePath);
            if (!File.Exists(dbFilePath))
            {
                var context = new SqLiteContext(_connectionString);
                context.CreateDatabase();
            }
        }

        public void AddMessage(Message message)
        {
            using (var context = new SqLiteContext(_connectionString))
            {
                var user = context.Users.FirstOrDefault(c => c.Name.Equals(message.User.Name));
                if (user != null)
                {
                    message.User = user;
                }
                else
                {
                    context.Users.Add(message.User);
                }
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        public IList<Message> GetMessages()
        {
            using (var context = new SqLiteContext(_connectionString))
            {
                var messages = context.Messages.Include(c => c.User) .ToArray();
                return messages;
            }
        }
    }
}
