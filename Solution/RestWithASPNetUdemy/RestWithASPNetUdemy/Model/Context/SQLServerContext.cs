using Microsoft.EntityFrameworkCore;

namespace RestWithASPNetUdemy.Model.Context
{
    public class SQLServerContext: DbContext
    {

        public SQLServerContext() 
        { 
        }

        public SQLServerContext(DbContextOptions<SQLServerContext> options): base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
