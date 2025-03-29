using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorAppCRUDScaffold;

namespace BlazorAppCRUDScaffold.Data
{
    public class BlazorAppCRUDScaffoldContext : DbContext
    {
        public BlazorAppCRUDScaffoldContext (DbContextOptions<BlazorAppCRUDScaffoldContext> options)
            : base(options)
        {
          this.Database.EnsureCreated();
        }

        public DbSet<BlazorAppCRUDScaffold.Student> Student { get; set; } = default!;
    }
}
