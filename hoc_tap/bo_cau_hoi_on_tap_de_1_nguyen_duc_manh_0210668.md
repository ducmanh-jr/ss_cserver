# Bộ câu hỏi ôn tập Đề 1 - Nguyễn Đức Mạnh 0210668

Môn: Phát triển ứng dụng phía Server  
Dự án: `NguyenDucManh0210668`  
Công nghệ: ASP.NET Core Web API, Entity Framework Core, MySQL  
Bài toán: Quan hệ n-n giữa Nhân viên và Dự án thông qua bảng Phân công, có trường Số giờ làm việc.

## Tổng quan đề bài

Đề yêu cầu xây dựng Web API quản lý:

- Nhân viên: tên nhân viên, mã nhân viên, email.
- Dự án: tên dự án, mã dự án.
- Phân công: nhân viên tham gia dự án nào và số giờ làm việc.

Các ràng buộc chính:

- Mã nhân viên không được trùng.
- Email không được trùng.
- Tên dự án không được trùng.
- Mã dự án không được trùng.
- Khóa chính các bảng là `int`, tự tăng.
- Có quan hệ khóa ngoại rõ ràng trong migration bằng Fluent API.
- API trả response JSON thống nhất gồm `isSuccess`, `data`, `message`, `code`.
- Xử lý lỗi nghiệp vụ bằng `UserFriendlyException0210668De1`.

## Bộ 1 - Kiến trúc project

### Câu 1

Project được tổ chức theo những thư mục chính nào? Mỗi thư mục có nhiệm vụ gì?

### Đáp án

Project có các thư mục chính:

| Thư mục | Vai trò |
|---|---|
| `Constants` | Chứa hằng số thông báo dùng chung |
| `Controllers` | Nhận request HTTP và trả `IActionResult` |
| `DbContexts` | Chứa `AppDbContext0210668De1`, cấu hình Entity Framework Core |
| `Dtos` | Chứa DTO chia theo nghiệp vụ `NhanViens`, `DuAns`, `PhanCongs` |
| `Entities` | Chứa entity ánh xạ bảng database |
| `Exceptions` | Chứa `UserFriendlyException0210668De1` |
| `Migrations` | Chứa migration tạo bảng, khóa chính, khóa ngoại, index |
| `Services/Interfaces` | Khai báo interface service |
| `Services/Implements` | Cài đặt logic nghiệp vụ |
| `Utils` | Chứa response chuẩn, middleware xử lý lỗi, phân trang |

Luồng xử lý chuẩn:

```text
Client
-> Controller
-> Service interface
-> Service implementation
-> DbContext
-> MySQL
```

### Câu 2

Vì sao Controller không nên xử lý trực tiếp logic trùng mã nhân viên hoặc email?

### Đáp án

Controller chỉ nên nhận request, gọi service và trả response. Logic nghiệp vụ như kiểm tra trùng mã nhân viên, trùng email, kiểm tra nhân viên tồn tại nên đặt trong service.

Trong project, logic này nằm ở:

```text
Services/Implements/NhanVienService0210668De1.cs
```

Hàm kiểm tra:

```csharp
private async Task EnsureUniqueNhanVienAsync(string maNhanVien, string email, int? ignoreId)
```

Lợi ích:

- Controller gọn, đúng trách nhiệm.
- Logic có thể tái sử dụng.
- Dễ test.
- Dễ bảo trì khi nghiệp vụ thay đổi.

### Câu 3

Dependency Injection được cấu hình ở đâu?

### Đáp án

DI được cấu hình trong `Program.cs`:

```csharp
builder.Services.AddScoped<INhanVienService0210668De1, NhanVienService0210668De1>();
builder.Services.AddScoped<IDuAnService0210668De1, DuAnService0210668De1>();
builder.Services.AddScoped<IPhanCongService0210668De1, PhanCongService0210668De1>();
```

Controller nhận service qua constructor:

```csharp
private readonly INhanVienService0210668De1 _nhanVienService;

public NhanVienController0210668De1(INhanVienService0210668De1 nhanVienService)
{
    _nhanVienService = nhanVienService;
}
```

## Bộ 2 - Entity và quan hệ database

### Câu 1

Ba entity chính trong bài là gì? Mỗi entity tương ứng bảng nào?

### Đáp án

Ba entity chính:

| Entity | Bảng |
|---|---|
| `NhanVien0210668De1` | `NhanViens` |
| `DuAn0210668De1` | `DuAns` |
| `PhanCong0210668De1` | `PhanCongs` |

