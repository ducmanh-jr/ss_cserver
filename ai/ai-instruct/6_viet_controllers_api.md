# 🌐 Viết controllers và API

> [!IMPORTANT]
> **Controller** là cửa ngõ giao tiếp của ứng dụng. Nó tiếp nhận Request từ người dùng, chuyển giao cho Service xử lý và trả về Response (dưới dạng JSON).

## 1. 🎯 Vai trò của controller

**✅ Controller dùng để:**
- Nhận HTTP request (GET, POST, PUT, DELETE).
- Tự động Validate `ModelState` (nhờ `[ApiController]`).
- Gọi Service tương ứng.
- Đóng gói Response thành `IActionResult`.
- Bắt lỗi `UserFriendlyException` và trả về mã HTTP phù hợp (400 BadRequest).

**❌ Controller KHÔNG NÊN chứa:**
- Query database trực tiếp bằng EF Core.
- Logic nghiệp vụ (check trùng lặp, tính toán số liệu).

---

## 2. 📝 Tạo `EnterprisesController1234De1`

**Đường dẫn:** `Controllers/EnterprisesController1234De1.cs`

> [!NOTE]
> Nhờ Dependency Injection, ta chỉ cần truyền `IEnterpriseService1234De1` vào constructor, ASP.NET Core sẽ tự động tiêm class implement vào!

**Code:**

```csharp
using Microsoft.AspNetCore.Mvc;
using NguyenVanA1234.Constants;
using NguyenVanA1234.Dtos.Enterprises;
using NguyenVanA1234.Exceptions;
using NguyenVanA1234.Services.Interfaces;

namespace NguyenVanA1234.Controllers;

[ApiController]
[Route("api/enterprises")]
public class EnterprisesController1234De1 : ControllerBase
{
    private readonly IEnterpriseService1234De1 _enterpriseService;

    public EnterprisesController1234De1(IEnterpriseService1234De1 enterpriseService)
    {
        _enterpriseService = enterpriseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnterpriseDto1234De1 input)
    {
        try
        {
            var result = await _enterpriseService.CreateAsync(input);
            return Ok(new
            {
                Message = SuccessMessages1234De1.CreateEnterpriseSuccess,
                Data = result
            });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEnterpriseDto1234De1 input)
    {
        try
        {
            var result = await _enterpriseService.UpdateAsync(id, input);
            return Ok(new
            {
                Message = SuccessMessages1234De1.UpdateEnterpriseSuccess,
                Data = result
            });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _enterpriseService.DeleteAsync(id);
            return Ok(new { Message = SuccessMessages1234De1.DeleteEnterpriseSuccess });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] FilterEnterpriseDto1234De1 input)
    {
        try
        {
            var result = await _enterpriseService.GetListAsync(input);
            return Ok(result);
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("{enterpriseId:int}/top-products")]
    public async Task<IActionResult> GetTopProducts(int enterpriseId)
    {
        try
        {
            var result = await _enterpriseService.GetTopProductsAsync(enterpriseId);
            return Ok(result);
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
```

---

## 3. 🔍 Giải thích Route

| Attribute | HTTP Method | URL | Mục đích |
| :--- | :--- | :--- | :--- |
| `[Route("api/enterprises")]` | (Base Route) | `/api/enterprises` | Tiền tố áp dụng cho toàn bộ controller. |
| `[HttpPost]` | POST | `/api/enterprises` | Thêm mới doanh nghiệp. |
| `[HttpPut("{id:int}")]` | PUT | `/api/enterprises/1` | Sửa doanh nghiệp theo ID. |
| `[HttpDelete("{id:int}")]` | DELETE | `/api/enterprises/1` | Xóa doanh nghiệp theo ID. |
| `[HttpGet]` | GET | `/api/enterprises?PageSize=10` | Lấy danh sách (Có phân trang/tìm kiếm). |
| `[HttpGet("{enterpriseId:int}/top-products")]` | GET | `/api/enterprises/1/top-products` | Route đặc biệt lấy sản phẩm nhập nhiều nhất. |

---

## 4. ❓ Tại sao lại trả về `IActionResult`?

`IActionResult` là interface vô cùng mạnh mẽ, giúp API có thể trả về các HTTP Status Code chuẩn xác:
- `Ok(...)` ➔ **HTTP 200** (Thành công).
- `BadRequest(...)` ➔ **HTTP 400** (Lỗi validate hoặc lỗi do người dùng).
- `NotFound(...)` ➔ **HTTP 404** (Không tìm thấy tài nguyên).

> [!WARNING]
> Đề thi thường yêu cầu trả về chuẩn `IActionResult`. Tuyệt đối không trả về object/DTO một cách trực tiếp!

---

## 5. 🛠️ Về `ModelState.IsValid`

> [!TIP]
> Do controller có attribute `[ApiController]`, ASP.NET Core sẽ **tự động** kiểm tra `ModelState.IsValid` và ném ra lỗi `400 BadRequest` nếu DataAnnotations ở DTO bị sai (VD: Thiếu field bắt buộc, quá độ dài ký tự). Bạn không cần phải viết code check thủ công.
