# 📋 BẢNG TỔNG HỢP RÀNG BUỘC CHUẨN THEO ĐỀ BÀI

> Áp dụng cho: Đề 1 (Shipper), Đề 2 (Thiết bị), Đề 3 (Phương tiện), Đề Doanh nghiệp
> Chỉ cần thay tên Entity và thông báo lỗi theo từng đề. Cấu trúc giữ nguyên.

---

## 🗂️ LỚP 1: DTO — Kiểm tra dữ liệu đầu vào

### Ràng buộc bắt buộc với MỌI trường kiểu `string`:
```csharp
private string _tenTruong;

[Required(ErrorMessage = "Tên trường là bắt buộc")]
[StringLength(200, ErrorMessage = "Tên trường không được vượt quá 200 ký tự")]
public string TenTruong
{
    get => _tenTruong;
    set => _tenTruong = value?.Trim(); // ← BẮT BUỘC phải Trim()
}
```

| Annotation | Ý nghĩa | Khi nào dùng |
|---|---|---|
| `[Required]` | Bắt buộc phải nhập | Mọi trường quan trọng |
| `[StringLength(200)]` | Giới hạn độ dài chuỗi | Mọi trường kiểu string |
| `value?.Trim()` | Tự động cắt khoảng trắng 2 đầu | Mọi trường kiểu string |
| `[Range(1, int.MaxValue)]` | Giá trị số phải > 0 | Trường kiểu số (số lượng, tuổi...) |
| `[RegularExpression(@"^\d+$")]` | Chỉ cho phép chữ số | Khi đề yêu cầu chỉ nhập số |

### Độ dài chuỗi khuyến nghị theo từng loại trường:
| Loại trường | StringLength |
|---|---|
| Tên (người, doanh nghiệp, sản phẩm...) | 200 |
| Mã (MST, CCCD, mã sản phẩm...) | 50 |
| Địa chỉ, mô tả | 500 |
| Số điện thoại | 20 |

---

## 🗂️ LỚP 2: SERVICE — Kiểm tra logic nghiệp vụ

### Ràng buộc bắt buộc khi THÊM MỚI (Create):
```csharp
// 1. Kiểm tra trùng tên
if (await _context.Entities.AnyAsync(e => e.Ten == dto.Ten))
    throw new UserFriendlyException0210668De1("Tên đã tồn tại.");

// 2. Kiểm tra trùng mã
if (await _context.Entities.AnyAsync(e => e.Ma == dto.Ma))
    throw new UserFriendlyException0210668De1("Mã đã tồn tại.");
```

### Ràng buộc bắt buộc khi SỬA (Update):
```csharp
// 1. Kiểm tra tồn tại
var entity = await _context.Entities.FindAsync(id);
if (entity == null)
    throw new UserFriendlyException0210668De1("Không tìm thấy.");

// 2. Kiểm tra trùng tên (NGOẠI TRỪ chính nó - dùng && e.Id != id)
if (await _context.Entities.AnyAsync(e => e.Ten == dto.Ten && e.Id != id))
    throw new UserFriendlyException0210668De1("Tên đã tồn tại.");

// 3. Kiểm tra trùng mã (NGOẠI TRỪ chính nó)
if (await _context.Entities.AnyAsync(e => e.Ma == dto.Ma && e.Id != id))
    throw new UserFriendlyException0210668De1("Mã đã tồn tại.");
```

### Ràng buộc bắt buộc khi XÓA (Delete):
```csharp
// Kiểm tra tồn tại trước khi xóa
var entity = await _context.Entities.FindAsync(id);
if (entity == null)
    throw new UserFriendlyException0210668De1("Không tìm thấy.");
```

