# 📚 Đọc đề và lộ trình làm bài

> [!IMPORTANT]
> Đây là bước quan trọng nhất trước khi code. Bạn cần hiểu rõ yêu cầu và cấu trúc dữ liệu để tránh đi sai hướng!

## 1. 🔍 Phân tích đề

Đề yêu cầu xây dựng Web API cho bài toán:

- 🏢 Một doanh nghiệp có thể nhập **nhiều sản phẩm**.
- 📦 Một sản phẩm có thể được nhập bởi **nhiều doanh nghiệp**.
- 🔢 Mỗi cặp doanh nghiệp - sản phẩm cần lưu thêm **số lượng nhập**.

> [!NOTE]
> Đây là quan hệ **nhiều-nhiều (many-to-many)** có dữ liệu phụ, nên bắt buộc tạo bảng trung gian riêng.

---

## 2. 🏗️ Xác định entity

Cần **3 entity chính**:

### 🏢 `Enterprise1234De1`

Dùng để lưu thông tin doanh nghiệp.

**✅ Cần có:**
- `Id`
- `Name`
- `TaxCode`
- `Address`
- Navigation đến danh sách `EnterpriseProducts`

**❌ Không nên chứa:**
- Logic thêm/sửa/xóa.
- Logic check trùng.
- Logic phân trang.

**🔗 Liên hệ với:**
- `EnterpriseProduct1234De1`
- `AppDbContext1234De1`
- DTO trong `Dtos/Enterprises`

### 📦 `Product1234De1`

Dùng để lưu thông tin sản phẩm.

**✅ Cần có:**
- `Id`
- `Name`
- `Code`
- `ImportDate`
- Navigation đến danh sách `EnterpriseProducts`

**❌ Không nên chứa:**
- Số lượng nhập của từng doanh nghiệp.
- Logic tìm sản phẩm nhập nhiều nhất.

**🔗 Liên hệ với:**
- `EnterpriseProduct1234De1`
- `AppDbContext1234De1`
- DTO trong `Dtos/Products`

### 🔗 `EnterpriseProduct1234De1`

Dùng để lưu quan hệ giữa doanh nghiệp và sản phẩm.

**✅ Cần có:**
- `EnterpriseId`
- `ProductId`
- `Quantity`
- Navigation `Enterprise`
- Navigation `Product`

**❌ Không nên chứa:**
- Tên doanh nghiệp.
- Tên sản phẩm.
- Mã sản phẩm.

> [!TIP]
> **Lý do:** Các thông tin đó đã nằm trong bảng gốc, bảng trung gian chỉ lưu **khóa ngoại** và **thông tin phụ** của mối quan hệ.

---

## 3. ❓ Vì sao cần bảng trung gian?

Nếu chỉ có `Enterprise` và `Product`, ta **không biết**:

- Doanh nghiệp nào nhập sản phẩm nào.
- Mỗi doanh nghiệp nhập bao nhiêu sản phẩm.
- Sản phẩm nào là sản phẩm nhập nhiều nhất của riêng một doanh nghiệp.

Bảng trung gian giải quyết việc này bằng cách lưu từng cặp:

| EnterpriseId | ProductId | Quantity |
| :--- | :--- | :--- |
| 1 | 2 | 100 |
| 1 | 3 | 250 |
| 2 | 2 | 80 |

> [!WARNING]
> `Quantity` không thể đặt trong `Product`, vì cùng một sản phẩm có thể có số lượng khác nhau ở từng doanh nghiệp.

---

## 4. 🌐 Xác định API cần làm

Cần ít nhất các API sau:

```http
POST   /api/enterprises
PUT    /api/enterprises/{id}
DELETE /api/enterprises/{id}
GET    /api/enterprises?PageSize=10&PageIndex=1&Keyword=abc
GET    /api/enterprises/{enterpriseId}/top-products
```

**Trong đó:**
- Thêm/sửa doanh nghiệp phải **check trùng** `Name` và `TaxCode`.
- Danh sách doanh nghiệp phải có `PageSize`, `PageIndex`.
- `Keyword` lọc **gần đúng** theo `Name` hoặc `TaxCode`.
- Top products trả ra danh sách sản phẩm có `Name` và `Code`.

---

## 5. ⏱️ Lộ trình làm bài 120 phút

> [!TIP]
> Hãy tuân thủ nghiêm ngặt mốc thời gian này để đảm bảo hoàn thành bài thi!

### 🟢 0 - 10 phút: Tạo project và folder
- Tạo project Web API.
- Cài package EF Core SQL Server.
- Tạo folder đúng yêu cầu.
- Cấu hình connection string.

### 🟡 10 - 30 phút: Viết entity và DbContext
- Tạo 3 entity.
- Tạo DbContext.
- Cấu hình primary key, foreign key, unique index.
- Đăng ký DbContext trong `Program.cs`.

### 🟠 30 - 50 phút: Viết DTO, exception, constants
- Tạo create/update/delete/filter DTO.
- Thêm validation annotation.
- Trim string trong setter.
- Tạo `UserFriendlyException`.
- Tạo message constants.

### 🔴 50 - 80 phút: Viết service
- Interface service.
- Implement service.
- Logic thêm/sửa/xóa.
- Logic phân trang và keyword.
- Logic top products.

### 🟣 80 - 95 phút: Viết controller
- Route rõ ràng.
- Controller trả `IActionResult`.
- Gọi service.
- Xử lý `UserFriendlyException`.

### 🔵 95 - 110 phút: Migration và seed data
- Chạy migration.
- Update database.
- Thêm dữ liệu mẫu.
- Kiểm tra bảng và khóa ngoại.

### 🏁 110 - 120 phút: Test và sửa lỗi
- Test trên Swagger/Postman.
- Check lỗi trùng tên/mã số thuế.
- Check phân trang, tìm kiếm.
- Check top products.
- Chạy `dotnet build`.
