using System;
using System.Collections.Generic;
using BOs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAOs;

public partial class Sp25lionDbContext : DbContext
{
    public Sp25lionDbContext()
    {
    }

    public Sp25lionDbContext(DbContextOptions<Sp25lionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LionAccount> LionAccounts { get; set; }

    public virtual DbSet<LionProfile> LionProfiles { get; set; }

    public virtual DbSet<LionType> LionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnectionStringDB");
        return connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LionAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("LionAccount");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<LionProfile>(entity =>
        {
            entity.ToTable("LionProfile");

            entity.Property(e => e.Characteristics).HasMaxLength(2000);
            entity.Property(e => e.LionName).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Warning).HasMaxLength(1500);

            entity.HasOne(d => d.LionType).WithMany(p => p.LionProfiles)
                .HasForeignKey(d => d.LionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LionProfile_LionType");
        });

        modelBuilder.Entity<LionType>(entity =>
        {
            entity.ToTable("LionType");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.LionTypeName).HasMaxLength(250);
            entity.Property(e => e.Origin).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
