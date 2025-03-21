using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppContext : DbContext //DbContext is a class provided by Entity Framework Core that represents a session with the database, allowing us to query and save data.
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) {
            //The constructor of the MyAppContext class takes an instance of DbContextOptions as a parameter and passes it to the base class constructor
        }

        public DbSet<Item> Items { get; set; } //The DbSet class represents a collection of entities in the context that can be queried and saved to the database.
    }
}
