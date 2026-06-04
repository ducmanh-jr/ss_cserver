# CHẶNG 3: XỬ LÝ NGHIỆP VỤ HỆ THỐNG (SERVICES)

## 📖 PHẦN LÝ THUYẾT: MỤC TIÊU
- Chứa toàn bộ não bộ của ứng dụng: Xử lý Thêm, Sửa, Xóa, Phân trang và Lấy Top Sản phẩm. Cắt khoảng trắng (Trim), kiểm tra trùng lặp và báo lỗi.

---

## 🛠️ PHẦN THAO TÁC TAY

### THAO TÁC 1: TẠO CUSTOM EXCEPTION
**🎯 Mục tiêu & Ý nghĩa:** 
- Tạo một "kiểu lỗi riêng" mang tên `UserFriendlyException`. Khi code nghiệp vụ của bạn phát hiện dữ liệu không hợp lệ (như trùng mã, trùng tên), nó sẽ "ném" ra lỗi này. Điều này giúp hệ thống dễ dàng phân biệt giữa lỗi dữ liệu người dùng nhập sai với lỗi hỏng hóc hệ thống nghiêm trọng.

1. Tại thư mục gốc dự án, tạo một thư mục mới tên là `Exceptions`.
2. Click chuột phải vào thư mục `Exceptions`, chọn **New File** và đặt tên là `UserFriendlyException.cs`.
3. Mở file ra và dán **Code số 1** bên dưới vào.

### THAO TÁC 2: TẠO INTERFACE
**🎯 Mục tiêu & Ý nghĩa:** 
- File `IEnterpriseService.cs` (Interface) đóng vai trò như một **Bản cam kết** (hay Menu). Nó chỉ liệt kê danh sách những hành động (Thêm, Sửa, Xóa...) mà ứng dụng CẦN PHẢI LÀM, mà chưa cần quan tâm bên trong làm như thế nào.

1. Tại thư mục gốc dự án, tạo một thư mục mới tên là `Services`.
2. Bên trong thư mục `Services`, tạo một thư mục con tên là `Interfaces`.
3. Click chuột phải vào thư mục `Interfaces`, chọn **New File** và đặt tên là `IEnterpriseService.cs`.
4. Mở file ra và dán **Code số 2** bên dưới vào (Đã bổ sung đầy đủ 5 hàm).

### THAO TÁC 3: TẠO SERVICE THỰC THI ĐẦY ĐỦ
**🎯 Mục tiêu & Ý nghĩa:** 
- File `EnterpriseService.cs` đóng vai trò là "Bộ não thực sự" (Lớp thực thi) của hệ thống. Nó sẽ dựa trên "Bản cam kết" bên trên để đi vào chi tiết **viết logic**: làm sao để kiểm tra trùng, làm sao để lưu xuống DB bằng DbContext, làm sao để phân trang.

1. Bên trong thư mục `Services` (đã tạo ở trên), tạo tiếp một thư mục con tên là `Implements`.
2. Click chuột phải vào thư mục `Implements`, chọn **New File** và đặt tên là `EnterpriseService.cs`.
3. Mở file ra và dán **Code số 3** bên dưới vào (Chứa full logic của toàn bộ bài thi).

### THAO TÁC 4: ĐĂNG KÝ VÀO PROGRAM.CS
**🎯 Mục tiêu & Ý nghĩa:** 
- Lệnh `AddScoped` giúp báo cho hệ thống biết (Dependency Injection) rằng: *"Bất cứ khi nào ai đó trong hệ thống cần dùng đến các chức năng trong `IEnterpriseService`, hãy cấp cho họ một phiên bản xử lý thực tế `EnterpriseService` để họ dùng"*.

1. Mở file `Program.cs`, tìm dòng `var app = builder.Build();`. Dán **Code số 4** vào **ngay trên** nó.

---

## 💻 PHẦN CODE ĐỂ COPY

**Code số 1: UserFriendlyException.cs**
```csharp
namespace nguyenducmanh0210668.Exceptions;
public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message) {}
}
```

