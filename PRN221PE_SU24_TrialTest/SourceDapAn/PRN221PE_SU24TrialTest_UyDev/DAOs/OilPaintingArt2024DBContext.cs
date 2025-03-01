using BOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAOs
{
    public partial class OilPaintingArt2024DBContext : DbContext
    {
        public OilPaintingArt2024DBContext()
        {
        }

        public OilPaintingArt2024DBContext(DbContextOptions<OilPaintingArt2024DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OilPaintingArt> OilPaintingArts { get; set; } = null!;
        public virtual DbSet<SupplierCompany> SupplierCompanies { get; set; } = null!;
        public virtual DbSet<SystemAccount> SystemAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DefaultConnectionString"];
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OilPaintingArt>(entity =>
            {
                entity.ToTable("OilPaintingArt");

                entity.Property(e => e.OilPaintingArtId).ValueGeneratedNever();

                entity.Property(e => e.ArtTitle).HasMaxLength(100);

                entity.Property(e => e.Artist).HasMaxLength(80);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.NotablFeatures).HasMaxLength(250);

                entity.Property(e => e.OilPaintingArtLocation).HasMaxLength(240);

                entity.Property(e => e.OilPaintingArtStyle).HasMaxLength(50);

                entity.Property(e => e.PriceOfOilPaintingArt).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SupplierId).HasMaxLength(30);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.OilPaintingArts)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__OilPainti__Suppl__3C69FB99");
            });

            modelBuilder.Entity<SupplierCompany>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK__Supplier__4BE666B433198C38");

                entity.ToTable("SupplierCompany");

                entity.Property(e => e.SupplierId).HasMaxLength(30);

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CompanyTypeDescription).HasMaxLength(250);
            });

            modelBuilder.Entity<SystemAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__SystemAc__349DA5868B865C9F");

                entity.ToTable("SystemAccount");

                entity.HasIndex(e => e.AccountEmail, "UQ__SystemAc__FC770D3337FD7FFF")
                    .IsUnique();

                entity.Property(e => e.AccountId)
                    .ValueGeneratedNever()
                    .HasColumnName("AccountID");

                entity.Property(e => e.AccountEmail).HasMaxLength(80);

                entity.Property(e => e.AccountFullName).HasMaxLength(80);

                entity.Property(e => e.AccountPassword).HasMaxLength(40);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
