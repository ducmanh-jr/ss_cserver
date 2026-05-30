# ⚙️ Viết services interfaces và implements

> [!IMPORTANT]
> **Service Layer** là "trái tim" của ứng dụng, nơi chứa toàn bộ logic nghiệp vụ (business logic).

## 1. 🎯 Vai trò của Service

**✅ Service CẦN làm:**
- Thêm/Sửa/Xóa doanh nghiệp.
- Check trùng tên doanh nghiệp, mã số thuế.
- Lấy danh sách có phân trang và filter (keyword).
- Xử lý logic tính toán (VD: Lấy danh sách sản phẩm nhập nhiều nhất).

**❌ Service KHÔNG NÊN làm:**
- Nhận HTTP request hoặc trả `IActionResult` (việc của Controller).
- Chứa route API.

---

## 2. 📄 Tạo Interface Service

**Đường dẫn:** `Services/Interfaces/IEnterpriseService1234De1.cs`

> [!TIP]
> Việc tạo Interface giúp tuân thủ nguyên lý Dependency Inversion (DI), giúp Controller không bị phụ thuộc cứng vào Class cụ thể.

**Code:**

```csharp
using NguyenVanA1234.Dtos.Common;
using NguyenVanA1234.Dtos.Enterprises;
using NguyenVanA1234.Dtos.Products;

namespace NguyenVanA1234.Services.Interfaces;

public interface IEnterpriseService1234De1
{
    Task<EnterpriseDto1234De1> CreateAsync(CreateEnterpriseDto1234De1 input);
    Task<EnterpriseDto1234De1> UpdateAsync(int id, UpdateEnterpriseDto1234De1 input);
    Task DeleteAsync(int id);
    Task<PagedResultDto1234De1<EnterpriseDto1234De1>> GetListAsync(FilterEnterpriseDto1234De1 input);
    Task<List<TopProductDto1234De1>> GetTopProductsAsync(int enterpriseId);
}
```

---

## 3. 🛠️ Tạo Implement Service

**Đường dẫn:** `Services/Implements/EnterpriseService1234De1.cs`

> [!NOTE]
> - Nên gọi Database thông qua `AppDbContext`.
> - Gom nhóm các logic check trùng vào một private method để tái sử dụng.

**Code:**

