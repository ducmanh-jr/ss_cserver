# 🗄️ Thiết kế database, entities và DbContext

> [!IMPORTANT]
> Code First EF Core dựa hoàn toàn vào các Entity và DbContext bạn định nghĩa ở đây để tạo bảng trong Database.

## 1. 🏢 Tạo entity `Enterprise1234De1`

**Đường dẫn:** `Entities/Enterprise1234De1.cs`

File này dùng để đại diện bảng doanh nghiệp trong database.

> [!NOTE]
> - EF Core dựa vào entity để tạo bảng.
> - Service dựa vào entity để thêm/sửa/xóa doanh nghiệp.
> - **Không nên chứa:** Validation DTO, Logic check trùng, Logic trả response API.

**Code:**

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

---

## 2. 📦 Tạo entity `Product1234De1`

**Đường dẫn:** `Entities/Product1234De1.cs`

File này dùng để đại diện bảng sản phẩm.

> [!WARNING]
> - Không chứa `Quantity`, vì số lượng phụ thuộc vào từng doanh nghiệp.
> - Không chứa logic nghiệp vụ.

**Code:**

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

---

## 3. 🔗 Tạo entity `EnterpriseProduct1234De1` (Bảng trung gian)

**Đường dẫn:** `Entities/EnterpriseProduct1234De1.cs`

File này dùng để đại diện bảng trung gian. Quan hệ doanh nghiệp - sản phẩm là nhiều-nhiều và có thêm thông tin `Quantity`.

> [!TIP]
> Bảng này **chỉ nên** lưu khóa ngoại và các trường phụ (như `Quantity`). Không lưu lại Tên doanh nghiệp hay Tên sản phẩm.

**Code:**

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

---

## 4. 🗃️ Tạo DbContext

**Đường dẫn:** `DbContexts/AppDbContext1234De1.cs`

File này cực kỳ quan trọng để:
- Khai báo `DbSet`.
- Cấu hình khóa chính, khóa ngoại, unique index.
- Seed dữ liệu mẫu `HasData`.

**Code:**

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

        // Cấu hình Enterprise
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

        // Cấu hình Product
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

        // Cấu hình EnterpriseProduct (Bảng trung gian)
        modelBuilder.Entity<EnterpriseProduct1234De1>(entity =>
        {
            entity.ToTable("EnterpriseProducts");
            
            // Khóa chính gộp (Composite Key)
            entity.HasKey(ep => new { ep.EnterpriseId, ep.ProductId });
            entity.Property(ep => ep.Quantity).IsRequired();

            // Khóa ngoại
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
            new Enterprise1234De1 { Id = 1, Name = "Công ty ABC", TaxCode = "MST001", Address = "Hà Nội" },
            new Enterprise1234De1 { Id = 2, Name = "Công ty XYZ", TaxCode = "MST002", Address = "TP HCM" }
        );

        modelBuilder.Entity<Product1234De1>().HasData(
            new Product1234De1 { Id = 1, Name = "Laptop Dell", Code = "SP001", ImportDate = new DateTime(2026, 1, 10) },
            new Product1234De1 { Id = 2, Name = "Bàn phím cơ", Code = "SP002", ImportDate = new DateTime(2026, 1, 11) },
            new Product1234De1 { Id = 3, Name = "Chuột không dây", Code = "SP003", ImportDate = new DateTime(2026, 1, 12) }
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

---

## 5. 💡 Giải thích cấu hình quan hệ

> [!TIP]
> - `entity.HasKey(ep => new { ep.EnterpriseId, ep.ProductId });`
>   Dùng composite key (khóa gộp) để một doanh nghiệp không bị trùng cùng một sản phẩm trong bảng trung gian.
> - `HasForeignKey(...)`
>   Chỉ định rõ ràng khóa ngoại để EF Core tạo bảng đúng chuẩn trong SQL.
> - `HasIndex(...).IsUnique()`
>   Giúp database **bảo vệ** chống trùng lặp dữ liệu, tăng cường an toàn dữ liệu!
