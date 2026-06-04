# Giải thích các luồng xử lý Đề 1 - Nguyễn Đức Mạnh 0210668

Project: `NguyenDucManh0210668`  
Công nghệ: ASP.NET Core Web API, Entity Framework Core, MySQL  
Bài toán: Quản lý Nhân viên, Dự án và Phân công theo quan hệ n-n.

## 1. Luồng tổng quan của một request API

Mọi request đi qua luồng tổng quát:

```text
Client / Swagger
-> Controller
-> Service Interface
-> Service Implementation
-> AppDbContext0210668De1
-> Entity Framework Core
-> MySQL
-> Trả dữ liệu về Service
-> Map Entity sang DTO
-> Controller bọc ApiResponse0210668De1
-> Client nhận JSON
```

Ví dụ với API thêm nhân viên:

```text
POST /api/nhan-viens
-> NhanVienController0210668De1.CreateAsync()
-> INhanVienService0210668De1.CreateAsync()
-> NhanVienService0210668De1.CreateAsync()
-> _dbContext.NhanViens.Add()
-> _dbContext.SaveChangesAsync()
-> Trả NhanVienDto0210668De1
-> ApiResponse0210668De1<NhanVienDto0210668De1>
```

## 2. Luồng khởi động ứng dụng

File chính:

```text
Program.cs
```

Khi chạy:

```powershell
dotnet run
```

Ứng dụng thực hiện các bước:

```text
1. Tạo WebApplicationBuilder
2. Đăng ký Controller
3. Đăng ký Swagger
4. Đăng ký DbContext dùng MySQL
5. Đăng ký Dependency Injection cho Service
6. Cấu hình response lỗi validation
7. Build app
8. Bật Swagger trong môi trường Development
9. Bật middleware xử lý exception
10. Map Controllers
11. Chạy web server
```

Code quan trọng:

```csharp
builder.Services.AddDbContext<AppDbContext0210668De1>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0)));
});
```

Đoạn trên nói rằng project dùng MySQL làm database.

Đăng ký DI:

```csharp
builder.Services.AddScoped<INhanVienService0210668De1, NhanVienService0210668De1>();
builder.Services.AddScoped<IDuAnService0210668De1, DuAnService0210668De1>();
builder.Services.AddScoped<IPhanCongService0210668De1, PhanCongService0210668De1>();
```

## 3. Luồng tạo database bằng migration

Lệnh:

```powershell
dotnet ef database update
```

Luồng xử lý:

```text
dotnet ef database update
-> Build project
-> Đọc AppDbContext0210668De1
-> Đọc migration trong thư mục Migrations
-> Kết nối MySQL bằng connection string
-> Tạo database nguyenducmanh0210668de1 nếu chưa có
-> Tạo bảng NhanViens
-> Tạo bảng DuAns
-> Tạo bảng PhanCongs
-> Tạo khóa chính, khóa ngoại, unique index
-> Ghi migration đã chạy vào __EFMigrationsHistory
```

Connection string:

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=nguyenducmanh0210668de1;User=root;Password=0000;AllowPublicKeyRetrieval=True;SslMode=None"
```

Kết quả database:

```text
NhanViens
DuAns
PhanCongs
__EFMigrationsHistory
```

## 4. Luồng mô hình hóa quan hệ n-n

Đề bài yêu cầu quan hệ n-n:

```text
Một nhân viên có thể tham gia nhiều dự án.
Một dự án có thể có nhiều nhân viên.
```

Không lưu trực tiếp danh sách dự án trong nhân viên hoặc danh sách nhân viên trong dự án. Thay vào đó tạo bảng trung gian:

```text
PhanCongs
```

Luồng quan hệ:

```text
NhanViens 1 - n PhanCongs n - 1 DuAns
```

Trong `PhanCongs` có:

```text
Id
NhanVienId
DuAnId
SoGioLamViec
```

Ý nghĩa:

```text
NhanVienId = nhân viên nào
DuAnId = dự án nào
SoGioLamViec = làm bao nhiêu giờ trong dự án đó
```

Fluent API cấu hình:

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

Unique index:

```csharp
entity.HasIndex(phanCong => new { phanCong.NhanVienId, phanCong.DuAnId }).IsUnique();
```

Ý nghĩa: một nhân viên không được có hai bản ghi phân công trùng cho cùng một dự án.

## 5. Luồng thêm nhân viên

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "tenNhanVien": "Nguyễn Đức Mạnh",
  "maNhanVien": "NV001",
  "email": "manh001@example.com"
}
```

