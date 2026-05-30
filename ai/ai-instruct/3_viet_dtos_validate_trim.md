# Viet DTO, validate va trim string

## 1. Vi sao dung DTO

DTO dung de tach du lieu API khoi entity database.

Can DTO vi:

- Khong de client gui thua field nhu `Id` hoac navigation.
- Co the validate input bang DataAnnotations.
- Co the trim string ngay khi nhan request.
- Response chi tra field can thiet.

DTO khong nen chua:

- `DbContext`.
- Query LINQ vao database.
- Logic them/sua/xoa.

## 2. Tao DTO them doanh nghiep

Duong dan:

```text
Dtos/Enterprises/CreateEnterpriseDto1234De1.cs
```

File nay dung de nhan body khi them doanh nghiep.

Code:

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class CreateEnterpriseDto1234De1
{
    private string _name = string.Empty;
    private string _taxCode = string.Empty;
    private string _address = string.Empty;

    [Required(ErrorMessage = "Ten doanh nghiep khong duoc de trong")]
    [StringLength(255, ErrorMessage = "Ten doanh nghiep toi da 255 ky tu")]
    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Ma so thue khong duoc de trong")]
    [StringLength(50, ErrorMessage = "Ma so thue toi da 50 ky tu")]
    public string TaxCode
    {
        get => _taxCode;
        set => _taxCode = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Dia chi khong duoc de trong")]
    [StringLength(500, ErrorMessage = "Dia chi toi da 500 ky tu")]
    public string Address
    {
        get => _address;
        set => _address = value?.Trim() ?? string.Empty;
    }
}
```

## 3. Tao DTO sua doanh nghiep

Duong dan:

```text
Dtos/Enterprises/UpdateEnterpriseDto1234De1.cs
```

File nay dung de nhan body khi sua doanh nghiep.

Code:

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class UpdateEnterpriseDto1234De1
{
    private string _name = string.Empty;
    private string _taxCode = string.Empty;
    private string _address = string.Empty;

    [Required(ErrorMessage = "Ten doanh nghiep khong duoc de trong")]
    [StringLength(255, ErrorMessage = "Ten doanh nghiep toi da 255 ky tu")]
    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Ma so thue khong duoc de trong")]
    [StringLength(50, ErrorMessage = "Ma so thue toi da 50 ky tu")]
    public string TaxCode
    {
        get => _taxCode;
        set => _taxCode = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Dia chi khong duoc de trong")]
    [StringLength(500, ErrorMessage = "Dia chi toi da 500 ky tu")]
    public string Address
    {
        get => _address;
        set => _address = value?.Trim() ?? string.Empty;
    }
}
```

## 4. Tao DTO xoa doanh nghiep

Duong dan:

```text
Dtos/Enterprises/DeleteEnterpriseDto1234De1.cs
```

De bai yeu cau DTO create/update/delete/filter validate bang built-in annotation. Neu API xoa dung route id, DTO nay van nen tao de dung khi can xoa bang body hoac de dap ung cau truc bai.

Code:

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class DeleteEnterpriseDto1234De1
{
    [Range(1, int.MaxValue, ErrorMessage = "Id doanh nghiep phai lon hon 0")]
    public int Id { get; set; }
}
```

## 5. Tao DTO filter doanh nghiep

Duong dan:

```text
Dtos/Enterprises/FilterEnterpriseDto1234De1.cs
```

File nay dung de nhan query string `PageSize`, `PageIndex`, `Keyword`.

Code:

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class FilterEnterpriseDto1234De1
{
    private string? _keyword;

    [Range(1, 100, ErrorMessage = "PageSize phai tu 1 den 100")]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue, ErrorMessage = "PageIndex phai lon hon 0")]
    public int PageIndex { get; set; } = 1;

    [StringLength(255, ErrorMessage = "Keyword toi da 255 ky tu")]
    public string? Keyword
    {
        get => _keyword;
        set => _keyword = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
```

## 6. Tao DTO response doanh nghiep

Duong dan:

```text
Dtos/Enterprises/EnterpriseDto1234De1.cs
```

File nay dung de tra du lieu doanh nghiep ra API.

Code:

```csharp
namespace NguyenVanA1234.Dtos.Enterprises;

public class EnterpriseDto1234De1
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TaxCode { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}
```

## 7. Tao DTO phan trang dung chung

Duong dan:

```text
Dtos/Common/PagedResultDto1234De1.cs
```

File nay dung de tra ket qua co phan trang.

Code:

```csharp
namespace NguyenVanA1234.Dtos.Common;

public class PagedResultDto1234De1<T>
{
    public int TotalItems { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public List<T> Items { get; set; } = new();
}
```

## 8. Tao DTO top product

Duong dan:

```text
Dtos/Products/TopProductDto1234De1.cs
```

File nay dung de tra danh sach san pham nhap nhieu nhat cua mot doanh nghiep.

Theo de, dau ra gom:

- Ten san pham
- Ma san pham

Code:

```csharp
namespace NguyenVanA1234.Dtos.Products;

public class TopProductDto1234De1
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;
}
```

## 9. DTO bang trung gian neu can mo rong

Duong dan:

```text
Dtos/EnterpriseProducts/EnterpriseProductDto1234De1.cs
```

File nay khong bat buoc cho API trong de, nhung folder nay bat buoc co. Co the tao DTO don gian de bieu dien quan he neu giao vien hoi.

Code:

```csharp
namespace NguyenVanA1234.Dtos.EnterpriseProducts;

public class EnterpriseProductDto1234De1
{
    public int EnterpriseId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
```
