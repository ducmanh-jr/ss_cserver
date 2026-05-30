# Quy trinh lam bai sach tu dau den cuoi

## 1. Nguyen tac lam bai

Lam theo thu tu sau de tranh loi:

1. Tao project.
2. Cai package.
3. Tao folder.
4. Viet entity.
5. Viet DbContext.
6. Cau hinh `appsettings.json`.
7. Cau hinh `Program.cs`.
8. Viet DTO.
9. Viet exception/constants/utils.
10. Viet service interface.
11. Viet service implement.
12. Viet controller.
13. Build.
14. Migration.
15. Update database.
16. Test API.

Dung build nhieu lan, moi lan sau khi xong mot cum file quan trong.

## 2. Thu tu tao file

### Buoc 1: Entity

Tao:

```text
Entities/Enterprise1234De1.cs
Entities/Product1234De1.cs
Entities/EnterpriseProduct1234De1.cs
```

Muc tieu:

- Co du 3 bang.
- Quan he nhieu-nhieu co bang trung gian.
- `Quantity` nam dung trong bang trung gian.

### Buoc 2: DbContext

Tao:

```text
DbContexts/AppDbContext1234De1.cs
```

Muc tieu:

- Khai bao `DbSet`.
- Cau hinh table.
- Cau hinh key.
- Cau hinh foreign key.
- Cau hinh unique index.
- Seed data.

### Buoc 3: DTO

Tao:

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

Muc tieu:

- Input co validate.
- String input duoc trim.
- Response khong tra entity truc tiep.

### Buoc 4: Exception, constants, utils

Tao:

```text
Exceptions/UserFriendlyException.cs
Constants/ErrorMessages1234De1.cs
Constants/SuccessMessages1234De1.cs
Utils/StringUtils1234De1.cs
```

Muc tieu:

- Loi nghiep vu ro rang.
- Message khong viet lap lai.
- Co file utils nho neu can.

### Buoc 5: Service

Tao:

```text
Services/Interfaces/IEnterpriseService1234De1.cs
Services/Implements/EnterpriseService1234De1.cs
```

Muc tieu:

- Controller khong viet logic database.
- Service check trung ten/ma so thue.
- Service phan trang/tim kiem.
- Service lay top products.

### Buoc 6: Controller

Tao:

```text
Controllers/EnterprisesController1234De1.cs
```

Muc tieu:

- API dung HTTP method.
- Tra `IActionResult`.
- Route ro rang.
- Bat `UserFriendlyException`.

## 3. Thu tu chay lenh

Tao project:

```powershell
dotnet new webapi -n NguyenVanA1234
cd NguyenVanA1234
```

Cai package:

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

Chay project:

```powershell
dotnet run
```

## 4. Thu tu test API

Test theo thu tu nay:

1. `GET /api/enterprises?PageSize=10&PageIndex=1`
2. `GET /api/enterprises?PageSize=10&PageIndex=1&Keyword=ABC`
3. `POST /api/enterprises`
4. `POST /api/enterprises` voi ten trung.
5. `POST /api/enterprises` voi ma so thue trung.
6. `PUT /api/enterprises/{id}`
7. `PUT /api/enterprises/{id}` voi ten cua doanh nghiep khac.
8. `GET /api/enterprises/{enterpriseId}/top-products`
9. `DELETE /api/enterprises/{id}`
10. `DELETE /api/enterprises/{id}` voi id khong ton tai.

## 5. Checklist truoc khi nop bai

Mo migration va kiem tra:

- Co `CreateTable("Enterprises")`.
- Co `CreateTable("Products")`.
- Co `CreateTable("EnterpriseProducts")`.
- `EnterpriseProducts` co `Quantity`.
- Co `ForeignKey` den `Enterprises`.
- Co `ForeignKey` den `Products`.
- Co unique index cho `Name`, `TaxCode`, `Code`.

Mo code va kiem tra:

- Class dat dung `1234De1` theo MSSV/de.
- Public property PascalCase.
- Local variable camelCase.
- Private field `_camelCase`.
- DTO co `[Required]`, `[StringLength]`, `[Range]`.
- DTO string co setter trim.
- Controller tra `Task<IActionResult>`.
- Service khong tra `IActionResult`.
- Controller khong dung `_dbContext`.
- Service co `_dbContext`.
- `Program.cs` dang ky DbContext va service.

Test API va chup/ghi lai ket qua:

- Them doanh nghiep thanh cong.
- Them doanh nghiep trung ten bi loi.
- Them doanh nghiep trung ma so thue bi loi.
- Sua doanh nghiep thanh cong.
- Xoa doanh nghiep thanh cong.
- Phan trang thanh cong.
- Tim kiem keyword thanh cong.
- Top products tra dung san pham co quantity lon nhat.

## 6. Neu bi het thoi gian

Uu tien theo thu tu:

1. Entity + DbContext + migration dung.
2. API them/sua/xoa doanh nghiep.
3. Check trung ten/ma so thue.
4. Phan trang + keyword.
5. Top products.
6. Seed data.
7. Constants/utils lam dep code.

Khong nen danh qua nhieu thoi gian cho UI, auth, repository pattern, AutoMapper, middleware phuc tap, vi de khong yeu cau.

## 7. Cau tra loi ngan khi giao vien hoi quy trinh

Em tach project thanh cac tang don gian:

- Entity va DbContext de mo ta database.
- DTO de validate input va tra output gon.
- Service de xu ly nghiep vu.
- Controller de expose API va tra `IActionResult`.

Quan he doanh nghiep - san pham la nhieu-nhieu co du lieu phu `Quantity`, nen em tao entity trung gian `EnterpriseProduct`. Em dung EF Core Code First de tao migration va update SQL Server. Cac loi nghiep vu nhu trung ten, trung ma so thue, khong tim thay doanh nghiep duoc nem bang `UserFriendlyException`.