Luồng xử lý:

```text
Client gửi JSON
-> ASP.NET Core map JSON sang NhanVienCreateDto0210668De1
-> DTO tự trim các trường string
-> Model validation chạy annotation Required, StringLength, EmailAddress
-> NhanVienController0210668De1.CreateAsync()
-> NhanVienService0210668De1.CreateAsync()
-> Kiểm tra mã nhân viên đã tồn tại chưa
-> Kiểm tra email đã tồn tại chưa
-> Tạo entity NhanVien0210668De1
-> Add vào _dbContext.NhanViens
-> SaveChangesAsync ghi xuống MySQL
-> Map entity sang NhanVienDto0210668De1
-> Controller trả ApiResponse0210668De1
```

Response:

```json
{
  "isSuccess": true,
  "data": {
    "id": 1,
    "tenNhanVien": "Nguyễn Đức Mạnh",
    "maNhanVien": "NV001",
    "email": "manh001@example.com"
  },
  "message": "Thêm mới thành công.",
  "code": 200
}
```

## 6. Luồng validate DTO

Ví dụ DTO tạo nhân viên:

```text
NhanVienCreateDto0210668De1
```

Các annotation:

```csharp
[Required]
[StringLength]
[EmailAddress]
```

Luồng validate:

```text
Client gửi request
-> ASP.NET Core bind JSON vào DTO
-> Setter DTO trim string
-> ApiController tự kiểm tra ModelState
-> Nếu hợp lệ: chạy vào action controller
-> Nếu không hợp lệ: trả 400 Bad Request
```

Project có cấu hình response validate trong `Program.cs`:

```csharp
options.InvalidModelStateResponseFactory = context =>
{
    var errors = context.ModelState
        .Where(modelState => modelState.Value?.Errors.Count > 0)
        .SelectMany(modelState => modelState.Value!.Errors.Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
            ? "Dữ liệu không hợp lệ."
            : error.ErrorMessage))
        .ToList();

    var response = ApiResponse0210668De1<object>.Fail(
        string.Join(" ", errors),
        StatusCodes.Status400BadRequest);

    return new BadRequestObjectResult(response);
};
```

Nếu thiếu email:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Email là bắt buộc.",
  "code": 400
}
```

## 7. Luồng trim string trong DTO

Yêu cầu đề bài: toàn bộ trường string trong DTO phải xử lý `.Trim()`.

Ví dụ:

```csharp
private string _tenNhanVien = string.Empty;

