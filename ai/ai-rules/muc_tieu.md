# Muc tieu va quy tac lam bai

## Muc tieu du an

Xay dung ung dung ASP.NET Core Web API quan ly quan he nhieu-nhieu giua:

- Doanh nghiep
- San pham
- Bang trung gian Doanh nghiep - San pham, co them `Quantity` de biet moi doanh nghiep da nhap bao nhieu san pham.

Muc tieu khong phai viet he thong lon, ma la viet dung de, dung nghiep vu, dung convention, co migration va co API test duoc.

## Yeu cau de bai

Doanh nghiep gom:

- Ten doanh nghiep
- Ma so thue kieu `string`
- Dia chi
- Ten doanh nghiep khong trung
- Ma so thue khong trung

San pham gom:

- Ten san pham
- Ma san pham
- Ngay nhap san pham
- Ten san pham khong trung
- Ma san pham khong trung

Bang quan he gom:

- `EnterpriseId`
- `ProductId`
- `Quantity` kieu `int`

Chuc nang bat buoc:

- Migration va update database bang EF Core Code First voi SQL Server.
- Them, sua, xoa doanh nghiep.
- Them/sua doanh nghiep phai check trung ten va ma so thue.
- Xem danh sach doanh nghiep co phan trang `PageSize`, `PageIndex`.
- Loc gan dung theo ten doanh nghiep hoac ma so thue bang `Keyword`.
- Liet ke san pham nhap nhieu nhat cua mot doanh nghiep.
- API san pham nhap nhieu nhat nhan dau vao la id doanh nghiep.
- Dau ra gom ten san pham va ma san pham.
- Co du lieu mau de test API.

## Coding convention

Dung dung quy tac sau:

- Class, interface, property, field public: `PascalCase`.
- Bien local: `camelCase`.
- Private field: `_camelCase`.
- Ten class theo mau `<TenClass><MSSV><DeThi>`.

Vi du MSSV `1234`, de `1`:

```csharp
public class Enterprise1234De1
public class Product1234De1
public class EnterpriseDto1234De1
public interface IEnterpriseService1234De1
public class EnterpriseService1234De1
```

## Cau truc folder chuan

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

## Tieu chi code sach

- Controller mong: chi nhan request, goi service, tra response.
- Service chua logic nghiep vu: validate trung, them, sua, xoa, tim kiem, phan trang.
- DbContext chi cau hinh database va quan he.
- DTO chi dung de nhan/tra du lieu API, khong chua logic truy van database.
- Entity chi dai dien bang trong database.
- Constants chua message dung lai nhieu lan.
- Exception dung cho loi nguoi dung co the hieu duoc.
- Utils chi chua ham nho, dung chung, khong phu thuoc nghiep vu cu the.

## Nhung thu khong duoc lam sai

- Khong bo qua bang trung gian `EnterpriseProduct`.
- Khong dat `Quantity` trong `Product` hoac `Enterprise`; `Quantity` thuoc quan he giua doanh nghiep va san pham.
- Khong tra entity truc tiep neu API chi can response DTO.
- Khong viet toan bo logic trong controller.
- Khong check trung bang so sanh co dau cach thua; phai trim input.
- Khong quen unique index cho ten/ma.
- Khong quen dang ky service va DbContext trong `Program.cs`.
- Khong quen chay `Update-Database`.
- Khong dung `PageIndex = 0`; nen quy uoc `PageIndex` bat dau tu 1.
