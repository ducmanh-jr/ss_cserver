# ⚠️ Viết exception, utils và constants

> [!IMPORTANT]
> Việc tạo các class quản lý lỗi và chuỗi (constants) giúp code của bạn "sạch", dễ bảo trì và thể hiện sự chuyên nghiệp khi chấm điểm.

## 1. 🚨 Tạo `UserFriendlyException`

**Đường dẫn:** `Exceptions/UserFriendlyException.cs`

File này dùng để ném lỗi nghiệp vụ mà người dùng (client) có thể hiểu được.

> [!TIP]
> **Vì sao cần file này?**
> - Không nên trả lỗi hệ thống thô (như stack trace) cho client.
> - Service có thể ném lỗi rõ ràng: trùng tên, không tìm thấy doanh nghiệp,...

**Code:**

```csharp
namespace NguyenVanA1234.Exceptions;

public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message)
    {
    }
}
```

---

## 2. 🔤 Tạo Constants Message

**Đường dẫn:** `Constants/ErrorMessages1234De1.cs`

Gom các thông báo lỗi dùng lại nhiều lần vào một chỗ. Tránh hard-code chuỗi!

> [!NOTE]
> Khi cần sửa đổi thông báo lỗi, bạn chỉ cần sửa ở 1 nơi duy nhất.

**Code:**

```csharp
namespace NguyenVanA1234.Constants;

public static class ErrorMessages1234De1
{
    public const string EnterpriseNotFound = "Không tìm thấy doanh nghiệp";
    public const string EnterpriseNameExists = "Tên doanh nghiệp đã tồn tại";
    public const string EnterpriseTaxCodeExists = "Mã số thuế đã tồn tại";
    public const string PageSizeInvalid = "PageSize phải lớn hơn 0";
    public const string PageIndexInvalid = "PageIndex phải lớn hơn 0";
}
```

### Tạo Success Messages (Khuyến nghị)
**Đường dẫn:** `Constants/SuccessMessages1234De1.cs`

**Code:**

```csharp
namespace NguyenVanA1234.Constants;

public static class SuccessMessages1234De1
{
    public const string CreateEnterpriseSuccess = "Thêm doanh nghiệp thành công";
    public const string UpdateEnterpriseSuccess = "Sửa doanh nghiệp thành công";
    public const string DeleteEnterpriseSuccess = "Xóa doanh nghiệp thành công";
}
```

---

## 3. 🛠️ Tạo util normalize string

**Đường dẫn:** `Utils/StringUtils1234De1.cs`

Chứa các hàm xử lý string dùng chung. Giữ file thật nhỏ gọn.

**Code:**

```csharp
namespace NguyenVanA1234.Utils;

public static class StringUtils1234De1
{
    public static string Normalize(string? value)
    {
        return value?.Trim() ?? string.Empty;
    }
}
```

---

## 4. ❓ Có nên tạo Middleware xử lý exception không?

> [!WARNING]
> **KHÔNG NÊN** thêm middleware phức tạp nếu đề không yêu cầu! Nó dễ gây lỗi và tốn thời gian trong phòng thi.

**Cách làm chuẩn cho bài thi:**
1. Service ném `UserFriendlyException`.
2. Controller `catch (UserFriendlyException ex)` và trả về `BadRequest(new { message = ex.Message })`.
3. Lỗi không mong muốn sẽ tự động bị ASP.NET Core catch và trả `500 Server Error`.
