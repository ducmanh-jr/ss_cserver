# 📦 Viết DTO, validate và trim string

> [!IMPORTANT]
> DTO (Data Transfer Object) dùng để tách dữ liệu API khỏi entity database. Điều này giúp bảo mật dữ liệu và chuẩn hóa dữ liệu đầu vào.

## 1. ❓ Vì sao dùng DTO

Cần DTO vì:
- **Bảo mật:** Không để client gửi thừa field như `Id` hoặc navigation.
- **Validation:** Có thể validate input bằng DataAnnotations.
- **Chuẩn hóa:** Có thể `Trim()` string ngay khi nhận request.
- **Tối ưu:** Response chỉ trả về các field cần thiết.

> [!WARNING]
> DTO **không nên** chứa:
> - `DbContext`.
> - Query LINQ vào database.
> - Logic nghiệp vụ (thêm/sửa/xóa).

---

## 2. 📝 Tạo DTO thêm doanh nghiệp

**Đường dẫn:** `Dtos/Enterprises/CreateEnterpriseDto1234De1.cs`

> [!TIP]
> Sử dụng field `_name` kết hợp thuộc tính `Name` để tự động `Trim()` khoảng trắng ngay khi dữ liệu được set!

**Code:**

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class CreateEnterpriseDto1234De1
{
    private string _name = string.Empty;
    private string _taxCode = string.Empty;
    private string _address = string.Empty;

    [Required(ErrorMessage = "Tên doanh nghiệp không được để trống")]
    [StringLength(255, ErrorMessage = "Tên doanh nghiệp tối đa 255 ký tự")]
    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Mã số thuế không được để trống")]
    [StringLength(50, ErrorMessage = "Mã số thuế tối đa 50 ký tự")]
    public string TaxCode
    {
        get => _taxCode;
        set => _taxCode = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [StringLength(500, ErrorMessage = "Địa chỉ tối đa 500 ký tự")]
    public string Address
    {
        get => _address;
        set => _address = value?.Trim() ?? string.Empty;
    }
}
```

---

## 3. ✏️ Tạo DTO sửa doanh nghiệp

**Đường dẫn:** `Dtos/Enterprises/UpdateEnterpriseDto1234De1.cs`

*(Giống hệt Create DTO nhưng việc tách riêng giúp bạn dễ dàng mở rộng logic sửa đổi sau này nếu cần)*

**Code:**

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class UpdateEnterpriseDto1234De1
{
    private string _name = string.Empty;
    private string _taxCode = string.Empty;
    private string _address = string.Empty;

    [Required(ErrorMessage = "Tên doanh nghiệp không được để trống")]
    [StringLength(255, ErrorMessage = "Tên doanh nghiệp tối đa 255 ký tự")]
    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Mã số thuế không được để trống")]
    [StringLength(50, ErrorMessage = "Mã số thuế tối đa 50 ký tự")]
    public string TaxCode
    {
        get => _taxCode;
        set => _taxCode = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [StringLength(500, ErrorMessage = "Địa chỉ tối đa 500 ký tự")]
    public string Address
    {
        get => _address;
        set => _address = value?.Trim() ?? string.Empty;
    }
}
```

---

## 4. 🗑️ Tạo DTO xóa doanh nghiệp

**Đường dẫn:** `Dtos/Enterprises/DeleteEnterpriseDto1234De1.cs`

> [!NOTE]
> Đề bài yêu cầu DTO create/update/delete/filter validate bằng built-in annotation. Nếu API xóa dùng route id, DTO này vẫn nên tạo để đáp ứng cấu trúc bài thi.

**Code:**

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class DeleteEnterpriseDto1234De1
{
    [Range(1, int.MaxValue, ErrorMessage = "Id doanh nghiệp phải lớn hơn 0")]
    public int Id { get; set; }
}
```

---

## 5. 🔍 Tạo DTO filter doanh nghiệp

**Đường dẫn:** `Dtos/Enterprises/FilterEnterpriseDto1234De1.cs`

Dùng để nhận query string `PageSize`, `PageIndex`, `Keyword`. Tự động set mặc định và trim keyword!

**Code:**

```csharp
using System.ComponentModel.DataAnnotations;

namespace NguyenVanA1234.Dtos.Enterprises;

public class FilterEnterpriseDto1234De1
{
    private string? _keyword;

    [Range(1, 100, ErrorMessage = "PageSize phải từ 1 đến 100")]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue, ErrorMessage = "PageIndex phải lớn hơn 0")]
    public int PageIndex { get; set; } = 1;

    [StringLength(255, ErrorMessage = "Keyword tối đa 255 ký tự")]
    public string? Keyword
    {
        get => _keyword;
        set => _keyword = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
```

---

## 6. 📤 Tạo DTO response (Data Output)

### 6.1 DTO Response Doanh nghiệp
**Đường dẫn:** `Dtos/Enterprises/EnterpriseDto1234De1.cs`

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

### 6.2 DTO Top Product
**Đường dẫn:** `Dtos/Products/TopProductDto1234De1.cs`

```csharp
namespace NguyenVanA1234.Dtos.Products;

public class TopProductDto1234De1
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
```

### 6.3 DTO Phân trang dùng chung (Generic)
**Đường dẫn:** `Dtos/Common/PagedResultDto1234De1.cs`

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