`NhanVien0210668De1` có các trường chính:

```csharp
public int Id { get; set; }
public string TenNhanVien { get; set; } = string.Empty;
public string MaNhanVien { get; set; } = string.Empty;
public string Email { get; set; } = string.Empty;
```

`DuAn0210668De1` có:

```csharp
public int Id { get; set; }
public string TenDuAn { get; set; } = string.Empty;
public string MaDuAn { get; set; } = string.Empty;
```

`PhanCong0210668De1` có:

```csharp
public int Id { get; set; }
public int NhanVienId { get; set; }
public int DuAnId { get; set; }
public int SoGioLamViec { get; set; }
```

### Câu 2

Quan hệ n-n giữa Nhân viên và Dự án được biểu diễn như thế nào?

### Đáp án

Quan hệ n-n được tách thành hai quan hệ 1-n thông qua bảng trung gian `PhanCongs`.

```text
NhanViens 1 - n PhanCongs n - 1 DuAns
```

Mỗi bản ghi `PhanCongs` cho biết:

- Nhân viên nào.
- Dự án nào.
- Số giờ làm việc là bao nhiêu.

Trong entity:

```csharp
public ICollection<PhanCong0210668De1> PhanCongs { get; set; }
```

Trong bảng phân công:

```csharp
public int NhanVienId { get; set; }
public int DuAnId { get; set; }
public int SoGioLamViec { get; set; }
```

### Câu 3

Khóa ngoại được cấu hình ở đâu?

### Đáp án

Khóa ngoại được cấu hình trong `AppDbContext0210668De1`, hàm `OnModelCreating`.

```csharp
entity.HasOne(phanCong => phanCong.NhanVien)
    .WithMany(nhanVien => nhanVien.PhanCongs)
    .HasForeignKey(phanCong => phanCong.NhanVienId)
    .OnDelete(DeleteBehavior.Cascade);
```

```csharp
entity.HasOne(phanCong => phanCong.DuAn)
    .WithMany(duAn => duAn.PhanCongs)
    .HasForeignKey(phanCong => phanCong.DuAnId)
    .OnDelete(DeleteBehavior.Cascade);
```

Trong migration MySQL, khóa ngoại được sinh thành:

```sql
FOREIGN KEY (`NhanVienId`) REFERENCES `NhanViens` (`Id`) ON DELETE CASCADE
FOREIGN KEY (`DuAnId`) REFERENCES `DuAns` (`Id`) ON DELETE CASCADE
```

## Bộ 3 - Migration và MySQL

### Câu 1

Project đang dùng database gì và cấu hình ở đâu?

### Đáp án

Project đang dùng MySQL.

