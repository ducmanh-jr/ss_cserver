# Doc de va lo trinh lam bai

## 1. Phan tich de

De yeu cau xay dung Web API cho bai toan:

- Mot doanh nghiep co the nhap nhieu san pham.
- Mot san pham co the duoc nhap boi nhieu doanh nghiep.
- Moi cap doanh nghiep - san pham can luu them so luong nhap.

Day la quan he nhieu-nhieu co du lieu phu, nen bat buoc tao bang trung gian rieng.

## 2. Xac dinh entity

Can 3 entity chinh:

### `Enterprise1234De1`

Dung de luu thong tin doanh nghiep.

Can co:

- `Id`
- `Name`
- `TaxCode`
- `Address`
- Navigation den danh sach `EnterpriseProducts`

Khong nen chua:

- Logic them/sua/xoa.
- Logic check trung.
- Logic phan trang.

Lien he voi:

- `EnterpriseProduct1234De1`
- `AppDbContext1234De1`
- DTO trong `Dtos/Enterprises`

### `Product1234De1`

Dung de luu thong tin san pham.

Can co:

- `Id`
- `Name`
- `Code`
- `ImportDate`
- Navigation den danh sach `EnterpriseProducts`

Khong nen chua:

- So luong nhap cua tung doanh nghiep.
- Logic tim san pham nhap nhieu nhat.

Lien he voi:

- `EnterpriseProduct1234De1`
- `AppDbContext1234De1`
- DTO trong `Dtos/Products`

### `EnterpriseProduct1234De1`

Dung de luu quan he giua doanh nghiep va san pham.

Can co:

- `EnterpriseId`
- `ProductId`
- `Quantity`
- Navigation `Enterprise`
- Navigation `Product`

Khong nen chua:

- Ten doanh nghiep.
- Ten san pham.
- Ma san pham.

Ly do: cac thong tin do da nam trong bang goc, bang trung gian chi luu khoa ngoai va thong tin phu cua moi quan he.

## 3. Vi sao can bang trung gian

Neu chi co `Enterprise` va `Product`, ta khong biet:

- Doanh nghiep nao nhap san pham nao.
- Moi doanh nghiep nhap bao nhieu san pham.
- San pham nao la san pham nhap nhieu nhat cua rieng mot doanh nghiep.

Bang trung gian giai quyet viec nay bang cach luu tung cap:

```text
EnterpriseId | ProductId | Quantity
1            | 2         | 100
1            | 3         | 250
2            | 2         | 80
```

`Quantity` khong the dat trong `Product`, vi cung mot san pham co the co so luong khac nhau o tung doanh nghiep.

## 4. Xac dinh API can lam

Can it nhat cac API:

```text
POST   /api/enterprises
PUT    /api/enterprises/{id}
DELETE /api/enterprises/{id}
GET    /api/enterprises?PageSize=10&PageIndex=1&Keyword=abc
GET    /api/enterprises/{enterpriseId}/top-products
```

Trong do:

- Them/sua doanh nghiep phai check trung `Name` va `TaxCode`.
- Danh sach doanh nghiep phai co `PageSize`, `PageIndex`.
- `Keyword` loc gan dung theo `Name` hoac `TaxCode`.
- Top products tra ra danh sach san pham co `Name` va `Code`.

## 5. Lo trinh lam bai 120 phut

### 0 - 10 phut: Tao project va folder

- Tao project Web API.
- Cai package EF Core SQL Server.
- Tao folder dung yeu cau.
- Cau hinh connection string.

### 10 - 30 phut: Viet entity va DbContext

- Tao 3 entity.
- Tao DbContext.
- Cau hinh primary key, foreign key, unique index.
- Dang ky DbContext trong `Program.cs`.

### 30 - 50 phut: Viet DTO, exception, constants

- Tao create/update/delete/filter DTO.
- Them validation annotation.
- Trim string trong setter.
- Tao `UserFriendlyException`.
- Tao message constants.

### 50 - 80 phut: Viet service

- Interface service.
- Implement service.
- Logic them/sua/xoa.
- Logic phan trang va keyword.
- Logic top products.

### 80 - 95 phut: Viet controller

- Route ro rang.
- Controller tra `IActionResult`.
- Goi service.
- Xu ly `UserFriendlyException`.

### 95 - 110 phut: Migration va seed data

- Chay migration.
- Update database.
- Them du lieu mau.
- Kiem tra bang va khoa ngoai.

### 110 - 120 phut: Test va sua loi

- Test tren Swagger/Postman.
- Check loi trung ten/ma so thue.
- Check phan trang, tim kiem.
- Check top products.
- Chay `dotnet build`.
