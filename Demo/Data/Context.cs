using Demo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