public string TenNhanVien
{
    get => _tenNhanVien;
    set => _tenNhanVien = value?.Trim() ?? string.Empty;
}
```

Luồng:

```text
Client gửi "  Nguyễn Đức Mạnh  "
-> JSON binder gọi setter TenNhanVien
-> Setter chạy value.Trim()
-> DTO lưu "Nguyễn Đức Mạnh"
-> Service nhận dữ liệu đã sạch khoảng trắng
-> Database lưu dữ liệu đã trim
```

Lợi ích:

- Tránh lỗi trùng dữ liệu do khoảng trắng.
- Dữ liệu lưu xuống database sạch hơn.
- Khi tìm kiếm hoặc hiển thị không bị dư khoảng trắng.

## 8. Luồng kiểm tra trùng mã nhân viên và email

Trong `NhanVienService0210668De1`, trước khi thêm hoặc sửa nhân viên đều gọi:

```csharp
await EnsureUniqueNhanVienAsync(input.MaNhanVien, input.Email, ignoreId);
```

Luồng khi thêm:

```text
Nhận MaNhanVien và Email
-> Query NhanViens bằng AnyAsync để kiểm tra MaNhanVien
-> Nếu có bản ghi trùng: ném UserFriendlyException0210668De1
-> Query NhanViens bằng AnyAsync để kiểm tra Email
-> Nếu có bản ghi trùng: ném UserFriendlyException0210668De1
-> Nếu không trùng: cho phép thêm mới
```

Khi thêm trùng mã:

```csharp
throw new UserFriendlyException0210668De1("Mã nhân viên đã tồn tại.");
```

Response:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Mã nhân viên đã tồn tại.",
  "code": 400
}
```

Khi sửa, truyền `ignoreId` để bỏ qua chính nhân viên đang sửa:

```text
Nhân viên id = 1 có email manh@example.com
Sửa lại tên nhưng giữ nguyên email
-> Không báo trùng vì bỏ qua id = 1
```

## 9. Luồng cập nhật nhân viên

Endpoint:

```http
PUT /api/nhan-viens
```

Body:

```json
{
  "id": 1,
  "tenNhanVien": "Nguyễn Đức Mạnh Updated",
  "maNhanVien": "NV001",
  "email": "manh001@example.com"
}
```

Luồng:

```text
Client gửi JSON
-> Bind vào NhanVienUpdateDto0210668De1
-> Validate Id, tên, mã, email
-> Controller gọi service update
-> Service tìm nhân viên theo Id
-> Nếu không thấy: ném UserFriendlyException0210668De1
-> Kiểm tra trùng mã và email, bỏ qua chính Id đang sửa
-> Gán lại TenNhanVien, MaNhanVien, Email
-> SaveChangesAsync
-> Trả DTO mới
```

Nếu không tìm thấy:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Không tìm thấy nhân viên.",
  "code": 404
}
```

## 10. Luồng xóa nhân viên

Endpoint:

```http
DELETE /api/nhan-viens
```

Body:

```json
{
  "id": 1
}
```

Luồng:

```text
Client gửi Id
-> Bind vào NhanVienDeleteDto0210668De1
-> Validate Id > 0
-> Controller gọi service delete
-> Service tìm nhân viên theo Id
-> Nếu không thấy: ném UserFriendlyException0210668De1
-> Remove nhân viên khỏi DbContext
-> SaveChangesAsync
-> MySQL xóa nhân viên
-> Nếu có phân công liên quan, FK cascade xóa phân công
-> Trả response thành công
```

Response:

```json
{
  "isSuccess": true,
  "data": null,
  "message": "Xóa thành công.",
  "code": 200
}
```

## 11. Luồng phân trang và tìm kiếm nhân viên

Endpoint:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=NV
```

Luồng:

```text
Client gửi PageIndex, PageSize, Keyword qua query string
-> ASP.NET Core bind vào NhanVienFilterDto0210668De1
-> DTO trim Keyword
-> Validate PageIndex >= 1, PageSize từ 1 đến 100
-> Controller gọi service GetPagedAsync
-> Service tạo query AsNoTracking
-> Nếu có Keyword: lọc theo TenNhanVien hoặc MaNhanVien
-> CountAsync để lấy tổng số bản ghi
-> OrderBy, Skip, Take để phân trang ở database
-> Select sang NhanVienDto0210668De1
-> ToListAsync
-> Trả PagedResult0210668De1
```

Code chính:

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

Điểm tối ưu:

- Không tải toàn bộ database lên RAM.
- `Skip` và `Take` được EF Core dịch sang SQL.
- `AsNoTracking` giúp query đọc nhanh hơn.
- `Select` chỉ lấy các cột cần trả về.

