using DucManhJr1234.Entities;
using Microsoft.EntityFrameworkCore;

namespace DucManhJr1234.DbContexts;

public class AppDbContext1234De1 : DbContext
{
    public AppDbContext1234De1(DbContextOptions<AppDbContext1234De1> options) : base(options)
    {
    }

    public DbSet<Enterprise1234De1> Enterprises => Set<Enterprise1234De1>();

    public DbSet<Product1234De1> Products => Set<Product1234De1>();

    public DbSet<EnterpriseProduct1234De1> EnterpriseProducts => Set<EnterpriseProduct1234De1>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Enterprise1234De1>(entity =>
        {
            entity.ToTable("Enterprises");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
            entity.Property(e => e.TaxCode).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(500).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.TaxCode).IsUnique();
        });

        modelBuilder.Entity<Product1234De1>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.Name).HasMaxLength(255).IsRequired();
            entity.Property(p => p.Code).HasMaxLength(50).IsRequired();
            entity.Property(p => p.ImportDate).IsRequired();
            entity.HasIndex(p => p.Name).IsUnique();
            entity.HasIndex(p => p.Code).IsUnique();
        });

        modelBuilder.Entity<EnterpriseProduct1234De1>(entity =>
        {
            entity.ToTable("EnterpriseProducts");
            entity.HasKey(ep => new { ep.EnterpriseId, ep.ProductId });
            entity.Property(ep => ep.Quantity).IsRequired();

            entity.HasOne(ep => ep.Enterprise)
                .WithMany(e => e.EnterpriseProducts)
                .HasForeignKey(ep => ep.EnterpriseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ep => ep.Product)
                .WithMany(p => p.EnterpriseProducts)
                .HasForeignKey(ep => ep.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enterprise1234De1>().HasData(
            new Enterprise1234De1 { Id = 1, Name = "Cong ty ABC", TaxCode = "MST001", Address = "Ha Noi" },
            new Enterprise1234De1 { Id = 2, Name = "Cong ty XYZ", TaxCode = "MST002", Address = "TP HCM" },
            new Enterprise1234De1 { Id = 3, Name = "Cong ty Demo", TaxCode = "MST003", Address = "Da Nang" }
        );

        modelBuilder.Entity<Product1234De1>().HasData(
            new Product1234De1 { Id = 1, Name = "Laptop Dell", Code = "SP001", ImportDate = new DateTime(2026, 1, 10) },
            new Product1234De1 { Id = 2, Name = "Ban phim co", Code = "SP002", ImportDate = new DateTime(2026, 1, 11) },
            new Product1234De1 { Id = 3, Name = "Chuot khong day", Code = "SP003", ImportDate = new DateTime(2026, 1, 12) },
            new Product1234De1 { Id = 4, Name = "Man hinh 24 inch", Code = "SP004", ImportDate = new DateTime(2026, 1, 13) }
        );

        modelBuilder.Entity<EnterpriseProduct1234De1>().HasData(
            new EnterpriseProduct1234De1 { EnterpriseId = 1, ProductId = 1, Quantity = 20 },
            new EnterpriseProduct1234De1 { EnterpriseId = 1, ProductId = 2, Quantity = 50 },
            new EnterpriseProduct1234De1 { EnterpriseId = 1, ProductId = 3, Quantity = 50 },
            new EnterpriseProduct1234De1 { EnterpriseId = 2, ProductId = 1, Quantity = 15 },
            new EnterpriseProduct1234De1 { EnterpriseId = 2, ProductId = 4, Quantity = 70 }
        );
    }
}
