# CHẶNG 1: NỀN MÓNG & DATABASE (THIẾT LẬP CSDL)

## 📖 PHẦN LÝ THUYẾT: MỤC TIÊU
- **Entities:** "Bản vẽ thiết kế" của cấu trúc dữ liệu.
- **DbContext:** "Người quản lý kho", dùng bản vẽ để kết nối với SQL Server.
- **Migration:** Đội thợ xây, đọc DbContext và sinh ra các bảng thật trong SQL Server.

---

## 🛠️ PHẦN THAO TÁC TAY

### THAO TÁC 1: TẠO FILE CHO ENTITIES
**🎯 Mục tiêu & Ý nghĩa:** 
- Các file Entities là nơi **định nghĩa cấu trúc dữ liệu** (như các bảng trong SQL). 
- Mỗi Class (ví dụ: `Product0210668`) sẽ trở thành một **bảng** trong cơ sở dữ liệu.
- Mỗi thuộc tính (ví dụ: `Name`, `Price`) sẽ trở thành một **cột** trong bảng đó. Code C# cần các file này để hiểu được dự án sẽ lưu trữ những thông tin gì.

1. Mở thư mục gốc của dự án (`nguyenducmanh0210668`).
2. Tạo một thư mục mới tên là `Entities` (nếu chưa có).
3. Click chuột phải vào thư mục `Entities`, chọn **New File** để tạo lần lượt 3 file: 
   - `Enterprise0210668.cs`
   - `Product0210668.cs`
   - `EnterpriseProduct0210668.cs`
4. Lần lượt mở các file này ra và copy/paste Code số 1, 2, 3 tương ứng ở bên dưới vào.

### THAO TÁC 2: TẠO FILE DBCONTEXT
**🎯 Mục tiêu & Ý nghĩa:** 
- `AppDbContext0210668.cs` đóng vai trò là **cầu nối (cổng giao tiếp)** giữa ứng dụng C# của bạn và hệ quản trị cơ sở dữ liệu SQL Server. 
- Nó tập hợp các Entities (bản vẽ) ở Thao tác 1 để báo cho Entity Framework biết cần phải tạo ra/quản lý những bảng nào trong SQL, đồng thời là nơi chịu trách nhiệm thực thi các lệnh Thêm/Sửa/Xóa/Lấy dữ liệu sau này.

1. Tại thư mục gốc dự án, tạo một thư mục mới tên là `DbContexts` (nếu chưa có).
2. Click chuột phải vào thư mục `DbContexts`, chọn **New File** và đặt tên file là `AppDbContext0210668.cs`.
3. Mở file vừa tạo lên và dán **Code số 4** ở bên dưới vào.

### THAO TÁC 3: ĐĂNG KÝ DBCONTEXT VÀO PROGRAM.CS
**🎯 Mục tiêu & Ý nghĩa:** 
- `Program.cs` là nơi khởi chạy toàn bộ ứng dụng. 
- Việc đăng ký DbContext ở đây giống như bạn nói với ứng dụng rằng: *"Này ứng dụng, hãy dùng cái cầu nối AppDbContext cùng với địa chỉ ConnectionString này để kết nối tới SQL Server nhé, để mỗi khi có ai cần truy xuất dữ liệu thì có sẵn kết nối để dùng."*

1. Mở file `Program.cs` ở thư mục gốc.
2. Tìm dòng trống bên dưới `builder.Services.AddSwaggerGen();`.
3. Dán **Code số 5** vào để kết nối ứng dụng với SQL Server.

### THAO TÁC 4: MỞ TERMINAL VÀ CHẠY LỆNH
1. Bấm **Terminal** -> **New Terminal**. Đảm bảo đang đứng ở thư mục gốc.
2. Gõ lệnh đóng gói: `dotnet ef migrations add InitialCreate` rồi nhấn Enter.
3. Gõ lệnh xây DB: `dotnet ef database update` rồi nhấn Enter.

---

## 💻 PHẦN CODE ĐỂ COPY

**Code số 1: (Enterprise0210668.cs)**
```csharp
using System.ComponentModel.DataAnnotations;

namespace nguyenducmanh0210668.Entities;

public class Enterprise0210668
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string TaxCode { get; set; } = null!;

    public string? Address { get; set; }

    public ICollection<EnterpriseProduct0210668> EnterpriseProducts { get; set; } = new List<EnterpriseProduct0210668>();
}
```

**Code số 2: (Product0210668.cs)**
```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nguyenducmanh0210668.Entities;

public class Product0210668
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public ICollection<EnterpriseProduct0210668> EnterpriseProducts { get; set; } = new List<EnterpriseProduct0210668>();
}
```

**Code số 3: (EnterpriseProduct0210668.cs)**
```csharp
namespace nguyenducmanh0210668.Entities;

public class EnterpriseProduct0210668
{
    public int EnterpriseId { get; set; }
    public Enterprise0210668 Enterprise { get; set; } = null!;

    public int ProductId { get; set; }
    public Product0210668 Product { get; set; } = null!;
}
```

**Code số 4 (AppDbContext0210668.cs)**
```csharp
using Microsoft.EntityFrameworkCore;
using nguyenducmanh0210668.Entities;

namespace nguyenducmanh0210668.DbContexts;
public class AppDbContext0210668 : DbContext
{
    public AppDbContext0210668(DbContextOptions<AppDbContext0210668> options) : base(options) {}

    public DbSet<Enterprise0210668> Enterprises => Set<Enterprise0210668>();
    public DbSet<Product0210668> Products => Set<Product0210668>();
    public DbSet<EnterpriseProduct0210668> EnterpriseProducts => Set<EnterpriseProduct0210668>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EnterpriseProduct0210668>().HasKey(ep => new { ep.EnterpriseId, ep.ProductId });
        modelBuilder.Entity<Enterprise0210668>().HasIndex(e => e.Name).IsUnique();
        modelBuilder.Entity<Enterprise0210668>().HasIndex(e => e.TaxCode).IsUnique();
        modelBuilder.Entity<Product0210668>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<Product0210668>().HasIndex(p => p.Code).IsUnique();
    }
}
```

**Code số 5: Bổ sung vào Program.cs (Dưới dòng AddSwaggerGen)**
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<nguyenducmanh0210668.DbContexts.AppDbContext0210668>(options =>
    options.UseSqlServer(connectionString));
```
