using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;

namespace RepositoryLayer
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
    } 
}