## 12. Luồng tạo dự án

Endpoint:

```http
POST /api/du-ans
```

Body:

```json
{
  "tenDuAn": "Dự án Server",
  "maDuAn": "DA001"
}
```

Luồng:

```text
Client gửi JSON
-> DuAnController0210668De1.CreateAsync()
-> DuAnService0210668De1.CreateAsync()
-> Kiểm tra trùng TenDuAn
-> Kiểm tra trùng MaDuAn
-> Tạo DuAn0210668De1
-> Add vào DbContext
-> SaveChangesAsync
-> Trả DuAnDto0210668De1
```

Nếu trùng tên:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Tên dự án đã tồn tại.",
  "code": 400
}
```

## 13. Luồng tạo hoặc cập nhật phân công

Endpoint:

```http
POST /api/phan-congs
```

Body:

```json
{
  "nhanVienId": 1,
  "duAnId": 1,
  "soGioLamViec": 80
}
```

Luồng:

```text
Client gửi NhanVienId, DuAnId, SoGioLamViec
-> Bind vào PhanCongCreateOrUpdateDto0210668De1
-> Validate Id > 0 và SoGioLamViec từ 1 đến 10000
-> Controller gọi PhanCongService0210668De1
-> Kiểm tra nhân viên có tồn tại không
-> Kiểm tra dự án có tồn tại không
-> Tìm phân công theo cặp NhanVienId + DuAnId
-> Nếu chưa có: tạo mới
-> Nếu đã có: cập nhật SoGioLamViec
-> SaveChangesAsync
-> Query lại phân công vừa lưu kèm tên nhân viên, tên dự án
-> Trả PhanCongDto0210668De1
```

Lý do dùng tạo hoặc cập nhật:

```text
Cùng một nhân viên và cùng một dự án chỉ nên có một phân công.
Nếu gửi lại cùng cặp NhanVienId + DuAnId thì cập nhật số giờ thay vì tạo bản ghi trùng.
```

## 14. Luồng liệt kê dự án theo số giờ giảm dần

Endpoint:

```http
GET /api/nhan-viens/{id}/du-ans-theo-so-gio
```

Ví dụ:

```http
GET /api/nhan-viens/1/du-ans-theo-so-gio
```

Luồng:

```text
Client truyền id nhân viên trên route
-> NhanVienController0210668De1.GetDuAnsTheoSoGioNhieuNhatAsync(id)
-> Service kiểm tra nhân viên có tồn tại không
-> Nếu không tồn tại: ném UserFriendlyException0210668De1
-> Query bảng PhanCongs
-> Lọc theo NhanVienId
-> Sắp xếp giảm dần theo SoGioLamViec
-> Select sang DuAnTheoSoGioDto0210668De1
-> Trả danh sách dự án
```

Code chính:

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

Điểm quan trọng:

- Không dùng vòng lặp để query từng dự án.
- Không bị lỗi N+1.
- EF Core sinh query SQL join/projection tối ưu.
- Chỉ trả đúng 3 trường đề yêu cầu: tên dự án, mã dự án, số giờ làm việc.

## 15. Luồng xử lý exception

Middleware:

```text
ExceptionMiddleware0210668De1
```

Đăng ký:

```csharp
app.UseMiddleware<ExceptionMiddleware0210668De1>();
```

Luồng:

```text
Request đi vào pipeline
-> Middleware gọi _next(context)
-> Controller/Service xử lý
-> Nếu không lỗi: trả response bình thường
-> Nếu UserFriendlyException0210668De1: middleware bắt và trả lỗi thân thiện
-> Nếu Exception khác: middleware log lỗi và trả 500
```

Lỗi nghiệp vụ:

```csharp
catch (UserFriendlyException0210668De1 exception)
{
    var statusCode = GetUserFriendlyStatusCode(exception.Message);
    await WriteErrorResponseAsync(context, exception.Message, statusCode);
}
```

Lỗi hệ thống:

```csharp
catch (Exception exception)
{
    _logger.LogError(exception, "Đã xảy ra lỗi hệ thống.");
    await WriteErrorResponseAsync(context, "Đã xảy ra lỗi hệ thống.", StatusCodes.Status500InternalServerError);
}
```

## 16. Luồng response chuẩn

Class:

```text
ApiResponse0210668De1<TData>
```

Cấu trúc:

```csharp
public bool IsSuccess { get; set; }
public TData? Data { get; set; }
public string Message { get; set; } = string.Empty;
public int Code { get; set; }
```

Luồng thành công:

```text
Controller nhận result từ service
-> Gọi ApiResponse0210668De1<T>.Success()
-> Trả Ok(response)
```

Ví dụ:

```csharp
return Ok(ApiResponse0210668De1<NhanVienDto0210668De1>.Success(
    result,
    MessageConstants0210668De1.Created));
