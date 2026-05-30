# 🏆 Quy trình làm bài sạch từ đầu đến cuối

## 1. 📋 Nguyên tắc làm bài

> [!IMPORTANT]
> Làm theo thứ tự sau để tránh lỗi:

1. Tạo project.
2. Cài package.
3. Tạo folder.
4. Viết entity.
5. Viết DbContext.
6. Cấu hình `appsettings.json`.
7. Cấu hình `Program.cs`.
8. Viết DTO.
9. Viết exception/constants/utils.
10. Viết service interface.
11. Viết service implement.
12. Viết controller.
13. Build.
14. Migration.
15. Update database.
16. Test API.

> [!TIP]
> Dùng build nhiều lần, mỗi lần sau khi xong một cụm file quan trọng.

---

## 2. 🗂️ Thứ tự tạo file

### 🟢 Bước 1: Entity

Tạo:

```text
Entities/Enterprise1234De1.cs
Entities/Product1234De1.cs
Entities/EnterpriseProduct1234De1.cs
```

**Mục tiêu:**
- Có đủ 3 bảng.
- Quan hệ nhiều-nhiều có bảng trung gian.
- `Quantity` nằm đúng trong bảng trung gian.

### 🟡 Bước 2: DbContext

Tạo:

```text
DbContexts/AppDbContext1234De1.cs
```

**Mục tiêu:**
- Khai báo `DbSet`.
- Cấu hình table.
- Cấu hình key.
- Cấu hình foreign key.
- Cấu hình unique index.
- Seed data.

### 🟠 Bước 3: DTO

Tạo:

```text
Dtos/Enterprises/CreateEnterpriseDto1234De1.cs
Dtos/Enterprises/UpdateEnterpriseDto1234De1.cs
Dtos/Enterprises/DeleteEnterpriseDto1234De1.cs
Dtos/Enterprises/FilterEnterpriseDto1234De1.cs
Dtos/Enterprises/EnterpriseDto1234De1.cs
Dtos/Products/TopProductDto1234De1.cs
Dtos/EnterpriseProducts/EnterpriseProductDto1234De1.cs
Dtos/Common/PagedResultDto1234De1.cs
```

**Mục tiêu:**
- Input có validate.
- String input được trim.
- Response không trả entity trực tiếp.

### 🔴 Bước 4: Exception, constants, utils

Tạo:

```text
Exceptions/UserFriendlyException.cs
Constants/ErrorMessages1234De1.cs
Constants/SuccessMessages1234De1.cs
Utils/StringUtils1234De1.cs
```

**Mục tiêu:**
- Lỗi nghiệp vụ rõ ràng.
- Message không viết lặp lại.
- Có file utils nhỏ nếu cần.

### 🟣 Bước 5: Service

Tạo:

```text
Services/Interfaces/IEnterpriseService1234De1.cs
Services/Implements/EnterpriseService1234De1.cs
```

**Mục tiêu:**
- Controller không viết logic database.
- Service check trùng tên/mã số thuế.
- Service phân trang/tìm kiếm.
- Service lấy top products.

### 🔵 Bước 6: Controller

Tạo:

```text
Controllers/EnterprisesController1234De1.cs
```

**Mục tiêu:**
- API dùng HTTP method.
- Trả `IActionResult`.
- Route rõ ràng.
- Bắt `UserFriendlyException`.

---

## 3. 💻 Thứ tự chạy lệnh

Tạo project:
```powershell
dotnet new webapi -n NguyenVanA1234
cd NguyenVanA1234
```

Cài package:
```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

Build:
```powershell
dotnet build
```

Migration:
```powershell
dotnet ef migrations add InitialCreate
```

Update database:
```powershell
dotnet ef database update
```

Chạy project:
```powershell
dotnet run
```

---

## 4. 🧪 Thứ tự test API

Test theo thứ tự này:

1. `GET /api/enterprises?PageSize=10&PageIndex=1`
2. `GET /api/enterprises?PageSize=10&PageIndex=1&Keyword=ABC`
3. `POST /api/enterprises`
4. `POST /api/enterprises` với tên trùng.
5. `POST /api/enterprises` với mã số thuế trùng.
6. `PUT /api/enterprises/{id}`
7. `PUT /api/enterprises/{id}` với tên của doanh nghiệp khác.
8. `GET /api/enterprises/{enterpriseId}/top-products`
9. `DELETE /api/enterprises/{id}`
10. `DELETE /api/enterprises/{id}` với id không tồn tại.

---

## 5. ✅ Checklist trước khi nộp bài

Mở migration và kiểm tra:
- Có `CreateTable("Enterprises")`.
- Có `CreateTable("Products")`.
- Có `CreateTable("EnterpriseProducts")`.
- `EnterpriseProducts` có `Quantity`.
- Có `ForeignKey` đến `Enterprises`.
- Có `ForeignKey` đến `Products`.
- Có unique index cho `Name`, `TaxCode`, `Code`.

Mở code và kiểm tra:
- Class đặt đúng `1234De1` theo MSSV/đề.
- Public property PascalCase.
- Local variable camelCase.
- Private field `_camelCase`.
- DTO có `[Required]`, `[StringLength]`, `[Range]`.
- DTO string có setter trim.
- Controller trả `Task<IActionResult>`.
- Service không trả `IActionResult`.
- Controller không dùng `_dbContext`.
- Service có `_dbContext`.
- `Program.cs` đăng ký DbContext và service.

Test API và chụp/ghi lại kết quả:
- Thêm doanh nghiệp thành công.
- Thêm doanh nghiệp trùng tên bị lỗi.
- Thêm doanh nghiệp trùng mã số thuế bị lỗi.
- Sửa doanh nghiệp thành công.
- Xóa doanh nghiệp thành công.
- Phân trang thành công.
- Tìm kiếm keyword thành công.
- Top products trả đúng sản phẩm có quantity lớn nhất.

---

## 6. ⏳ Nếu bị hết thời gian

> [!WARNING]
> Ưu tiên theo thứ tự:

1. Entity + DbContext + migration đúng.
2. API thêm/sửa/xóa doanh nghiệp.
3. Check trùng tên/mã số thuế.
4. Phân trang + keyword.
5. Top products.
6. Seed data.
7. Constants/utils làm đẹp code.

> [!NOTE]
> Không nên dành quá nhiều thời gian cho UI, auth, repository pattern, AutoMapper, middleware phức tạp, vì đề không yêu cầu.

---

## 7. 🎓 Câu trả lời ngắn khi giáo viên hỏi quy trình

Em tách project thành các tầng đơn giản:

- **Entity và DbContext** để mô tả database.
- **DTO** để validate input và trả output gọn.
- **Service** để xử lý nghiệp vụ.
- **Controller** để expose API và trả `IActionResult`.

Quan hệ doanh nghiệp - sản phẩm là nhiều-nhiều có dữ liệu phụ `Quantity`, nên em tạo entity trung gian `EnterpriseProduct`. Em dùng EF Core Code First để tạo migration và update SQL Server. Các lỗi nghiệp vụ như trùng tên, trùng mã số thuế, không tìm thấy doanh nghiệp được ném bằng `UserFriendlyException`.