Connection string nằm trong `appsettings.json` và `appsettings.Development.json`:

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=nguyenducmanh0210668de1;User=root;Password=0000;AllowPublicKeyRetrieval=True;SslMode=None"
```

Provider MySQL được cấu hình trong `Program.cs`:

```csharp
options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 0)));
```

### Câu 2

Lệnh nào dùng để tạo database từ migration?

### Đáp án

Chạy trong thư mục project:

```powershell
dotnet ef database update
```

Lệnh này sẽ:

1. Build project.
2. Đọc migration trong thư mục `Migrations`.
3. Kết nối MySQL.
4. Tạo database `nguyenducmanh0210668de1` nếu chưa có.
5. Tạo các bảng `NhanViens`, `DuAns`, `PhanCongs`.
6. Ghi lịch sử migration vào bảng `__EFMigrationsHistory`.

### Câu 3

Làm sao biết migration đã được áp dụng?

### Đáp án

Chạy:

```powershell
dotnet ef migrations list
```

Kết quả đúng sẽ có migration:

```text
20260603030402_InitialCreate0210668De1
```

Hoặc kiểm tra trong MySQL:

```sql
SELECT * FROM __EFMigrationsHistory;
```

## Bộ 4 - DTO và validation

### Câu 1

DTO tạo nhân viên validate những trường nào?

### Đáp án

DTO tạo nhân viên là:

```text
Dtos/NhanViens/NhanVienCreateDto0210668De1.cs
```

Các trường validate:

| Trường | Validation |
|---|---|
| `TenNhanVien` | `[Required]`, `[StringLength(150, MinimumLength = 2)]` |
| `MaNhanVien` | `[Required]`, `[StringLength(50, MinimumLength = 2)]` |
| `Email` | `[Required]`, `[StringLength(150)]`, `[EmailAddress]` |

Ví dụ:

```csharp
[Required(ErrorMessage = "Email là bắt buộc.")]
[StringLength(150, ErrorMessage = "Email không được vượt quá 150 ký tự.")]
[EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
public string Email
```

### Câu 2

Yêu cầu trim string trong DTO được xử lý như thế nào?

### Đáp án

String được trim ngay trong setter của DTO.

Ví dụ:

```csharp
private string _tenNhanVien = string.Empty;

public string TenNhanVien
{
    get => _tenNhanVien;
    set => _tenNhanVien = value?.Trim() ?? string.Empty;
}
```

Nếu client gửi:

```json
{
  "tenNhanVien": "  Nguyễn Đức Mạnh  "
}
```

DTO sẽ nhận thành:

```text
Nguyễn Đức Mạnh
```

### Câu 3

Khi client gửi thiếu email lúc tạo nhân viên, API trả gì?

### Đáp án

Do Controller có `[ApiController]`, model validation sẽ chạy tự động. Project đã cấu hình response validation trong `Program.cs`.

Khi thiếu email, API trả HTTP `400 Bad Request` với format:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Email là bắt buộc.",
  "code": 400
}
```

## Bộ 5 - API Nhân viên

### Câu 1

API thêm nhân viên nằm ở đâu? Luồng xử lý như thế nào?

### Đáp án

API thêm nhân viên nằm trong:

```text
Controllers/NhanVienController0210668De1.cs
```

Route:

```http
POST /api/nhan-viens
```

Luồng xử lý:

```text
Client gửi JSON
-> NhanVienController0210668De1.CreateAsync()
-> INhanVienService0210668De1.CreateAsync()
-> NhanVienService0210668De1.CreateAsync()
-> Kiểm tra trùng mã nhân viên và email
-> Add entity vào DbContext
-> SaveChangesAsync()
-> Trả NhanVienDto0210668De1
```

Body mẫu:

```json
{
  "tenNhanVien": "Nguyễn Đức Mạnh",
  "maNhanVien": "NV001",
  "email": "manh@example.com"
}
```

### Câu 2

Khi thêm nhân viên bị trùng mã nhân viên, lỗi được xử lý ở đâu?

### Đáp án

Lỗi được kiểm tra trong service:

```text
Services/Implements/NhanVienService0210668De1.cs
```

Logic:

```csharp
var duplicatedMaNhanVien = await _dbContext.NhanViens
    .AnyAsync(item => item.MaNhanVien == maNhanVien && (!ignoreId.HasValue || item.Id != ignoreId.Value));

if (duplicatedMaNhanVien)
{
    throw new UserFriendlyException0210668De1("Mã nhân viên đã tồn tại.");
}
```

Middleware bắt lỗi và trả response:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Mã nhân viên đã tồn tại.",
  "code": 400
}
```

### Câu 3

API sửa nhân viên có kiểm tra trùng mã và email không?

### Đáp án

Có.

Khi sửa, service gọi:

```csharp
await EnsureUniqueNhanVienAsync(input.MaNhanVien, input.Email, input.Id);
```

Tham số `ignoreId` giúp bỏ qua chính nhân viên đang sửa. Nhờ đó:

- Nếu giữ nguyên email cũ của chính mình thì không bị báo trùng.
- Nếu đổi sang email của nhân viên khác thì bị báo trùng.

Route:

```http
PUT /api/nhan-viens
```

Body mẫu:

```json
{
  "id": 1,
  "tenNhanVien": "Nguyễn Đức Mạnh Updated",
  "maNhanVien": "NV001",
  "email": "manh@example.com"
}
```

### Câu 4

API xóa nhân viên nhận dữ liệu như thế nào?

### Đáp án

API xóa nhân viên dùng:

```http
DELETE /api/nhan-viens
```

Body:

```json
{
  "id": 1
}
```

DTO:

```text
Dtos/NhanViens/NhanVienDeleteDto0210668De1.cs
```

Validation:

```csharp
[Required(ErrorMessage = "Id nhân viên là bắt buộc.")]
[Range(1, int.MaxValue, ErrorMessage = "Id nhân viên phải lớn hơn 0.")]
public int Id { get; set; }
```

Nếu không tìm thấy nhân viên, service ném:

```csharp
throw new UserFriendlyException0210668De1(MessageConstants0210668De1.NotFoundNhanVien);
```

Middleware trả HTTP `404 Not Found`.

## Bộ 6 - Phân trang và tìm kiếm

### Câu 1

API xem danh sách nhân viên có phân trang là API nào?

### Đáp án

Route:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=NV
```

