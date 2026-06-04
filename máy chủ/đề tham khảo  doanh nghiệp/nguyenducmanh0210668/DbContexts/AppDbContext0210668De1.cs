using Microsoft.EntityFrameworkCore;
using nguyenducmanh0210668.Entities;

namespace nguyenducmanh0210668.DbContexts
{
    public class AppDbContext0210668De1 : DbContext
    {
        public AppDbContext0210668De1(DbContextOptions<AppDbContext0210668De1> options) : base(options)
        {
        }

        public DbSet<Enterprise0210668De1> Enterprises { get; set; } = null!;
        public DbSet<Product0210668De1> Products { get; set; } = null!;
        public DbSet<EnterpriseProduct0210668De1> EnterpriseProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise0210668De1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.TaxCode).IsUnique();
            });

            modelBuilder.Entity<Product0210668De1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.Code).IsUnique();
            });

            modelBuilder.Entity<EnterpriseProduct0210668De1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.HasOne(ep => ep.Enterprise)
                      .WithMany(e => e.EnterpriseProducts)
                      .HasForeignKey(ep => ep.EnterpriseId);

                entity.HasOne(ep => ep.Product)
                      .WithMany(p => p.EnterpriseProducts)
                      .HasForeignKey(ep => ep.ProductId);
            });
        }
    }
}