```csharp
using Microsoft.EntityFrameworkCore;
using NguyenVanA1234.Constants;
using NguyenVanA1234.DbContexts;
using NguyenVanA1234.Dtos.Common;
using NguyenVanA1234.Dtos.Enterprises;
using NguyenVanA1234.Dtos.Products;
using NguyenVanA1234.Entities;
using NguyenVanA1234.Exceptions;
using NguyenVanA1234.Services.Interfaces;

namespace NguyenVanA1234.Services.Implements;

public class EnterpriseService1234De1 : IEnterpriseService1234De1
{
    private readonly AppDbContext1234De1 _dbContext;

    public EnterpriseService1234De1(AppDbContext1234De1 dbContext)
    {
        _dbContext = dbContext;
    }

    // --- 1. THÊM DOANH NGHIỆP ---
    public async Task<EnterpriseDto1234De1> CreateAsync(CreateEnterpriseDto1234De1 input)
    {
        await ValidateEnterpriseUniqueAsync(input.Name, input.TaxCode);

        var enterprise = new Enterprise1234De1
        {
            Name = input.Name,
            TaxCode = input.TaxCode,
            Address = input.Address
        };

        _dbContext.Enterprises.Add(enterprise);
        await _dbContext.SaveChangesAsync();

        return MapToEnterpriseDto(enterprise);
    }

    // --- 2. SỬA DOANH NGHIỆP ---
    public async Task<EnterpriseDto1234De1> UpdateAsync(int id, UpdateEnterpriseDto1234De1 input)
    {
        var enterprise = await _dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
        if (enterprise == null)
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);

        // Chú ý truyền id vào để bỏ qua bản ghi hiện tại khi check trùng!
        await ValidateEnterpriseUniqueAsync(input.Name, input.TaxCode, id);

        enterprise.Name = input.Name;
        enterprise.TaxCode = input.TaxCode;
        enterprise.Address = input.Address;

        await _dbContext.SaveChangesAsync();

        return MapToEnterpriseDto(enterprise);
    }

    // --- 3. XÓA DOANH NGHIỆP ---
    public async Task DeleteAsync(int id)
    {
        var enterprise = await _dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
        if (enterprise == null)
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);

        _dbContext.Enterprises.Remove(enterprise);
        await _dbContext.SaveChangesAsync();
    }

    // --- 4. DANH SÁCH & PHÂN TRANG ---
    public async Task<PagedResultDto1234De1<EnterpriseDto1234De1>> GetListAsync(FilterEnterpriseDto1234De1 input)
    {
        if (input.PageSize <= 0) throw new UserFriendlyException(ErrorMessages1234De1.PageSizeInvalid);
        if (input.PageIndex <= 0) throw new UserFriendlyException(ErrorMessages1234De1.PageIndexInvalid);

        var query = _dbContext.Enterprises.AsNoTracking();

        // Lọc Keyword
        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim();
            query = query.Where(e => e.Name.Contains(keyword) || e.TaxCode.Contains(keyword));
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderBy(e => e.Id)
            .Skip((input.PageIndex - 1) * input.PageSize) // Công thức phân trang chuẩn
            .Take(input.PageSize)
            .Select(e => new EnterpriseDto1234De1
            {
                Id = e.Id,
                Name = e.Name,
                TaxCode = e.TaxCode,
                Address = e.Address
            })
            .ToListAsync();

        return new PagedResultDto1234De1<EnterpriseDto1234De1>
        {
            TotalItems = totalItems,
            PageSize = input.PageSize,
            PageIndex = input.PageIndex,
            Items = items
        };
    }

    // --- 5. LOGIC PHỨC TẠP: TOP PRODUCTS ---
    public async Task<List<TopProductDto1234De1>> GetTopProductsAsync(int enterpriseId)
    {
        var enterpriseExists = await _dbContext.Enterprises.AnyAsync(e => e.Id == enterpriseId);
        if (!enterpriseExists)
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);

        // 5.1 Tìm Max Quantity của doanh nghiệp này
        var maxQuantity = await _dbContext.EnterpriseProducts
            .Where(ep => ep.EnterpriseId == enterpriseId)
            .MaxAsync(ep => (int?)ep.Quantity);

        if (maxQuantity == null)
            return new List<TopProductDto1234De1>();

        // 5.2 Lọc ra các sản phẩm có số lượng bằng Max
        return await _dbContext.EnterpriseProducts
            .AsNoTracking()
            .Where(ep => ep.EnterpriseId == enterpriseId && ep.Quantity == maxQuantity)
            .Select(ep => new TopProductDto1234De1
            {
                Name = ep.Product.Name,
                Code = ep.Product.Code
            })
            .ToListAsync();
    }

    // --- CÁC HÀM TIỆN ÍCH PRIVATE ---
    private async Task ValidateEnterpriseUniqueAsync(string name, string taxCode, int? currentId = null)
    {
        var nameExists = await _dbContext.Enterprises.AnyAsync(e =>
            e.Name == name && (!currentId.HasValue || e.Id != currentId.Value));

        if (nameExists) throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNameExists);

        var taxCodeExists = await _dbContext.Enterprises.AnyAsync(e =>
            e.TaxCode == taxCode && (!currentId.HasValue || e.Id != currentId.Value));

        if (taxCodeExists) throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseTaxCodeExists);
    }

    private static EnterpriseDto1234De1 MapToEnterpriseDto(Enterprise1234De1 enterprise)
    {
        return new EnterpriseDto1234De1
        {
            Id = enterprise.Id,
            Name = enterprise.Name,
            TaxCode = enterprise.TaxCode,
            Address = enterprise.Address
        };
    }
}
```

---

## 4. 🧠 Giải thích Logic

### Thêm & Sửa doanh nghiệp
> [!WARNING]
> Dòng cực kỳ quan trọng khi sửa: `e.Id != currentId.Value`
> - Nếu không bỏ qua `Id` hiện tại, khi user sửa thông tin khác nhưng giữ nguyên tên/mã số thuế, hệ thống sẽ báo lỗi trùng lặp (trùng với chính nó!).

### Phân trang
Công thức tính toán dòng cần bỏ qua `Skip`:
```csharp
Skip((PageIndex - 1) * PageSize).Take(PageSize)
```
- `PageIndex = 1`: Bỏ qua 0 dòng (Lấy trang đầu).
- `PageIndex = 2`: Bỏ qua `PageSize` dòng.

### Sản phẩm nhập nhiều nhất
> [!TIP]
> - Đầu tiên tìm ra số `Quantity` lớn nhất (Max).
> - Sau đó lấy TẤT CẢ sản phẩm có số lượng bằng Max (vì có thể có 2 sản phẩm cùng đạt số lượng lớn nhất!).