Controller:

```csharp
[HttpGet]
public async Task<IActionResult> GetPagedAsync([FromQuery] NhanVienFilterDto0210668De1 input)
```

DTO filter:

```text
Dtos/NhanViens/NhanVienFilterDto0210668De1.cs
```

Các tham số:

| Tham số | Ý nghĩa |
|---|---|
| `PageIndex` | Trang hiện tại, bắt đầu từ 1 |
| `PageSize` | Số bản ghi mỗi trang |
| `Keyword` | Từ khóa lọc gần đúng theo tên hoặc mã nhân viên |

### Câu 2

Logic phân trang được viết bằng LINQ như thế nào?

### Đáp án

Trong `NhanVienService0210668De1.GetPagedAsync()`:

```csharp
var items = await query
    .OrderBy(item => item.TenNhanVien)
    .ThenBy(item => item.MaNhanVien)
    .Skip((input.PageIndex - 1) * input.PageSize)
    .Take(input.PageSize)
    .Select(item => new NhanVienDto0210668De1
    {
        Id = item.Id,
        TenNhanVien = item.TenNhanVien,
        MaNhanVien = item.MaNhanVien,
        Email = item.Email
    })
    .ToListAsync();
```

Ý nghĩa:

- `OrderBy`: sắp xếp ổn định.
- `Skip`: bỏ qua các bản ghi của trang trước.
- `Take`: lấy số bản ghi theo `PageSize`.
- `Select`: map entity sang DTO ngay trong query.

### Câu 3

Keyword lọc theo những trường nào?

### Đáp án

Keyword lọc gần đúng theo:

- `TenNhanVien`
- `MaNhanVien`

Code:

```csharp
query = query.Where(item =>
    item.TenNhanVien.Contains(input.Keyword) ||
    item.MaNhanVien.Contains(input.Keyword));
```

Nếu gọi:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=Manh
```

API sẽ tìm nhân viên có tên hoặc mã chứa `Manh`.

## Bộ 7 - API Dự án và Phân công

### Câu 1

Vì sao project có thêm API Dự án và Phân công trong khi đề chỉ nhấn mạnh CRUD Nhân viên?

### Đáp án

Đề yêu cầu API:

```text
Liệt kê danh sách những dự án mà một nhân viên dành nhiều giờ làm việc nhất.
```

Muốn kiểm tra API này cần có dữ liệu dự án và phân công. Vì vậy project thêm API phụ:

```http
POST /api/du-ans
GET /api/du-ans

POST /api/phan-congs
GET /api/phan-congs
```

Các API này giúp nhập dữ liệu demo/test mà không cần insert SQL thủ công.

### Câu 2

API tạo dự án kiểm tra trùng những gì?

### Đáp án

API tạo dự án kiểm tra:

- Trùng tên dự án.
- Trùng mã dự án.

Trong `DuAnService0210668De1.CreateAsync()`:

```csharp
var duplicatedTenDuAn = await _dbContext.DuAns.AnyAsync(item => item.TenDuAn == input.TenDuAn);
```

```csharp
var duplicatedMaDuAn = await _dbContext.DuAns.AnyAsync(item => item.MaDuAn == input.MaDuAn);
```

Nếu trùng thì ném `UserFriendlyException0210668De1`.

### Câu 3

API phân công xử lý thế nào nếu cùng một nhân viên được phân công lại vào cùng một dự án?

### Đáp án

Service tìm phân công cũ theo cặp:

```csharp
NhanVienId
DuAnId
```

Nếu chưa có thì thêm mới. Nếu đã có thì cập nhật `SoGioLamViec`.

Code:

```csharp
var phanCong = await _dbContext.PhanCongs
    .FirstOrDefaultAsync(item => item.NhanVienId == input.NhanVienId && item.DuAnId == input.DuAnId);

