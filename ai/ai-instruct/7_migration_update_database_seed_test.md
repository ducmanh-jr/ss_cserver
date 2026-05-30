# 🚀 Migration, update database, seed data và test API

## 1. 🔍 Kiểm tra trước khi migration

Trước khi chạy migration, cần đảm bảo:

- Đã cài `Microsoft.EntityFrameworkCore.SqlServer`.
- Đã cài `Microsoft.EntityFrameworkCore.Design`.
- Đã có `AppDbContext1234De1`.
- Đã cấu hình connection string trong `appsettings.json`.
- Đã đăng ký DbContext trong `Program.cs`.
- Project build được.

Lệnh:

```powershell
dotnet build
```

## 2. 🏗️ Tạo migration

Chạy:

```powershell
dotnet ef migrations add InitialCreate
```

Sau lệnh này sẽ có folder:

```text
Migrations/
```

> [!NOTE]
> Trong migration cần kiểm tra có:
> - Bảng `Enterprises`.
> - Bảng `Products`.
> - Bảng `EnterpriseProducts`.
> - Khóa chính `Id` tự tăng cho `Enterprises`, `Products`.
> - Khóa chính ghép cho `EnterpriseProducts`.
> - Khóa ngoại từ `EnterpriseProducts` đến `Enterprises`.
> - Khóa ngoại từ `EnterpriseProducts` đến `Products`.
> - Unique index cho tên doanh nghiệp, mã số thuế.
> - Unique index cho tên sản phẩm, mã sản phẩm.

## 3. 💾 Update database

Chạy:

```powershell
dotnet ef database update
```

Nếu thành công, SQL Server sẽ có database, ví dụ:

```text
EnterpriseProduct1234De1Db
```

## 4. 🗃️ Kiểm tra database

Mở SQL Server Management Studio hoặc Azure Data Studio, kiểm tra:

```sql
SELECT * FROM Enterprises;
SELECT * FROM Products;
SELECT * FROM EnterpriseProducts;
```

Dữ liệu mẫu từ `HasData` nên có:

- 2 doanh nghiệp.
- 3 sản phẩm.
- 4 dòng quan hệ doanh nghiệp - sản phẩm.

## 5. ▶️ Chạy project

```powershell
dotnet run
```

Mở Swagger theo URL hiện trên terminal, thường là:

```text
https://localhost:<port>/swagger
```

> [!TIP]
> Nếu lỗi HTTPS certificate, có thể dùng URL HTTP nếu project hiện cả hai cổng.

---

## 6. 🧪 Test API thêm doanh nghiệp

Request:

```http
POST /api/enterprises
Content-Type: application/json
```

Body:

```json
{
  "name": "Công ty Test",
  "taxCode": "MST999",
  "address": "Đà Nẵng"
}
```

Kết quả đúng:

```json
{
  "message": "Thêm doanh nghiệp thành công",
  "data": {
    "id": 3,
    "name": "Công ty Test",
    "taxCode": "MST999",
    "address": "Đà Nẵng"
  }
}
```

Test trim string:

```json
{
  "name": "  Công ty Trim  ",
  "taxCode": "  MST998  ",
  "address": "  Cần Thơ  "
}
```

> [!IMPORTANT]
> Kết quả trả về/lưu database nên không còn dấu cách đầu cuối.

## 7. 🔴 Test lỗi trùng tên doanh nghiệp

Gửi lại body có `name` đã tồn tại:

```json
{
  "name": "Công ty ABC",
  "taxCode": "MST100",
  "address": "Hà Nội"
}
```

Kết quả đúng:

```json
{
  "message": "Tên doanh nghiệp đã tồn tại"
}
```

## 8. 🔴 Test lỗi trùng mã số thuế

```json
{
  "name": "Công ty Mới",
  "taxCode": "MST001",
  "address": "Hà Nội"
}
```

Kết quả đúng:

```json
{
  "message": "Mã số thuế đã tồn tại"
}
```

## 9. 🟢 Test API sửa doanh nghiệp

Request:

```http
PUT /api/enterprises/1
Content-Type: application/json
```

Body:

```json
{
  "name": "Công ty ABC Updated",
  "taxCode": "MST001",
  "address": "Hà Nội Updated"
}
```

Kết quả đúng:

- Sửa thành công.
- Không báo trùng mã số thuế nếu mã số thuế đó đang thuộc chính doanh nghiệp id 1.

## 10. 🗑️ Test API xóa doanh nghiệp

Request:

```http
DELETE /api/enterprises/2
```

Kết quả đúng:

```json
{
  "message": "Xóa doanh nghiệp thành công"
}
```

Nếu xóa id không tồn tại:

```json
{
  "message": "Không tìm thấy doanh nghiệp"
}
```

## 11. 📄 Test phân trang

Request:

```http
GET /api/enterprises?PageSize=10&PageIndex=1
```

Kết quả đúng:

```json
{
  "totalItems": 2,
  "pageSize": 10,
  "pageIndex": 1,
  "items": []
}
```

`items` sẽ có dữ liệu tùy database hiện tại.

Test page 2:

```http
GET /api/enterprises?PageSize=1&PageIndex=2
```

Kết quả đúng:

- `pageSize = 1`
- `pageIndex = 2`
- `items` có tối đa 1 dòng

## 12. 🔍 Test tìm kiếm Keyword

Theo tên:

```http
GET /api/enterprises?PageSize=10&PageIndex=1&Keyword=ABC
```

Theo mã số thuế:

```http
GET /api/enterprises?PageSize=10&PageIndex=1&Keyword=MST001
```

Kết quả đúng:

- Chỉ trả các doanh nghiệp có tên hoặc mã số thuế chứa keyword.

## 13. 🏆 Test sản phẩm nhập nhiều nhất

Request:

```http
GET /api/enterprises/1/top-products
```

Với seed data:

```text
EnterpriseId = 1
Product 1 Quantity = 20
Product 2 Quantity = 50
Product 3 Quantity = 50
```

Kết quả đúng:

```json
[
  {
    "name": "Bàn phím cơ",
    "code": "SP002"
  },
  {
    "name": "Chuột không dây",
    "code": "SP003"
  }
]
```

Nếu doanh nghiệp tồn tại nhưng chưa có sản phẩm:

```json
[]
```

Nếu doanh nghiệp không tồn tại:

```json
{
  "message": "Không tìm thấy doanh nghiệp"
}
```
