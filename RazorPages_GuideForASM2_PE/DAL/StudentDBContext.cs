using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesSample.ServiceLayers.DTOs;

namespace DAL
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
    }
}