if (phanCong is null)
{
    phanCong = new PhanCong0210668De1
    {
        NhanVienId = input.NhanVienId,
        DuAnId = input.DuAnId,
        SoGioLamViec = input.SoGioLamViec
    };
    _dbContext.PhanCongs.Add(phanCong);
}
else
{
    phanCong.SoGioLamViec = input.SoGioLamViec;
}
```

Trong database cũng có unique index:

```csharp
entity.HasIndex(phanCong => new { phanCong.NhanVienId, phanCong.DuAnId }).IsUnique();
```

## Bộ 8 - API dự án theo số giờ làm việc

### Câu 1

API nào trả danh sách dự án mà nhân viên dành nhiều giờ làm việc nhất?

### Đáp án

Route:

```http
GET /api/nhan-viens/{id}/du-ans-theo-so-gio
```

Ví dụ:

```http
GET /api/nhan-viens/1/du-ans-theo-so-gio
```

Controller:

```csharp
[HttpGet("{id:int}/du-ans-theo-so-gio")]
public async Task<IActionResult> GetDuAnsTheoSoGioNhieuNhatAsync(int id)
```

### Câu 2

Đầu ra của API này gồm những trường nào?

### Đáp án

Đầu ra dùng DTO:

```text
Dtos/DuAns/DuAnTheoSoGioDto0210668De1.cs
```

Các trường:

```csharp
public string TenDuAn { get; set; } = string.Empty;
public string MaDuAn { get; set; } = string.Empty;
public int SoGioLamViec { get; set; }
```

Response mẫu:

```json
{
  "isSuccess": true,
  "data": [
    {
      "tenDuAn": "Dự án A",
      "maDuAn": "DA001",
      "soGioLamViec": 90
    },
    {
      "tenDuAn": "Dự án B",
      "maDuAn": "DA002",
      "soGioLamViec": 35
    }
  ],
  "message": "Thao tác thành công.",
  "code": 200
}
```

### Câu 3

Logic sắp xếp giảm dần theo số giờ được viết ở đâu?

### Đáp án

Trong `NhanVienService0210668De1.GetDuAnsTheoSoGioNhieuNhatAsync()`:

```csharp
return await _dbContext.PhanCongs
    .AsNoTracking()
    .Where(item => item.NhanVienId == nhanVienId)
    .OrderByDescending(item => item.SoGioLamViec)
    .ThenBy(item => item.DuAn!.TenDuAn)
    .Select(item => new DuAnTheoSoGioDto0210668De1
    {
        TenDuAn = item.DuAn!.TenDuAn,
        MaDuAn = item.DuAn.MaDuAn,
        SoGioLamViec = item.SoGioLamViec
    })
    .ToListAsync();
```

Dòng quan trọng:

```csharp
OrderByDescending(item => item.SoGioLamViec)
```

## Bộ 9 - Response chuẩn và exception

### Câu 1

Response chuẩn của mọi API có dạng nào?

### Đáp án

Response chuẩn:

```json
{
  "isSuccess": true,
  "data": {},
  "message": "Thao tác thành công.",
  "code": 200
}
```

Class response:

```text
Utils/ApiResponse0210668De1.cs
```

Các thuộc tính:

```csharp
public bool IsSuccess { get; set; }
public TData? Data { get; set; }
public string Message { get; set; } = string.Empty;
public int Code { get; set; }
```

### Câu 2

`UserFriendlyException0210668De1` được định nghĩa như thế nào?

### Đáp án

File:

```text
Exceptions/UserFriendlyException0210668De1.cs
```

Code:

```csharp
public class UserFriendlyException0210668De1 : Exception
{
    public UserFriendlyException0210668De1(string message) : base(message)
    {
    }
}
```

Service dùng exception này để báo lỗi nghiệp vụ như:

- Nhân viên không tồn tại.
- Mã nhân viên đã tồn tại.
- Email đã tồn tại.
- Dự án không tồn tại.
- Tên dự án đã tồn tại.

### Câu 3

Exception được bắt ở đâu để trả response thống nhất?

### Đáp án

Exception được bắt trong middleware:

```text
Utils/ExceptionMiddleware0210668De1.cs
```

Đăng ký trong `Program.cs`:

```csharp
app.UseMiddleware<ExceptionMiddleware0210668De1>();
```

Nếu là `UserFriendlyException0210668De1`, middleware trả lỗi nghiệp vụ.

Nếu là lỗi không xác định, middleware log lỗi và trả:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Đã xảy ra lỗi hệ thống.",
  "code": 500
}
```

## Bộ 10 - Câu hỏi sửa code thường gặp

### Câu 1

Nếu muốn thêm trường `SoDienThoai` cho Nhân viên thì cần sửa những file nào?

### Đáp án

Cần sửa:

| File | Nội dung |
|---|---|
| `Entities/NhanVien0210668De1.cs` | Thêm property `SoDienThoai` |
| `DbContexts/AppDbContext0210668De1.cs` | Cấu hình độ dài, required nếu cần |
| `Dtos/NhanViens/NhanVienCreateDto0210668De1.cs` | Thêm field nhận từ request |
| `Dtos/NhanViens/NhanVienUpdateDto0210668De1.cs` | Thêm field cập nhật |
| `Dtos/NhanViens/NhanVienDto0210668De1.cs` | Thêm field trả về |
| `Services/Implements/NhanVienService0210668De1.cs` | Map DTO sang entity và entity sang DTO |
| `Migrations` | Tạo migration mới |

