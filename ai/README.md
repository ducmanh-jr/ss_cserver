# Huong dan lam bai ASP.NET Core Web API

Bo tai lieu nay giup lam lai du an tu dau cho de thi ASP.NET Core Web API: quan he nhieu-nhieu giua Doanh nghiep va San pham, bang trung gian co `Quantity`.

Trong cac vi du code, tai lieu dung mau ten `1234De1`. Khi lam bai that, thay:

- `1234` bang ma so sinh vien.
- `De1` bang ma de thi.
- Ten project bang `<HoVaTen><MSSV>`, vi du `NguyenVanA1234`.

## Thu tu doc

1. `ai-rules/muc_tieu.md`
2. `ai-instruct/0_doc_de_va_lo_trinh.md`
3. `ai-instruct/1_khoi_tao_project_va_cau_hinh.md`
4. `ai-instruct/2_thiet_ke_database_entities_dbcontext.md`
5. `ai-instruct/3_viet_dtos_validate_trim.md`
6. `ai-instruct/4_viet_exception_utils_constants.md`
7. `ai-instruct/5_viet_services_interfaces_implements.md`
8. `ai-instruct/6_viet_controllers_api.md`
9. `ai-instruct/7_migration_update_database_seed_test.md`
10. `ai-instruct/8_kiem_tra_loi_va_van_dap.md`
11. `ai-instruct/9_quy_trinh_lam_bai_sach_tu_dau_den_cuoi.md`

## Cong nghe dung

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Code First Migration
- Dependency Injection
- LINQ method syntax
- DataAnnotations validation
- Swagger/Postman de test API

## Cau truc folder can co

```text
Properties/
Constants/
Controllers/
DbContexts/
Dtos/
  Enterprises/
  Products/
  EnterpriseProducts/
  Common/
Entities/
Exceptions/
Migrations/
Services/
  Implements/
  Interfaces/
Utils/
appsettings.json
Program.cs
```

## Lenh kiem tra nhanh

```powershell
dotnet build
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Neu chua co tool EF:

```powershell
dotnet tool install --global dotnet-ef
```

## Cac API can demo

- `POST /api/enterprises` them doanh nghiep
- `PUT /api/enterprises/{id}` sua doanh nghiep
- `DELETE /api/enterprises/{id}` xoa doanh nghiep
- `GET /api/enterprises?PageSize=10&PageIndex=1&Keyword=abc` danh sach co phan trang va tim kiem gan dung
- `GET /api/enterprises/{enterpriseId}/top-products` danh sach san pham nhap nhieu nhat cua mot doanh nghiep

## Ket qua dung de

Du an dat yeu cau khi:

- Database tao bang `Enterprises`, `Products`, `EnterpriseProducts`.
- `EnterpriseProducts` co khoa ngoai den doanh nghiep, san pham va co cot `Quantity`.
- Ten doanh nghiep va ma so thue khong trung.
- Ten san pham va ma san pham khong trung.
- DTO co validate bang annotation.
- String trong DTO duoc trim.
- Controller tra ve `IActionResult`.
- Loi nghiep vu nem `UserFriendlyException`.
- Service duoc dang ky DI va controller chi goi service.
