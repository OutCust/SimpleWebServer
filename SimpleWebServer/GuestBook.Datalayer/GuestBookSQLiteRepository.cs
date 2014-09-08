using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.Datalayer
{
    public class GuestBookSqLiteRepository : IGuestBookRepository
    {
        private readonly string _connectionString;

        public GuestBookSqLiteRepository(string connectionString)
        {
            _connectionString = connectionString;

            var context = new SqLiteContext(_connectionString);

            var user = new User { Name = "qweasd"};

            context.Users.Add(user);
            context.SaveChanges();
        }

        public void AddMessage(Message message)
        {
            
        }

        public IList<Message> GetMessages()
        {
            return new Message[]{};
        }
    }
}
