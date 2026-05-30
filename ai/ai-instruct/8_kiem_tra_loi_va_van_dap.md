# 🩺 Kiểm tra lỗi và vấn đáp

## 1. ✅ Checklist trước khi nộp

- Project là ASP.NET Core Web API.
- Có Entity Framework Core SQL Server.
- Có Code First Migration.
- Có bảng `Enterprises`.
- Có bảng `Products`.
- Có bảng `EnterpriseProducts`.
- `EnterpriseProducts` có `Quantity`.
- Khóa ngoại được tạo đúng trong migration.
- Primary key của doanh nghiệp và sản phẩm là `int` tự tăng.
- Thêm/sửa doanh nghiệp check trùng tên.
- Thêm/sửa doanh nghiệp check trùng mã số thuế.
- Danh sách doanh nghiệp có `PageSize`, `PageIndex`.
- Danh sách doanh nghiệp lọc gần đúng bằng `Keyword`.
- API top products nhận `enterpriseId`.
- API top products trả `Name`, `Code`.
- DTO có DataAnnotations.
- String DTO được trim.
- Controller trả `IActionResult`.
- Service được đăng ký DI.
- Có `UserFriendlyException`.
- Có dữ liệu mẫu để test.

---

## 2. 🔌 Lỗi connection string

**Dấu hiệu:**

- `A network-related or instance-specific error occurred`.
- `Cannot open database`.
- `Login failed`.

**Cách sửa:**

- Kiểm tra SQL Server đang chạy.
- Nếu dùng SQL Express, thử:

```json
"Server=.\\SQLEXPRESS;Database=EnterpriseProduct1234De1Db;Trusted_Connection=True;TrustServerCertificate=True"
```

- Nếu dùng LocalDB, thử:

```json
"Server=(localdb)\\MSSQLLocalDB;Database=EnterpriseProduct1234De1Db;Trusted_Connection=True;TrustServerCertificate=True"
```

- Kiểm tra tên connection string trong `Program.cs` phải là `"DefaultConnection"`.

## 3. 💾 Lỗi chưa update database

**Dấu hiệu:**

- API báo không có bảng.
- SQL Server chưa có database.
- Migration đã có nhưng database rỗng.

**Cách sửa:**

```powershell
dotnet ef database update
```

Nếu migration chưa tạo:

```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 4. 🔴 Lỗi trùng tên/mã số thuế

**Dấu hiệu:**

- Thêm doanh nghiệp trùng nhưng vẫn thành công.
- Sửa doanh nghiệp thành tên đã có vẫn thành công.

**Cách sửa:**

- Trong service, thêm check:

```csharp
await _dbContext.Enterprises.AnyAsync(e => e.Name == name)
await _dbContext.Enterprises.AnyAsync(e => e.TaxCode == taxCode)
```

- Khi sửa, nhớ bỏ qua id hiện tại:

```csharp
e.Id != currentId.Value
```

- Trong DbContext, thêm unique index:

```csharp
entity.HasIndex(e => e.Name).IsUnique();
entity.HasIndex(e => e.TaxCode).IsUnique();
```

## 5. 📄 Lỗi PageIndex/PageSize sai

**Dấu hiệu:**

- Trang 1 không có dữ liệu dù database có.
- `Skip` tính sai.
- `PageIndex = 0` không báo lỗi.

**Cách sửa:**

- Validate DTO:

```csharp
[Range(1, 100)]
public int PageSize { get; set; } = 10;