### Ràng buộc bắt buộc khi THỐNG KÊ:
```csharp
// Kiểm tra entity cha tồn tại
var entity = await _context.Entities.FindAsync(id);
if (entity == null)
    throw new UserFriendlyException0210668De1("Không tìm thấy.");

// Tìm giá trị MAX trong bảng quan hệ
var maxSoLuong = await _context.BangQuanHe
    .Where(x => x.EntityId == id)
    .MaxAsync(x => (int?)x.SoLuong) ?? 0;

if (maxSoLuong == 0) return new List<ResultDto>();

// Lấy danh sách có giá trị bằng MAX
var result = await _context.BangQuanHe
    .Where(x => x.EntityId == id && x.SoLuong == maxSoLuong)
    .Select(x => new ResultDto { ... })
    .ToListAsync();
```

---

## 🗂️ LỚP 3: DBCONTEXT — Ràng buộc cứng trong Database

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // 1. Khóa chính kép cho bảng trung gian (quan hệ n-n)
    modelBuilder.Entity<BangQuanHe>()
        .HasKey(x => new { x.EntityAId, x.EntityBId });

    // 2. Ràng buộc UNIQUE (không được trùng) - Entity A
    modelBuilder.Entity<EntityA>().HasIndex(e => e.Ten).IsUnique();
    modelBuilder.Entity<EntityA>().HasIndex(e => e.Ma).IsUnique();

    // 3. Ràng buộc UNIQUE (không được trùng) - Entity B
    modelBuilder.Entity<EntityB>().HasIndex(e => e.Ten).IsUnique();
    modelBuilder.Entity<EntityB>().HasIndex(e => e.Ma).IsUnique();
}
```

---

## 🗂️ LỚP 4: EXCEPTION MIDDLEWARE — Bắt lỗi tập trung

```csharp
// File: Exceptions/ExceptionMiddleware0210668De1.cs
public async Task InvokeAsync(HttpContext context)
{
    try
    {
        await _next(context);
    }
    catch (UserFriendlyException0210668De1 ex)
    {
        // Lỗi do mình tự throw → trả về 400 Bad Request
        context.Response.StatusCode = 400;
        var result = JsonSerializer.Serialize(new { error = ex.Message });
        await context.Response.WriteAsync(result);
    }
    catch (Exception ex)
    {
        // Lỗi hệ thống → trả về 500 Internal Server Error
        context.Response.StatusCode = 500;
        var result = JsonSerializer.Serialize(new { error = "Lỗi hệ thống" });
        await context.Response.WriteAsync(result);
    }
}
```

> ⚠️ **Quan trọng:** Phải đăng ký Middleware này trong `Program.cs`:
> ```csharp
> app.UseMiddleware<ExceptionMiddleware0210668De1>();
> ```

---

## 🗂️ LỚP 5: CONTROLLER — Validate Model

```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateDto dto)
{
    // BẮT BUỘC phải có dòng này để kích hoạt [Required], [StringLength]...
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var result = await _service.CreateAsync(dto);
    return Ok(result);
}
```

---

## 📌 CHECKLIST TRƯỚC KHI NỘP BÀI

- [ ] Mọi DTO đều có `Trim()` cho trường string
- [ ] Mọi DTO Create/Update đều có `[Required]` và `[StringLength]`
- [ ] Controller có `if (!ModelState.IsValid) return BadRequest(ModelState)`
- [ ] Service có kiểm tra trùng khi Create
- [ ] Service có kiểm tra trùng (ngoại trừ chính nó) khi Update
- [ ] Service có kiểm tra tồn tại khi Delete
- [ ] Service có kiểm tra tồn tại khi Thống kê
- [ ] DbContext có `HasIndex().IsUnique()` cho các trường unique
- [ ] DbContext có `HasKey(composite)` cho bảng trung gian n-n
- [ ] `ExceptionMiddleware` đã được đăng ký trong `Program.cs`
- [ ] Tên class đúng format: `<Tên><MSV><Đề số>` (vd: `ShipperDto0210668De1`)
- [ ] Cấu trúc thư mục đúng: Constants, Controllers, DbContexts, Dtos, Entities, Exceptions, Migrations, Services/Implements, Services/Interfaces, Utils
