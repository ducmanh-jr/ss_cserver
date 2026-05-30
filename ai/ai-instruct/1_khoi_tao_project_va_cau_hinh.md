# Khoi tao project va cau hinh

## 1. Tao project ASP.NET Core Web API

Dung ten project theo de: `<HoVaTen><MSSV>`.

Vi du:

```powershell
dotnet new webapi -n NguyenVanA1234
cd NguyenVanA1234
```

File/folder duoc sinh ra tu template:

- `Program.cs`: cau hinh DI, middleware, Swagger, controller.
- `appsettings.json`: cau hinh connection string.
- `Properties/launchSettings.json`: cau hinh profile chay local.

Khong nen viet entity, service, DTO truc tiep trong `Program.cs`.

## 2. Cai package EF Core

Can SQL Server, migration va design-time tools:

```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

Neu may chua co `dotnet-ef`:

```powershell
dotnet tool install --global dotnet-ef
```

Kiem tra:

```powershell
dotnet ef --version
```

## 3. Tao folder bat buoc

Tao dung cau truc:

```powershell
mkdir Constants
mkdir Controllers
mkdir DbContexts
mkdir Dtos
mkdir Dtos\Enterprises
mkdir Dtos\Products
mkdir Dtos\EnterpriseProducts
mkdir Dtos\Common
mkdir Entities
mkdir Exceptions
mkdir Services
mkdir Services\Implements
mkdir Services\Interfaces
mkdir Utils
```

`Migrations` se duoc tao sau khi chay lenh migration.

## 4. Cau hinh `appsettings.json`

File nay dung de:

- Luu connection string.
- Luu cau hinh ung dung.

File nay khong nen chua:

- Code C#.
- Query SQL.
- Logic nghiep vu.

Vi du:

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

Neu dung SQL Server Express:

```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=EnterpriseProduct1234De1Db;Trusted_Connection=True;TrustServerCertificate=True"
```

Neu dung LocalDB:

```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EnterpriseProduct1234De1Db;Trusted_Connection=True;TrustServerCertificate=True"
```

## 5. Cau hinh `Program.cs`

File nay dung de:

- Dang ky controller.
- Dang ky Swagger.
- Dang ky DbContext.
- Dang ky service DI.
- Cau hinh middleware.

File nay khong nen chua:

- Logic them/sua/xoa doanh nghiep.
- LINQ truy van nghiep vu.
- Code seed du lieu dai va roi.

Khung `Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using NguyenVanA1234.DbContexts;
using NguyenVanA1234.Services.Implements;
using NguyenVanA1234.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext1234De1>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

Lien he:

- `AppDbContext1234De1` nam trong `DbContexts`.
- `IEnterpriseService1234De1` nam trong `Services/Interfaces`.
- `EnterpriseService1234De1` nam trong `Services/Implements`.

## 6. Lenh kiem tra sau khi cau hinh

```powershell
dotnet restore
dotnet build
```

Neu build loi vi chua co class DbContext/service, hay viet cac file o cac buoc tiep theo roi build lai.
