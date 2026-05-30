# Migration, update database, seed data va test API

## 1. Kiem tra truoc khi migration

Truoc khi chay migration, can dam bao:

- Da cai `Microsoft.EntityFrameworkCore.SqlServer`.
- Da cai `Microsoft.EntityFrameworkCore.Design`.
- Da co `AppDbContext1234De1`.
- Da cau hinh connection string trong `appsettings.json`.
- Da dang ky DbContext trong `Program.cs`.
- Project build duoc.

Lenh:

```powershell
dotnet build
```

## 2. Tao migration

Chay:

```powershell
dotnet ef migrations add InitialCreate
```

Sau lenh nay se co folder:

```text
Migrations/
```

Trong migration can kiem tra co:

- Bang `Enterprises`.
- Bang `Products`.
- Bang `EnterpriseProducts`.
- Khoa chinh `Id` tu tang cho `Enterprises`, `Products`.
- Khoa chinh ghep cho `EnterpriseProducts`.
- Khoa ngoai tu `EnterpriseProducts` den `Enterprises`.
- Khoa ngoai tu `EnterpriseProducts` den `Products`.
- Unique index cho ten doanh nghiep, ma so thue.
- Unique index cho ten san pham, ma san pham.

## 3. Update database

Chay:

```powershell
dotnet ef database update
```

Neu thanh cong, SQL Server se co database, vi du:

```text
EnterpriseProduct1234De1Db
```

## 4. Kiem tra database

Mo SQL Server Management Studio hoac Azure Data Studio, kiem tra:

```sql
SELECT * FROM Enterprises;
SELECT * FROM Products;
SELECT * FROM EnterpriseProducts;
```

Du lieu mau tu `HasData` nen co:

- 2 doanh nghiep.
- 3 san pham.
- 4 dong quan he doanh nghiep - san pham.

## 5. Chay project

```powershell
dotnet run
```

Mo Swagger theo URL hien tren terminal, thuong la:

```text
https://localhost:<port>/swagger
```

Neu loi HTTPS certificate, co the dung URL HTTP neu project hien ca hai cong.

## 6. Test API them doanh nghiep

Request:

```http
POST /api/enterprises
Content-Type: application/json
```

Body:

```json
{
  "name": "Cong ty Test",
  "taxCode": "MST999",
  "address": "Da Nang"
}
```

Ket qua dung:

```json
{
  "message": "Them doanh nghiep thanh cong",
  "data": {
    "id": 3,
    "name": "Cong ty Test",
    "taxCode": "MST999",
    "address": "Da Nang"
  }
}
```

Test trim string:

```json
{
  "name": "  Cong ty Trim  ",
  "taxCode": "  MST998  ",
  "address": "  Can Tho  "
}
```

Ket qua tra ve/luu database nen khong con dau cach dau cuoi.

## 7. Test loi trung ten doanh nghiep

Gui lai body co `name` da ton tai:

```json
{
  "name": "Cong ty ABC",
  "taxCode": "MST100",
  "address": "Ha Noi"
}
```

Ket qua dung:

```json
{
  "message": "Ten doanh nghiep da ton tai"
}
```

## 8. Test loi trung ma so thue

```json
{
  "name": "Cong ty Moi",
  "taxCode": "MST001",
  "address": "Ha Noi"
}
```

Ket qua dung:

```json
{
  "message": "Ma so thue da ton tai"
}
```

## 9. Test API sua doanh nghiep

Request:

```http
PUT /api/enterprises/1
Content-Type: application/json
```

Body:

```json
{
  "name": "Cong ty ABC Updated",
  "taxCode": "MST001",
  "address": "Ha Noi Updated"
}
```

Ket qua dung:

- Sua thanh cong.
- Khong bao trung ma so thue neu ma so thue do dang thuoc chinh doanh nghiep id 1.

## 10. Test API xoa doanh nghiep

Request:

```http
DELETE /api/enterprises/2
```

Ket qua dung:

```json
{
  "message": "Xoa doanh nghiep thanh cong"
}
```

Neu xoa id khong ton tai:

```json
{
  "message": "Khong tim thay doanh nghiep"
}
```

## 11. Test phan trang

Request:

```http
GET /api/enterprises?PageSize=10&PageIndex=1
```

Ket qua dung:

```json
{
  "totalItems": 2,
  "pageSize": 10,
  "pageIndex": 1,
  "items": []
}
```

`items` se co du lieu tuy database hien tai.

Test page 2:

```http
GET /api/enterprises?PageSize=1&PageIndex=2
```

Ket qua dung:

- `pageSize = 1`
- `pageIndex = 2`
- `items` co toi da 1 dong

## 12. Test tim kiem Keyword

Theo ten:

```http
GET /api/enterprises?PageSize=10&PageIndex=1&Keyword=ABC
```

Theo ma so thue:

```http
GET /api/enterprises?PageSize=10&PageIndex=1&Keyword=MST001
```

Ket qua dung:

- Chi tra cac doanh nghiep co ten hoac ma so thue chua keyword.

## 13. Test san pham nhap nhieu nhat

Request:

```http
GET /api/enterprises/1/top-products
```

Voi seed data:

```text
EnterpriseId = 1
Product 1 Quantity = 20
Product 2 Quantity = 50
Product 3 Quantity = 50
```

Ket qua dung:

```json
[
  {
    "name": "Ban phim co",
    "code": "SP002"
  },
  {
    "name": "Chuot khong day",
    "code": "SP003"
  }
]
```

Neu doanh nghiep ton tai nhung chua co san pham:

```json
[]
```

Neu doanh nghiep khong ton tai:

```json
{
  "message": "Khong tim thay doanh nghiep"
}
```