**Code số 2: IEnterpriseService.cs**
```csharp
using nguyenducmanh0210668.Dtos.Enterprises;

namespace nguyenducmanh0210668.Services.Interfaces;

public interface IEnterpriseService
{
    Task<EnterpriseResponseDto> CreateAsync(EnterpriseCreateDto dto);
    Task<EnterpriseResponseDto> UpdateAsync(int id, EnterpriseUpdateDto dto);
    Task DeleteAsync(int id);
    Task<object> GetPagedAsync(int pageIndex, int pageSize, string? keyword);
    Task<object> GetTopProductsAsync(int enterpriseId);
}
```

**Code số 3: EnterpriseService.cs (FULL LOGIC BAO TRỌN ĐIỂM)**
```csharp
using Microsoft.EntityFrameworkCore;
using nguyenducmanh0210668.DbContexts;
using nguyenducmanh0210668.Dtos.Enterprises;
using nguyenducmanh0210668.Entities;
using nguyenducmanh0210668.Exceptions;
using nguyenducmanh0210668.Services.Interfaces;

namespace nguyenducmanh0210668.Services.Implements;

public class EnterpriseService : IEnterpriseService
{
    private readonly AppDbContext0210668 _context;

    public EnterpriseService(AppDbContext0210668 context) { _context = context; }

    public async Task<EnterpriseResponseDto> CreateAsync(EnterpriseCreateDto dto)
    {
        var name = dto.Name.Trim();
        var taxCode = dto.TaxCode.Trim();

        if (await _context.Enterprises.AnyAsync(e => e.Name == name || e.TaxCode == taxCode))
            throw new UserFriendlyException("Tên doanh nghiệp hoặc Mã số thuế đã tồn tại.");

        var entity = new Enterprise0210668 { Name = name, TaxCode = taxCode, Address = dto.Address?.Trim() ?? "" };
        _context.Enterprises.Add(entity);
        await _context.SaveChangesAsync();
        return new EnterpriseResponseDto { Id = entity.Id, Name = entity.Name, TaxCode = entity.TaxCode, Address = entity.Address };
    }

    public async Task<EnterpriseResponseDto> UpdateAsync(int id, EnterpriseUpdateDto dto)
    {
        var entity = await _context.Enterprises.FirstOrDefaultAsync(e => e.Id == id) 
            ?? throw new UserFriendlyException("Không tìm thấy doanh nghiệp.");

        var name = dto.Name.Trim();
        var taxCode = dto.TaxCode.Trim();

        if (await _context.Enterprises.AnyAsync(e => e.Id != id && (e.Name == name || e.TaxCode == taxCode)))
            throw new UserFriendlyException("Tên doanh nghiệp hoặc Mã số thuế bị trùng với đơn vị khác.");

        entity.Name = name;
        entity.TaxCode = taxCode;
        entity.Address = dto.Address?.Trim() ?? "";
        await _context.SaveChangesAsync();
        return new EnterpriseResponseDto { Id = entity.Id, Name = entity.Name, TaxCode = entity.TaxCode, Address = entity.Address };
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Enterprises.FirstOrDefaultAsync(e => e.Id == id) 
            ?? throw new UserFriendlyException("Không tìm thấy doanh nghiệp.");
        _context.Enterprises.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<object> GetPagedAsync(int pageIndex, int pageSize, string? keyword)
    {
        var query = _context.Enterprises.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(e => e.Name.Contains(keyword));

        var total = await query.CountAsync();
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .Select(e => new EnterpriseResponseDto { Id = e.Id, Name = e.Name, TaxCode = e.TaxCode, Address = e.Address })
            .ToListAsync();

        return new { TotalCount = total, Items = items };
    }

    public async Task<object> GetTopProductsAsync(int enterpriseId)
    {
        // Lấy danh sách sản phẩm nhập nhiều nhất của một doanh nghiệp
        var products = await _context.EnterpriseProducts
            .Include(ep => ep.Product)
            .Where(ep => ep.EnterpriseId == enterpriseId)
            .OrderByDescending(ep => ep.Quantity)
            .Take(5)
            .Select(ep => new {
                ProductName = ep.Product.Name,
                ProductCode = ep.Product.Code,
                Quantity = ep.Quantity
            })
            .ToListAsync();

        return products;
    }
}
```

**Code số 4: Bổ sung vào Program.cs (TRƯỚC dòng `var app = builder.Build();`)**
```csharp
builder.Services.AddScoped<nguyenducmanh0210668.Services.Interfaces.IEnterpriseService, nguyenducmanh0210668.Services.Implements.EnterpriseService>();
```
