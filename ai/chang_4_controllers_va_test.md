# CHẶNG 4: CUNG CẤP API ENDPOINTS (CONTROLLERS)

## 📖 PHẦN LÝ THUYẾT: MỤC TIÊU
- **Controller:** Lễ tân đón khách, gọi các chức năng Thêm, Sửa, Xóa, Tìm kiếm từ Service để phục vụ HTTP Request.

---

## 🛠️ PHẦN THAO TÁC TAY

### THAO TÁC 1: TẠO CONTROLLER ĐẦY ĐỦ
**🎯 Mục tiêu & Ý nghĩa:** 
- File `EnterprisesController.cs` đóng vai trò là một "Người Lễ Tân". Nó mở ra các cánh cửa (Endpoints) như `POST /api/enterprises`, `GET /api/enterprises` để nhận yêu cầu (Requests) từ trình duyệt hay người dùng gửi tới, rồi chuyển giao yêu cầu đó cho "Bộ não" Service ở Chặng 3 xử lý. Sau khi xử lý xong, Lễ Tân lại trả kết quả (Response) ra ngoài.

1. Tại thư mục gốc dự án, tạo một thư mục mới tên là `Controllers` (nếu chưa có).
2. Click chuột phải vào thư mục `Controllers`, chọn **New File** và đặt tên là `EnterprisesController.cs`.
3. Mở file đó ra, dán **Code số 1** bên dưới vào (Chứa đủ 5 Endpoint API).

### THAO TÁC 2: CẤU HÌNH XỬ LÝ LỖI Ở PROGRAM.CS
**🎯 Mục tiêu & Ý nghĩa:** 
- Bắt tất cả các lỗi được "ném" ra trong hệ thống một cách tập trung (như lỗi `UserFriendlyException` đã làm ở Chặng 3). Nếu bắt được lỗi do người dùng nhập sai, đoạn code này sẽ trả ra trạng thái `400 Bad Request` kèm theo tin nhắn lỗi thật dễ hiểu cho người dùng cuối thay vì văng ra trang báo lỗi sập hệ thống (500 Internal Server Error).

1. Mở file `Program.cs`.
2. Dán **Code số 2** vào TRƯỚC dòng `app.UseAuthorization();`.

---

## 💻 PHẦN CODE ĐỂ COPY

**Code số 1: EnterprisesController.cs (FULL ENDPOINTS)**
```csharp
using Microsoft.AspNetCore.Mvc;
using nguyenducmanh0210668.Dtos.Enterprises;
using nguyenducmanh0210668.Services.Interfaces;

namespace nguyenducmanh0210668.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnterprisesController : ControllerBase
{
    private readonly IEnterpriseService _enterpriseService;

    public EnterprisesController(IEnterpriseService enterpriseService)
    {
        _enterpriseService = enterpriseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnterprise([FromBody] EnterpriseCreateDto dto)
    {
        var result = await _enterpriseService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEnterprise(int id, [FromBody] EnterpriseUpdateDto dto)
    {
        var result = await _enterpriseService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnterprise(int id)
    {
        await _enterpriseService.DeleteAsync(id);
        return Ok(new { Message = "Xóa doanh nghiệp thành công" });
    }

    [HttpGet]
    public async Task<IActionResult> GetEnterprises([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string? keyword = null)
    {
        var result = await _enterpriseService.GetPagedAsync(pageIndex, pageSize, keyword);
        return Ok(result);
    }

    [HttpGet("{id}/top-products")]
    public async Task<IActionResult> GetTopProducts(int id)
    {
        var result = await _enterpriseService.GetTopProductsAsync(id);
        return Ok(result);
    }
}
```

**Code số 2: Bổ sung vào Program.cs (TRƯỚC dòng `app.UseAuthorization();`)**
```csharp
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception is nguyenducmanh0210668.Exceptions.UserFriendlyException userFriendlyException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { ErrorMessage = userFriendlyException.Message });
        }
    });
});
```
