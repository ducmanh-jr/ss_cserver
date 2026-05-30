# Thiet ke database, entities va DbContext

## 1. Tao entity `Enterprise1234De1`

Duong dan:

```text
Entities/Enterprise1234De1.cs
```

File nay dung de dai dien bang doanh nghiep trong database.

Vi sao can file nay:

- EF Core dua vao entity de tao bang.
- Service dua vao entity de them/sua/xoa doanh nghiep.

File nay khong nen chua:

- Validation DTO.
- Logic check trung.
- Logic tra response API.

Code:

```csharp
namespace NguyenVanA1234.Entities;

public class Enterprise1234De1
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TaxCode { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public ICollection<EnterpriseProduct1234De1> EnterpriseProducts { get; set; } = new List<EnterpriseProduct1234De1>();
}
```

## 2. Tao entity `Product1234De1`

Duong dan:

```text
Entities/Product1234De1.cs
```

File nay dung de dai dien bang san pham.

Vi sao can file nay:

- Luu ten san pham, ma san pham, ngay nhap.
- Ket noi voi doanh nghiep thong qua bang trung gian.

File nay khong nen chua:

- `Quantity`, vi so luong phu thuoc tung doanh nghiep.
- Logic top products.

Code:

```csharp
namespace NguyenVanA1234.Entities;

public class Product1234De1
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public DateTime ImportDate { get; set; }

    public ICollection<EnterpriseProduct1234De1> EnterpriseProducts { get; set; } = new List<EnterpriseProduct1234De1>();
}
```

## 3. Tao entity `EnterpriseProduct1234De1`

Duong dan:

```text
Entities/EnterpriseProduct1234De1.cs
```

File nay dung de dai dien bang trung gian.

Vi sao can file nay:

- Quan he doanh nghiep - san pham la nhieu-nhieu.
- Quan he nay co them thong tin `Quantity`.
- EF Core can entity trung gian de tao khoa ngoai dung.

File nay khong nen chua:

- Ten doanh nghiep.
- Dia chi doanh nghiep.
- Ten san pham.
- Ma san pham.

Code:

```csharp
namespace NguyenVanA1234.Entities;

public class EnterpriseProduct1234De1
{
    public int EnterpriseId { get; set; }

    public Enterprise1234De1 Enterprise { get; set; } = null!;

    public int ProductId { get; set; }

    public Product1234De1 Product { get; set; } = null!;

    public int Quantity { get; set; }
}
```

## 4. Tao DbContext

Duong dan:

```text
DbContexts/AppDbContext1234De1.cs
```

File nay dung de:

- Khai bao `DbSet`.
- Cau hinh khoa chinh.
- Cau hinh khoa ngoai.
- Cau hinh unique index.
- Seed du lieu mau neu muon lam bang `HasData`.

File nay khong nen chua:

- Logic API.
- Logic check trung dung cho request.
- Logic phan trang.

Code:

```csharp
using Microsoft.EntityFrameworkCore;
using NguyenVanA1234.Entities;

namespace NguyenVanA1234.DbContexts;

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
            new Enterprise1234De1 { Id = 2, Name = "Cong ty XYZ", TaxCode = "MST002", Address = "TP HCM" }
        );

        modelBuilder.Entity<Product1234De1>().HasData(
            new Product1234De1 { Id = 1, Name = "Laptop Dell", Code = "SP001", ImportDate = new DateTime(2026, 1, 10) },
            new Product1234De1 { Id = 2, Name = "Ban phim co", Code = "SP002", ImportDate = new DateTime(2026, 1, 11) },
            new Product1234De1 { Id = 3, Name = "Chuot khong day", Code = "SP003", ImportDate = new DateTime(2026, 1, 12) }
        );

        modelBuilder.Entity<EnterpriseProduct1234De1>().HasData(
            new EnterpriseProduct1234De1 { EnterpriseId = 1, ProductId = 1, Quantity = 20 },
            new EnterpriseProduct1234De1 { EnterpriseId = 1, ProductId = 2, Quantity = 50 },
            new EnterpriseProduct1234De1 { EnterpriseId = 1, ProductId = 3, Quantity = 50 },
            new EnterpriseProduct1234De1 { EnterpriseId = 2, ProductId = 1, Quantity = 15 }
        );
    }
}
```

## 5. Giai thich cau hinh quan he

```csharp
entity.HasKey(ep => new { ep.EnterpriseId, ep.ProductId });
```

Dung composite key de mot doanh nghiep khong bi trung cung mot san pham trong bang trung gian.

```csharp
HasForeignKey(ep => ep.EnterpriseId)
HasForeignKey(ep => ep.ProductId)
```

Dung de migration tao khoa ngoai that trong SQL Server.

```csharp
HasIndex(e => e.Name).IsUnique()
HasIndex(e => e.TaxCode).IsUnique()
```

Dung de database cung bao ve quy tac khong trung, ngoai viec service da check truoc.
