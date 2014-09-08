using System;
using System.Data.Entity;
using System.Data.SQLite;

namespace GuestBook.Datalayer
{
    public class SqLiteContext : DbContext
    {
        public SqLiteContext(string connectionString)
            : base(new SQLiteConnection { ConnectionString = connectionString }, true)
        {
        }

        public void CreateDatabase()
        {

            var command = String.Format("{0}; {1}; {2}",
                "CREATE TABLE Users    (Id INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , Name TEXT)",
                "CREATE TABLE Messages (Id INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , Text TEXT, Date TEXT)",
                "ALTER TABLE Messages ADD COLUMN User_Id INTEGER REFERENCES Users(Id);");

            Database.ExecuteSqlCommand(command);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
