using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using WebApi_Server.Models;

namespace WebApi_Server.Repositories
{
    public class PersonContext : DbContext
    {
        public PersonContext()
        {
        }

        public PersonContext([NotNull] DbContextOptions<PersonContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