Lệnh tạo migration:

```powershell
dotnet ef migrations add AddSoDienThoaiToNhanVien0210668De1
dotnet ef database update
```

### Câu 2

Nếu muốn đổi route nhân viên từ `/api/nhan-viens` thành `/api/v1/nhan-viens` thì sửa ở đâu?

### Đáp án

Sửa trong controller:

```text
Controllers/NhanVienController0210668De1.cs
```

Từ:

```csharp
[Route("api/nhan-viens")]
```

Thành:

```csharp
[Route("api/v1/nhan-viens")]
```

Sau đó cần cập nhật:

- Swagger/cURL/Postman test.
- Frontend nếu có gọi API.
- Tài liệu hướng dẫn chạy.

### Câu 3

Nếu muốn PageSize mặc định là 20 thay vì 10 thì sửa ở đâu?

### Đáp án

Sửa trong DTO filter:

```text
Dtos/NhanViens/NhanVienFilterDto0210668De1.cs
```

Từ:

```csharp
public int PageSize { get; set; } = 10;
```

Thành:

```csharp
public int PageSize { get; set; } = 20;
```

Nếu client truyền `PageSize` trên query string thì giá trị client truyền sẽ được ưu tiên.

## Bộ 11 - Câu hỏi kiểm tra chạy project

### Câu 1

Các bước chạy project là gì?

### Đáp án

Mở terminal tại thư mục:

```powershell
cd C:\Users\Admin\Downloads\kiem_tra\NguyenDucManh0210668
```

Build:

```powershell
dotnet build
```

Cập nhật database:

```powershell
dotnet ef database update
```

Chạy API:

```powershell
dotnet run
```

Mở Swagger:

```text
http://localhost:5188/swagger
```

### Câu 2

Nếu `dotnet ef database update` lỗi không kết nối được MySQL thì kiểm tra gì?

### Đáp án

Kiểm tra theo thứ tự:

1. Service MySQL có chạy không.
2. Port có đúng `3306` không.
3. User/password có đúng không.
4. Connection string trong `appsettings.json`.
5. Package provider MySQL có đúng không.

Kiểm tra MySQL service:

```powershell
Get-Service MySQL80
```

Kiểm tra đăng nhập:

```powershell
& "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" --user=root --password=0000 --host=localhost --port=3306 -e "SELECT VERSION();"
```

### Câu 3

Nếu API trả lỗi trùng email thì kiểm tra ở tầng nào?

### Đáp án

Kiểm tra ở service:

```text
Services/Implements/NhanVienService0210668De1.cs
```

Cụ thể là hàm:

```csharp
EnsureUniqueNhanVienAsync
```

Ngoài ra database cũng có unique index:

```csharp
entity.HasIndex(nhanVien => nhanVien.Email).IsUnique();
```

Như vậy project có hai lớp bảo vệ:

- Service kiểm tra trước để trả lỗi thân thiện.
- Database unique index đảm bảo toàn vẹn dữ liệu.

## Tóm tắt công thức trả lời khi vấn đáp

Khi thầy hỏi một API hoặc một logic bất kỳ, trả lời theo mẫu:

```text
1. API nằm ở controller nào, route nào.
2. Controller gọi service interface nào.
3. Service implementation xử lý nghiệp vụ gì.
4. Service dùng LINQ/EF Core query nào để đọc/ghi database.
5. Entity/DTO nào tham gia.
6. Nếu có lỗi thì ném UserFriendlyException0210668De1.
7. ExceptionMiddleware0210668De1 bắt lỗi và trả ApiResponse0210668De1.
```

Ví dụ ngắn:

```text
API thêm nhân viên nằm trong NhanVienController0210668De1, route POST /api/nhan-viens.
Controller nhận NhanVienCreateDto0210668De1 rồi gọi INhanVienService0210668De1.CreateAsync.
Service kiểm tra trùng mã nhân viên và email bằng AnyAsync.
Nếu trùng thì ném UserFriendlyException0210668De1.
Nếu hợp lệ thì tạo NhanVien0210668De1, add vào DbContext và SaveChangesAsync.
Cuối cùng trả NhanVienDto0210668De1 trong ApiResponse0210668De1.
```

