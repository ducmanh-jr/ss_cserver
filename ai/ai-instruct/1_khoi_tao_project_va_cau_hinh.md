# 🚀 Khởi tạo project và cấu hình

## 1. 🆕 Tạo project ASP.NET Core Web API

Dùng tên project theo đề: `<HoVaTen><MSSV>`.

Ví dụ:

```powershell
dotnet new webapi -n NguyenVanA1234
cd NguyenVanA1234
```

> [!NOTE]
> **File/folder được sinh ra từ template:**
> - `Program.cs`: cấu hình DI, middleware, Swagger, controller.
> - `appsettings.json`: cấu hình connection string.
> - `Properties/launchSettings.json`: cấu hình profile chạy local.

> [!WARNING]
> **KHÔNG NÊN** viết entity, service, DTO trực tiếp trong `Program.cs`.

---

## 2. 📦 Cài package EF Core

Cần SQL Server, migration và design-time tools:

```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

> [!TIP]
> Nếu máy chưa có `dotnet-ef`, hãy cài đặt:
> ```powershell
> dotnet tool install --global dotnet-ef
> ```
> Kiểm tra:
> ```powershell
> dotnet ef --version
> ```

---

## 3. 📁 Tạo folder bắt buộc

Tạo đúng cấu trúc bằng lệnh:

```powershell
# Bước 1: Tạo các thư mục chính
mkdir Constants, Controllers, DbContexts, Dtos, Entities, Exceptions, Services, Utils

# Bước 2: Tạo các thư mục con trong Dtos
mkdir Dtos\Enterprises, Dtos\Products, Dtos\EnterpriseProducts, Dtos\Common

# Bước 3: Tạo các thư mục con trong Services
mkdir Services\Implements, Services\Interfaces
```

> [!NOTE]
> Thư mục `Migrations` sẽ tự động được tạo sau khi bạn chạy lệnh migration.

---

## 4. ⚙️ Cấu hình `appsettings.json`

File này dùng để:
- Lưu **connection string**.
- Lưu cấu hình ứng dụng.

> [!WARNING]
> File này **không nên** chứa Code C#, Query SQL, Logic nghiệp vụ.

**Ví dụ:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EnterpriseProduct1234De1Db;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

> [!TIP]
> - Nếu dùng **SQL Server Express**:
>   `"Server=.\\SQLEXPRESS;Database=...;Trusted_Connection=True;TrustServerCertificate=True"`
> - Nếu dùng **LocalDB**:
>   `"Server=(localdb)\\MSSQLLocalDB;Database=...;Trusted_Connection=True;TrustServerCertificate=True"`

---

## 5. 🛠️ Cấu hình `Program.cs`

File này dùng để:
- Đăng ký controller.
- Đăng ký Swagger.
- Đăng ký DbContext.
- Đăng ký service DI.
- Cấu hình middleware.

> [!WARNING]
> File này **không nên** chứa logic thêm/sửa/xóa, LINQ truy vấn, hay code seed dữ liệu dài dòng.

**Khung `Program.cs` chuẩn:**

```csharp
using Microsoft.EntityFrameworkCore;
using NguyenVanA1234.DbContexts;
using NguyenVanA1234.Services.Implements;
using NguyenVanA1234.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký DbContext
builder.Services.AddDbContext<AppDbContext1234De1>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Dependency Injection
builder.Services.AddScoped<IEnterpriseService1234De1, EnterpriseService1234De1>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

## 6. ✅ Lệnh kiểm tra sau khi cấu hình

```powershell
dotnet restore
dotnet build
```

> [!IMPORTANT]
> Nếu build lỗi vì chưa có class DbContext/service, đừng lo lắng! Hãy viết các file ở các bước tiếp theo rồi build lại.