```

Luồng lỗi:

```text
Service ném UserFriendlyException0210668De1
-> ExceptionMiddleware0210668De1 bắt
-> Gọi ApiResponse0210668De1<object>.Fail()
-> Trả JSON lỗi
```

## 17. Luồng tránh lỗi N+1

N+1 là lỗi khi:

```text
Query lấy danh sách phân công
-> Sau đó loop từng phân công
-> Mỗi vòng loop lại query thêm dự án
```

Ví dụ không tốt:

```text
Lấy 100 phân công
-> Query 1 lần danh sách phân công
-> Query thêm 100 lần để lấy dự án
-> Tổng 101 query
```

Project tránh N+1 bằng cách dùng projection:

```csharp
.Select(item => new DuAnTheoSoGioDto0210668De1
{
    TenDuAn = item.DuAn!.TenDuAn,
    MaDuAn = item.DuAn.MaDuAn,
    SoGioLamViec = item.SoGioLamViec
})
```

EF Core sẽ dịch thành query SQL phù hợp để lấy dữ liệu cần thiết trong một luồng truy vấn.

## 18. Luồng kiểm tra bài trước khi nộp

Chạy:

```powershell
dotnet build
```

Kỳ vọng:

```text
Build succeeded.
0 Warning(s)
0 Error(s)
```

Kiểm tra migration:

```powershell
dotnet ef migrations list
```

Kỳ vọng:

```text
20260603030402_InitialCreate0210668De1
```

Cập nhật database:

```powershell
dotnet ef database update
```

Kỳ vọng:

```text
No migrations were applied. The database is already up to date.
```

Chạy app:

```powershell
dotnet run
```

Mở:

```text
http://localhost:5188/swagger
```

Test nhanh:

```text
1. POST /api/nhan-viens
2. POST /api/du-ans
3. POST /api/phan-congs
4. GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=NV
5. GET /api/nhan-viens/{id}/du-ans-theo-so-gio
```

## 19. Tóm tắt các luồng cần nhớ khi vấn đáp

### Thêm nhân viên

```text
Controller -> Service -> Check trùng -> Add Entity -> SaveChangesAsync -> DTO -> ApiResponse
```

### Sửa nhân viên

```text
Controller -> Service -> Tìm theo Id -> Check trùng bỏ qua Id hiện tại -> Update field -> SaveChangesAsync
```

### Xóa nhân viên

```text
Controller -> Service -> Tìm theo Id -> Remove -> SaveChangesAsync -> Cascade xóa phân công liên quan
```

### Phân trang

```text
Query DTO -> AsNoTracking -> Where Keyword -> CountAsync -> OrderBy -> Skip -> Take -> Select DTO
```

### Dự án theo số giờ

```text
Route id nhân viên -> Check nhân viên tồn tại -> Query PhanCongs -> OrderByDescending SoGioLamViec -> Select DTO
```

### Exception

```text
Service throw UserFriendlyException0210668De1 -> Middleware catch -> ApiResponse Fail
```

