using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesCRUDSampleWithSignalR;

namespace RazorPagesCRUDSampleWithSignalR.Data
{
    public class RazorPagesCRUDSampleWithSignalRContext : DbContext
    {
        public RazorPagesCRUDSampleWithSignalRContext (DbContextOptions<RazorPagesCRUDSampleWithSignalRContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<RazorPagesCRUDSampleWithSignalR.Student> Student { get; set; } = default!;
    }
}
