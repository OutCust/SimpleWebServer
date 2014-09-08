using System.Data.Entity;
using System.Data.SQLite;

namespace GuestBook.Datalayer
{
    public class SqLiteContext : DbContext
    {
        public SqLiteContext(string connectionString)
            : base(new SQLiteConnection { ConnectionString = connectionString }, true)
        {
            Database.Initialize(true);


            //
            //Database.ExecuteSqlCommand(@"Create table Users (id int primary key, Name text )");
            Database.ExecuteSqlCommand("CREATE TABLE \"Users\" (\"Id\" INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , \"Name\" TEXT)");
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
