# Kiem tra loi va van dap

## 1. Checklist truoc khi nop

- Project la ASP.NET Core Web API.
- Co Entity Framework Core SQL Server.
- Co Code First Migration.
- Co bang `Enterprises`.
- Co bang `Products`.
- Co bang `EnterpriseProducts`.
- `EnterpriseProducts` co `Quantity`.
- Khoa ngoai duoc tao dung trong migration.
- Primary key cua doanh nghiep va san pham la `int` tu tang.
- Them/sua doanh nghiep check trung ten.
- Them/sua doanh nghiep check trung ma so thue.
- Danh sach doanh nghiep co `PageSize`, `PageIndex`.
- Danh sach doanh nghiep loc gan dung bang `Keyword`.
- API top products nhan `enterpriseId`.
- API top products tra `Name`, `Code`.
- DTO co DataAnnotations.
- String DTO duoc trim.
- Controller tra `IActionResult`.
- Service duoc dang ky DI.
- Co `UserFriendlyException`.
- Co du lieu mau de test.

## 2. Loi connection string

Dau hieu:

- `A network-related or instance-specific error occurred`.
- `Cannot open database`.
- `Login failed`.

Cach sua:

- Kiem tra SQL Server dang chay.
- Neu dung SQL Express, thu:

```json
"Server=.\\SQLEXPRESS;Database=EnterpriseProduct1234De1Db;Trusted_Connection=True;TrustServerCertificate=True"
```

- Neu dung LocalDB, thu:

```json
"Server=(localdb)\\MSSQLLocalDB;Database=EnterpriseProduct1234De1Db;Trusted_Connection=True;TrustServerCertificate=True"
```

- Kiem tra ten connection string trong `Program.cs` phai la `"DefaultConnection"`.

## 3. Loi chua update database

Dau hieu:

- API bao khong co bang.
- SQL Server chua co database.
- Migration da co nhung database rong.

Cach sua:

```powershell
dotnet ef database update
```

Neu migration chua tao:

```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 4. Loi trung ten/ma so thue

Dau hieu:

- Them doanh nghiep trung nhung van thanh cong.
- Sua doanh nghiep thanh ten da co van thanh cong.

Cach sua:

- Trong service, them check:

```csharp
await _dbContext.Enterprises.AnyAsync(e => e.Name == name)
await _dbContext.Enterprises.AnyAsync(e => e.TaxCode == taxCode)
```

- Khi sua, nho bo qua id hien tai:

```csharp
e.Id != currentId.Value
```

- Trong DbContext, them unique index:

```csharp
entity.HasIndex(e => e.Name).IsUnique();
entity.HasIndex(e => e.TaxCode).IsUnique();
```

## 5. Loi PageIndex/PageSize sai

Dau hieu:

- Trang 1 khong co du lieu du database co.
- `Skip` tinh sai.
- `PageIndex = 0` khong bao loi.

Cach sua:

- Validate DTO:

```csharp
[Range(1, 100)]
public int PageSize { get; set; } = 10;

[Range(1, int.MaxValue)]
public int PageIndex { get; set; } = 1;
```

- Service dung:

```csharp
Skip((input.PageIndex - 1) * input.PageSize)
```

## 6. Loi khoa ngoai

Dau hieu:

- Them `EnterpriseProduct` loi vi `EnterpriseId` hoac `ProductId` khong ton tai.
- Migration khong co foreign key.

Cach sua:

- Kiem tra entity trung gian co navigation:

```csharp
public Enterprise1234De1 Enterprise { get; set; } = null!;
public Product1234De1 Product { get; set; } = null!;
```

- Kiem tra DbContext co:

```csharp
HasOne(ep => ep.Enterprise).WithMany(e => e.EnterpriseProducts)
HasOne(ep => ep.Product).WithMany(p => p.EnterpriseProducts)
```

## 7. Loi DI chua dang ky service

Dau hieu:

```text
Unable to resolve service for type IEnterpriseService1234De1
```

Cach sua trong `Program.cs`:

```csharp
builder.Services.AddScoped<IEnterpriseService1234De1, EnterpriseService1234De1>();
```

Va them using:

```csharp
using NguyenVanA1234.Services.Implements;
using NguyenVanA1234.Services.Interfaces;
```

## 8. Loi migration khong tao quan he n-n dung

Dau hieu:

- EF tao bang la, khong co `EnterpriseProducts`.
- Bang trung gian khong co `Quantity`.
- Khoa ngoai thieu.

Cach sua:

- Khong dung skip navigation tu dong cho bai nay.
- Tao entity trung gian rieng `EnterpriseProduct1234De1`.
- Khai bao `DbSet<EnterpriseProduct1234De1>`.
- Cau hinh composite key va foreign key trong `OnModelCreating`.

## 9. Van dap mau

### Vi sao dung DTO?

DTO giup tach du lieu API khoi entity database. DTO cho phep validate input, trim string, va chi nhan/tra cac field can thiet.

### Vi sao khong tra entity truc tiep?

Entity co the chua navigation gay lap vo han, lo cau truc database, hoac tra thua du lieu. Response DTO giup API gon va an toan hon.

### Vi sao dung service?

Service la noi dat logic nghiep vu nhu check trung, phan trang, tim kiem va lay san pham nhap nhieu nhat. Nho vay controller gon va de kiem soat.

### Vi sao controller chi goi service?

Controller chi nen phu trach HTTP request/response. Neu controller chua logic database, code se roi, kho test va kho sua.

### Vi sao dung `UserFriendlyException`?

`UserFriendlyException` dung cho loi nghiep vu ma nguoi dung co the hieu, vi du trung ten doanh nghiep hoac khong tim thay doanh nghiep. API se tra message ro rang thay vi loi he thong.

### Vi sao dung LINQ?

LINQ giup truy van database bang C# ro rang, co the ket hop `Where`, `OrderBy`, `Skip`, `Take`, `Select`. EF Core se dich LINQ thanh SQL.

### Vi sao can bang trung gian trong quan he n-n?

Vi mot doanh nghiep co nhieu san pham va mot san pham co nhieu doanh nghiep. Bang trung gian luu tung cap doanh nghiep - san pham.

### Vi sao `Quantity` nam o bang trung gian?

`Quantity` la so luong cua mot san pham tai mot doanh nghiep cu the. Cung mot san pham co the co so luong khac nhau o cac doanh nghiep khac nhau, nen no khong thuoc rieng `Product`.

### Vi sao phai check trung ten/ma so thue?

De bai yeu cau ten doanh nghiep va ma so thue khong duoc trung. Check trong service giup tra loi than thien, unique index trong database giup bao ve du lieu o tang cuoi.

### Vi sao phan trang can `PageSize` va `PageIndex`?

`PageSize` quy dinh moi trang lay bao nhieu dong. `PageIndex` quy dinh lay trang nao. Neu khong phan trang, API co the tra qua nhieu du lieu va cham.