[Range(1, int.MaxValue)]
public int PageIndex { get; set; } = 1;
```

- Service dùng:

```csharp
Skip((input.PageIndex - 1) * input.PageSize)
```

## 6. 🔗 Lỗi khóa ngoại

**Dấu hiệu:**

- Thêm `EnterpriseProduct` lỗi vì `EnterpriseId` hoặc `ProductId` không tồn tại.
- Migration không có foreign key.

**Cách sửa:**

- Kiểm tra entity trung gian có navigation:

```csharp
public Enterprise1234De1 Enterprise { get; set; } = null!;
public Product1234De1 Product { get; set; } = null!;
```

- Kiểm tra DbContext có:

```csharp
HasOne(ep => ep.Enterprise).WithMany(e => e.EnterpriseProducts)
HasOne(ep => ep.Product).WithMany(p => p.EnterpriseProducts)
```

## 7. 💉 Lỗi DI chưa đăng ký service

**Dấu hiệu:**

```text
Unable to resolve service for type IEnterpriseService1234De1
```

**Cách sửa trong `Program.cs`:**

```csharp
builder.Services.AddScoped<IEnterpriseService1234De1, EnterpriseService1234De1>();
```

Và thêm using:

```csharp
using NguyenVanA1234.Services.Implements;
using NguyenVanA1234.Services.Interfaces;
```

## 8. 🏗️ Lỗi migration không tạo quan hệ n-n đúng

**Dấu hiệu:**

- EF tạo bảng lạ, không có `EnterpriseProducts`.
- Bảng trung gian không có `Quantity`.
- Khóa ngoại thiếu.

**Cách sửa:**

- Không dùng skip navigation tự động cho bài này.
- Tạo entity trung gian riêng `EnterpriseProduct1234De1`.
- Khai báo `DbSet<EnterpriseProduct1234De1>`.
- Cấu hình composite key và foreign key trong `OnModelCreating`.

---

## 9. 💬 Vấn đáp mẫu

> [!IMPORTANT]
> Đây là các câu hỏi thường gặp khi bảo vệ bài thi.

### Vì sao dùng DTO?

DTO giúp tách dữ liệu API khỏi entity database. DTO cho phép validate input, trim string, và chỉ nhận/trả các field cần thiết.

### Vì sao không trả entity trực tiếp?

Entity có thể chứa navigation gây lặp vô hạn, lộ cấu trúc database, hoặc trả thừa dữ liệu. Response DTO giúp API gọn và an toàn hơn.

### Vì sao dùng service?

Service là nơi đặt logic nghiệp vụ như check trùng, phân trang, tìm kiếm và lấy sản phẩm nhập nhiều nhất. Nhờ vậy controller gọn và dễ kiểm soát.

### Vì sao controller chỉ gọi service?

Controller chỉ nên phụ trách HTTP request/response. Nếu controller chứa logic database, code sẽ rối, khó test và khó sửa.

### Vì sao dùng `UserFriendlyException`?

`UserFriendlyException` dùng cho lỗi nghiệp vụ mà người dùng có thể hiểu, ví dụ trùng tên doanh nghiệp hoặc không tìm thấy doanh nghiệp. API sẽ trả message rõ ràng thay vì lỗi hệ thống.

### Vì sao dùng LINQ?

LINQ giúp truy vấn database bằng C# rõ ràng, có thể kết hợp `Where`, `OrderBy`, `Skip`, `Take`, `Select`. EF Core sẽ dịch LINQ thành SQL.

### Vì sao cần bảng trung gian trong quan hệ n-n?

Vì một doanh nghiệp có nhiều sản phẩm và một sản phẩm có nhiều doanh nghiệp. Bảng trung gian lưu từng cặp doanh nghiệp - sản phẩm.

### Vì sao `Quantity` nằm ở bảng trung gian?

`Quantity` là số lượng của một sản phẩm tại một doanh nghiệp cụ thể. Cùng một sản phẩm có thể có số lượng khác nhau ở các doanh nghiệp khác nhau, nên nó không thuộc riêng `Product`.

### Vì sao phải check trùng tên/mã số thuế?

Đề bài yêu cầu tên doanh nghiệp và mã số thuế không được trùng. Check trong service giúp trả lỗi thân thiện, unique index trong database giúp bảo vệ dữ liệu ở tầng cuối.

### Vì sao phân trang cần `PageSize` và `PageIndex`?

`PageSize` quy định mỗi trang lấy bao nhiêu dòng. `PageIndex` quy định lấy trang nào. Nếu không phân trang, API có thể trả quá nhiều dữ liệu và chậm.
