# CHẶNG 2: ĐÓNG GÓI DỮ LIỆU & RÀNG BUỘC (DTO & VALIDATION)

## 📖 PHẦN LÝ THUYẾT: MỤC TIÊU
- **DTO (Tờ giấy Order):** Làm khiên trung gian, khách hàng chỉ được gửi các thông tin trên form này (Name, TaxCode), không được gửi thẳng vào Database.
- **Validation (Kiểm tra lỗi):** Chặn dữ liệu rác bằng các thẻ `[Required]`, `[MaxLength]`.

---

## 🛠️ PHẦN THAO TÁC TAY

### THAO TÁC 1: TẠO THƯ MỤC VÀ FILE DTO
**🎯 Mục tiêu & Ý nghĩa:** 
- Các file DTO (Data Transfer Object) giống như những **biểu mẫu chuyên biệt**. Khi user muốn thêm mới hoặc cập nhật thông tin, họ điền vào "biểu mẫu Create" hoặc "biểu mẫu Update".
- Khi hệ thống trả dữ liệu ra ngoài, hệ thống dùng "biểu mẫu Response". Điều này giúp ta **bảo vệ** Entity gốc và chỉ nhận/trả đúng những thông tin mà ta cho phép, đồng thời kiểm tra tính hợp lệ dữ liệu bằng các thuộc tính như `[Required]`.

1. Tại thư mục gốc dự án (`nguyenducmanh0210668`), tạo một thư mục mới tên là `Dtos`.
2. Bên trong thư mục `Dtos`, tạo tiếp một thư mục con tên là `Enterprises`.
3. Click chuột phải vào thư mục `Enterprises` vừa tạo, chọn **New File** để tạo lần lượt 3 file: 
   - `EnterpriseCreateDto.cs`
   - `EnterpriseUpdateDto.cs`
   - `EnterpriseResponseDto.cs`
4. Lần lượt mở các file này ra và copy/paste Code tương ứng ở bên dưới vào.

---

## 💻 PHẦN CODE ĐỂ COPY

**1. Code cho EnterpriseCreateDto.cs**
```csharp
using System.ComponentModel.DataAnnotations;

namespace nguyenducmanh0210668.Dtos.Enterprises;

public class EnterpriseCreateDto
{
    [Required(ErrorMessage = "Tên doanh nghiệp là bắt buộc")]
    [MaxLength(255, ErrorMessage = "Tên không quá 255 ký tự")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mã số thuế là bắt buộc")]
    [MaxLength(50)]
    public string TaxCode { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;
}
```

**2. Code cho EnterpriseUpdateDto.cs**
```csharp
using System.ComponentModel.DataAnnotations;

namespace nguyenducmanh0210668.Dtos.Enterprises;

public class EnterpriseUpdateDto
{
    [Required(ErrorMessage = "Tên doanh nghiệp là bắt buộc")]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mã số thuế là bắt buộc")]
    [MaxLength(50)]
    public string TaxCode { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;
}
```

**3. Code cho EnterpriseResponseDto.cs**
```csharp
namespace nguyenducmanh0210668.Dtos.Enterprises;

public class EnterpriseResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TaxCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
```
