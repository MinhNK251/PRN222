using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesScraefflordSample;

namespace RazorPagesScraefflordSample.Data
{
    public class RazorPagesScraefflordSampleContext : DbContext
    {
        public RazorPagesScraefflordSampleContext (DbContextOptions<RazorPagesScraefflordSampleContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<RazorPagesScraefflordSample.Student> Student { get; set; } = default!;
    }
}
