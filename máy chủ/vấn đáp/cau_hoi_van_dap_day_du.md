# 📋 DANH SÁCH CÂU HỎI VẤN ĐÁP
## Môn: Phát Triển Ứng Dụng Phía Server (ASP.NET Core Web API)

> **Hướng dẫn:** Đây là các câu hỏi thường gặp khi vấn đáp. Hãy đọc kỹ và chuẩn bị câu trả lời bằng lời nói tự nhiên, kết hợp chỉ trực tiếp vào code của mình.

---

## 🔵 PHẦN 1: CẤU TRÚC DỰ ÁN (Tổ chức code)

### Câu 1: Hãy mô tả cây thư mục của dự án em?
**Trả lời gợi ý:**
- `Properties` – chứa cấu hình chạy ứng dụng (launchSettings.json)
- `Constants` – chứa các hằng số dùng chung (thông báo lỗi, giá trị cố định)
- `Controllers` – chứa các API Controller, nhận request và trả response
- `DbContexts` – chứa AppDbContext là cầu nối giữa C# và database
- `Dtos` – chứa các class Data Transfer Object, chia theo nghiệp vụ (Create, Update, Filter, Result...)
- `Entities` – chứa các class ánh xạ với bảng trong database
- `Exceptions` – chứa class `UserFriendlyException` và middleware xử lý lỗi
- `Migrations` – tự sinh ra khi chạy lệnh `dotnet ef migrations add`
- `Services` → `Interfaces` + `Implements` – chứa logic nghiệp vụ
- `Utils` – chứa các hàm tiện ích dùng chung
- `Program.cs` – điểm khởi đầu, đăng ký DI và middleware

---

### Câu 2: Tại sao lại phải tách thư mục như vậy, không để tất cả trong một file được không?
**Trả lời gợi ý:**
- Áp dụng nguyên tắc **Separation of Concerns** – mỗi thành phần chỉ làm một việc
- Dễ bảo trì, dễ tìm kiếm, dễ mở rộng khi thêm tính năng mới
- Khi có lỗi ở tầng nào thì chỉ cần sửa đúng tầng đó, không ảnh hưởng tầng khác

---

### Câu 3: Coding convention trong dự án là gì?
**Trả lời gợi ý:**
- `PascalCase` cho tên class, interface, property, method → ví dụ: `EnterpriseService`, `GetPagedAsync`
- `camelCase` cho biến cục bộ → ví dụ: `var enterprise = ...`
- `_camelCase` cho field private → ví dụ: `private readonly IEnterpriseService _enterpriseService`
- Tên class theo dạng: `<TênClass><MãSV><ĐềSố>` → ví dụ: `ShipperDto0210668De1`

---

## 🟢 PHẦN 2: ENTITY & DATABASE (EF Core + Code First)

### Câu 4: Entity là gì? Nó có vai trò gì trong dự án?
**Trả lời gợi ý:**
- Entity là class C# ánh xạ (map) với một bảng trong database
- Mỗi property trong entity tương ứng với một cột trong bảng
- EF Core dùng các entity này để tự động tạo bảng thông qua Migration
- Ví dụ: class `Enterprise` → bảng `Enterprises` trong DB

---

### Câu 5: Khóa chính (Primary Key) được định nghĩa thế nào?
**Trả lời gợi ý:**
- Là property tên `Id` kiểu `int` → EF Core tự nhận ra và tạo IDENTITY (tự tăng)
- Hoặc có thể dùng attribute `[Key]` nếu tên khác `Id`
- Ví dụ: `public int Id { get; set; }` trong class `Enterprise0210668De1`

---

### Câu 6: Quan hệ n-n được thiết kế thế nào trong dự án này?
**Trả lời gợi ý:**
- Bài toán có Doanh nghiệp và Sản phẩm quan hệ nhiều-nhiều (n-n)
- Tạo bảng trung gian `EnterpriseProduct` với các FK trỏ về 2 bảng chính
- Bảng trung gian thêm trường `Quantity` (số lượng) theo yêu cầu đề
- Trong entity: `Enterprise` có `ICollection<EnterpriseProduct>`, và `EnterpriseProduct` có 2 navigation property

---

### Câu 7: Migration là gì? Em chạy lệnh gì để tạo bảng trong database?
**Trả lời gợi ý:**
- Migration là lịch sử thay đổi cấu trúc database, được EF Core quản lý
- Lệnh thêm migration: `dotnet ef migrations add TenMigration`
- Lệnh cập nhật database: `dotnet ef database update`
- Code First nghĩa là viết code Entity trước, sau đó mới tạo bảng trong DB (ngược với Database First)

---

### Câu 8: Connection String được cấu hình ở đâu?
**Trả lời gợi ý:**
- Trong file `appsettings.json`, mục `ConnectionStrings` → `DefaultConnection`
- Trong `Program.cs` đọc ra bằng: `builder.Configuration.GetConnectionString("DefaultConnection")`
- Truyền vào `DbContext` thông qua `options.UseSqlite(...)` hoặc `options.UseSqlServer(...)`

---

## 🟡 PHẦN 3: DTO & VALIDATION

### Câu 9: DTO là gì? Tại sao không dùng thẳng Entity?
**Trả lời gợi ý:**
- DTO = Data Transfer Object – lớp trung gian để truyền dữ liệu giữa Controller và Client
- **Lý do không dùng Entity trực tiếp:**
  - Entity có thể chứa field nhạy cảm (Id, ngày tạo) → không muốn lộ ra ngoài
  - Entity có navigation property → sẽ gây lỗi vòng lặp JSON (circular reference)
  - DTO giúp validate dữ liệu đầu vào trước khi chạm vào database
- Chia thành: `CreateDto`, `UpdateDto`, `FilterDto`, `ResultDto` theo từng chức năng

---

### Câu 10: Validation được thực hiện bằng cách nào?
**Trả lời gợi ý:**
- Dùng Data Annotation Attributes trên các property của DTO:
  - `[Required]` → bắt buộc nhập
  - `[MaxLength(100)]` → giới hạn độ dài chuỗi
  - `[MinLength(3)]` → độ dài tối thiểu
  - `[Range(0, int.MaxValue)]` → giá trị số trong khoảng
  - `[StringLength(50, MinimumLength = 2)]` → giới hạn min + max
- Khi dữ liệu sai → `ModelState.IsValid == false` → Controller trả `BadRequest(ModelState)`

---

### Câu 11: Tại sao các DTO phải trim chuỗi?
**Trả lời gợi ý:**
- Người dùng có thể vô tình nhập dấu cách thừa: `"  Nguyễn Văn A  "`
- Nếu không trim → tên "Nguyễn Văn A" và "  Nguyễn Văn A  " được coi là khác nhau → trùng lặp dữ liệu
- Xử lý bằng cách override setter hoặc gán lại trong service: `dto.Name = dto.Name.Trim()`
- Hoặc tạo custom setter trong DTO:
```csharp
private string _name = null!;
public string Name 
{ 
    get => _name; 
    set => _name = value?.Trim() ?? string.Empty; 
}
```

---

## 🟠 PHẦN 4: SERVICE & DEPENDENCY INJECTION

### Câu 12: Dependency Injection (DI) là gì? Em áp dụng nó thế nào?
**Trả lời gợi ý:**
- DI là pattern: thay vì class tự tạo các đối tượng phụ thuộc, nó **nhận** từ bên ngoài qua constructor
- **Lợi ích:** dễ test (mock), dễ thay thế implement, không bị phụ thuộc cứng
- Trong dự án:
  1. Định nghĩa Interface: `IEnterpriseService`
  2. Tạo class triển khai: `EnterpriseService : IEnterpriseService`
  3. Đăng ký trong `Program.cs`: `builder.Services.AddScoped<IEnterpriseService, EnterpriseService>()`
  4. Controller nhận qua constructor: `public EnterprisesController(IEnterpriseService service)`
- DI Container của ASP.NET Core tự inject đúng implement khi tạo Controller

---

### Câu 13: `AddScoped` khác `AddSingleton` và `AddTransient` thế nào?
**Trả lời gợi ý:**
- `AddSingleton` → tạo 1 instance duy nhất cho toàn bộ vòng đời ứng dụng
- `AddScoped` → tạo 1 instance cho mỗi HTTP Request (phù hợp với DbContext và Service)
- `AddTransient` → tạo instance mới mỗi khi được inject
- Dùng `AddScoped` cho Service vì mỗi request cần 1 connection database riêng

---

### Câu 14: Interface trong Services có tác dụng gì?
**Trả lời gợi ý:**
- Interface khai báo "hợp đồng" – danh sách tính năng mà service phải có
- Controller chỉ biết Interface, không biết class thật → dễ thay đổi implement sau này
- Đúng nguyên tắc SOLID: **D** – Dependency Inversion (phụ thuộc vào abstraction, không phụ thuộc vào detail)

---

### Câu 15: Tại sao không viết logic nghiệp vụ trong Controller?
**Trả lời gợi ý:**
- Controller chỉ nên làm: nhận request, validate đầu vào, gọi service, trả response
- Logic nghiệp vụ phức tạp để trong Service → dễ tái sử dụng, dễ test
- Nếu viết trong Controller: code phình to, khó maintain, không thể dùng lại ở nơi khác

---

## 🔴 PHẦN 5: CONTROLLER & API ENDPOINTS

### Câu 16: `IActionResult` là gì? Tại sao dùng nó làm return type?
**Trả lời gợi ý:**
- `IActionResult` là interface cho phép trả về nhiều loại HTTP response khác nhau
- Ví dụ: `Ok(data)` → 200, `BadRequest(...)` → 400, `NotFound()` → 404
- Nếu dùng kiểu cụ thể (như `string` hay `EnterpriseDto`) thì chỉ trả được 200 OK
- `IActionResult` linh hoạt hơn vì một action có thể trả nhiều mã HTTP tùy tình huống

---

### Câu 17: Giải thích các HTTP Method được dùng trong dự án?
**Trả lời gợi ý:**
- `[HttpPost]` → Tạo mới (Create) → nhận data từ `[FromBody]`
- `[HttpPut("{id}")]` → Cập nhật (Update) → nhận `id` từ URL, data từ `[FromBody]`
- `[HttpDelete("{id}")]` → Xóa → nhận `id` từ URL
- `[HttpGet("paged")]` → Lấy danh sách phân trang → nhận filter từ `[FromQuery]`
- `[HttpGet("{id}/most-imported-products")]` → Lấy sản phẩm nhập nhiều nhất

---

### Câu 18: `[FromBody]` và `[FromQuery]` khác nhau thế nào?
**Trả lời gợi ý:**
- `[FromBody]` → đọc dữ liệu từ **body** của HTTP request (thường dùng với POST, PUT)
  - Dữ liệu ở dạng JSON trong body
- `[FromQuery]` → đọc dữ liệu từ **query string** trên URL (thường dùng với GET)
  - Ví dụ: `GET /api/enterprises/paged?PageIndex=1&PageSize=10&Keyword=ABC`

---

### Câu 19: `ApiController` attribute có tác dụng gì?
**Trả lời gợi ý:**
- Tự động validate ModelState → không cần `if (!ModelState.IsValid)` trong mỗi action
- Tự động bind `[FromBody]` cho parameter phức tạp
- Trả về 400 Bad Request với thông tin lỗi validation rõ ràng khi dữ liệu không hợp lệ
- Yêu cầu khai báo route attribute (`[Route]`)

---

## 🟣 PHẦN 6: XỬ LÝ NGOẠI LỆ

### Câu 20: `UserFriendlyException` là gì? Tại sao cần nó?
**Trả lời gợi ý:**
- Là custom exception được tạo riêng cho dự án:
```csharp
public class UserFriendlyException : Exception
{
    public UserFriendlyException(string message) : base(message) { }
}
```
- Khi service phát hiện lỗi nghiệp vụ (trùng tên, không tìm thấy...) → `throw new UserFriendlyException("Thông báo lỗi")`
- Middleware `ExceptionMiddleware` bắt exception này và trả về **400 Bad Request** với message đó
- Các exception khác (lỗi hệ thống) trả về **500 Internal Server Error**

---

### Câu 21: Middleware xử lý exception hoạt động thế nào?
**Trả lời gợi ý:**
- Middleware là lớp xử lý request/response theo chuỗi pipeline trong ASP.NET Core
- `ExceptionMiddleware` bọc toàn bộ pipeline → bắt tất cả exception chưa được xử lý
- Khi bắt được:
  - Nếu là `UserFriendlyException` → trả 400 với message thân thiện
  - Nếu là exception khác → trả 500 với message chung chung
- Đăng ký trong `Program.cs`: `app.UseMiddleware<ExceptionMiddleware>()`

---

## ⚡ PHẦN 7: LINQ & TRUY VẤN DATABASE

### Câu 22: LINQ là gì? Em dùng nó ở đâu trong dự án?
**Trả lời gợi ý:**
- LINQ = Language Integrated Query – ngôn ngữ truy vấn tích hợp vào C#
- EF Core dịch LINQ sang SQL để truy vấn database
- Trong dự án dùng:
  - `AnyAsync(...)` → kiểm tra có tồn tại không (thay `EXISTS` trong SQL)
  - `Where(...)` → lọc dữ liệu
  - `OrderBy(...)` / `OrderByDescending(...)` → sắp xếp
  - `Skip(...).Take(...)` → phân trang
  - `Select(...)` → ánh xạ sang DTO
  - `CountAsync()` → đếm tổng số bản ghi
  - `MaxAsync(...)` → tìm giá trị lớn nhất

---

### Câu 23: Phân trang (Pagination) được thực hiện thế nào?
**Trả lời gợi ý:**
- Dùng 2 tham số: `PageIndex` (trang số mấy, bắt đầu từ 1) và `PageSize` (số item mỗi trang)
- Logic:
```csharp
var totalItems = await query.CountAsync();
var items = await query
    .Skip((PageIndex - 1) * PageSize)  // bỏ qua bao nhiêu item
    .Take(PageSize)                     // lấy bao nhiêu item
    .ToListAsync();
```
- Trả về object chứa: `TotalItems`, `PageIndex`, `PageSize`, `Items`

---

### Câu 24: API tìm sản phẩm nhập nhiều nhất hoạt động thế nào?
**Trả lời gợi ý:**
- B1: Kiểm tra enterprise có tồn tại không (nếu không → throw exception)
- B2: Tìm `maxQuantity` = số lượng lớn nhất trong bảng trung gian theo enterpriseId
- B3: Lấy tất cả sản phẩm có `Quantity == maxQuantity` của enterprise đó
- Dùng `MaxAsync((int?)ep.Quantity) ?? 0` để xử lý trường hợp không có sản phẩm nào (tránh exception)

---

### Câu 25: `AsQueryable()` có tác dụng gì?
**Trả lời gợi ý:**
- Trả về `IQueryable<T>` thay vì `IEnumerable<T>`
- `IQueryable` cho phép xây dựng câu truy vấn dần dần (chưa thực sự chạy SQL)
- Chỉ khi gọi `ToListAsync()`, `CountAsync()`, `AnyAsync()` → SQL mới được thực thi
- Lợi ích: có thể thêm `Where()`, `OrderBy()` vào sau mà không chạy nhiều query

---

## 🌐 PHẦN 8: CÁC KHÁI NIỆM TỔNG QUÁT

### Câu 26: RESTful API là gì?
**Trả lời gợi ý:**
- REST = Representational State Transfer – kiến trúc thiết kế API
- Nguyên tắc chính:
  - Sử dụng HTTP Method đúng ngữ nghĩa (GET đọc, POST tạo, PUT sửa, DELETE xóa)
  - URL đặt theo resource (danh từ số nhiều): `/api/enterprises`, không phải `/api/getEnterprise`
  - Stateless: mỗi request độc lập, server không lưu trạng thái client
  - Trả về HTTP status code phù hợp: 200, 201, 400, 404, 500...

---

### Câu 27: async/await trong C# là gì? Tại sao dùng nó?
**Trả lời gợi ý:**
- `async`/`await` cho phép viết code bất đồng bộ (asynchronous) theo kiểu đồng bộ (dễ đọc)
- Khi gọi database (I/O bound), thread không bị block → có thể xử lý request khác trong khi chờ
- `Task<T>` = kiểu trả về của method async
- Ví dụ: `await _context.SaveChangesAsync()` thay vì `_context.SaveChanges()`

---

### Câu 28: `AddControllers()` và `MapControllers()` trong Program.cs có tác dụng gì?
**Trả lời gợi ý:**
- `builder.Services.AddControllers()` → đăng ký các dịch vụ liên quan đến Controller (JSON serializer, Model Binding, Validation...)
- `app.MapControllers()` → ánh xạ các route attribute trên Controller vào routing pipeline
- Cả 2 đều cần có → thiếu một trong hai sẽ không tìm thấy API

---

### Câu 29: Swagger có tác dụng gì?
**Trả lời gợi ý:**
- Swagger = UI tự động sinh ra dựa trên code API
- Cho phép test API trực tiếp trên trình duyệt mà không cần Postman
- Trong dự án: `AddSwaggerGen()` + `UseSwagger()` + `UseSwaggerUI()`
- Truy cập tại: `https://localhost:{port}/swagger`

---

### Câu 30: Kiểm tra trùng lặp (duplicate check) được thực hiện thế nào?
**Trả lời gợi ý:**
- Dùng `AnyAsync()` trong LINQ để kiểm tra có bản ghi nào thỏa điều kiện không
- **Khi tạo mới:** kiểm tra toàn bộ bảng
```csharp
if (await _context.Enterprises.AnyAsync(e => e.Name == dto.Name))
    throw new UserFriendlyException("Tên đã tồn tại");
```
- **Khi cập nhật:** phải loại trừ chính bản ghi đang sửa (dùng `e.Id != id`)
```csharp
if (await _context.Enterprises.AnyAsync(e => e.Name == dto.Name && e.Id != id))
    throw new UserFriendlyException("Tên đã tồn tại");
```

---

## 🎯 PHẦN 9: CÂU HỎI THỰC HÀNH / CHỈ VÀO CODE

### Câu 31: Chỉ vào trong code, đây là API nào, làm việc gì?
*(Giảng viên chỉ vào một method trong Controller)*

**Hướng dẫn trả lời:**
- Đọc HTTP attribute (`[HttpPost]`, `[HttpGet]`, ...)
- Đọc route trong attribute hoặc controller
- Giải thích: nhận dữ liệu từ đâu (`[FromBody]` / `[FromQuery]` / route param)
- Mô tả luồng: nhận → validate → gọi service → trả về

---

### Câu 32: Giải thích tại sao phải dùng `?? 0` trong `MaxAsync((int?)ep.Quantity) ?? 0`?
**Trả lời gợi ý:**
- Nếu doanh nghiệp chưa nhập sản phẩm nào, tập kết quả rỗng
- `MaxAsync` trên tập rỗng sẽ ném exception nếu kiểu không nullable
- Ép về `(int?)` → kiểu nullable → trả về `null` thay vì exception
- `?? 0` → nếu null thì gán bằng 0
- Sau đó kiểm tra `if (maxQuantity == 0) return new List<>()` → trả danh sách rỗng

---

### Câu 33: `FindAsync(id)` khác `FirstOrDefaultAsync(e => e.Id == id)` thế nào?
**Trả lời gợi ý:**
- `FindAsync(id)` kiểm tra cache của DbContext trước (nếu đã load rồi không query lại DB)
- `FirstOrDefaultAsync(...)` luôn query xuống database
- `FindAsync` hiệu quả hơn khi cùng một request đã load entity đó rồi
- Cả 2 đều trả `null` nếu không tìm thấy

---

### Câu 34: Em đặt tên class theo quy tắc gì? Tại sao?
**Trả lời gợi ý:**
- Đặt theo dạng: `<TênClass><MãSV><ĐềSố>`
- Ví dụ: `ShipperDto0210668De1`, `ShipperService0210668De1`
- **Lý do:** Trong phòng thi, nhiều sinh viên cùng làm → tránh namespace conflict khi giảng viên copy code vào chung project để kiểm tra
- Đây là yêu cầu bắt buộc trong đề thi

---

### Câu 35: Nếu gọi API xóa một doanh nghiệp đang có sản phẩm liên kết, điều gì xảy ra?
**Trả lời gợi ý:**
- Nếu không cấu hình cascade delete → database ném lỗi FK constraint violation
- Nếu có cascade delete (EF Core mặc định) → xóa cả bản ghi trong bảng trung gian
- Trong dự án, tùy cấu hình trong DbContext (`.OnDelete(DeleteBehavior.Cascade)` hay `Restrict`)
- Nên xử lý rõ ràng: kiểm tra xem có sản phẩm liên kết không → thông báo lỗi thân thiện

---

## 📝 BẢNG TÓM TẮT LUỒNG CHẠY

```
Client gọi API
    ↓
[Route] + [HttpMethod] → Controller Action nhận request
    ↓
[FromBody] / [FromQuery] → Model Binding (ánh xạ JSON → DTO)
    ↓
ModelState Validation → nếu sai → 400 Bad Request
    ↓
Controller gọi Service (qua Interface)
    ↓
Service kiểm tra nghiệp vụ (trùng lặp, tồn tại...)
    ↓ (nếu sai) throw UserFriendlyException
    ↓ (nếu đúng) Service gọi DbContext
    ↓
DbContext dịch LINQ → SQL → thực thi trên Database
    ↓
Service trả kết quả về Controller
    ↓
Controller trả Ok(result) / BadRequest / NotFound về Client
    ↓
ExceptionMiddleware bắt nếu có exception chưa xử lý
```

---

## 💡 MẸO KHI VẤN ĐÁP

1. **Chỉ tay vào code** khi trả lời – đừng chỉ nói lý thuyết suông
2. **Dùng từ khóa đúng:** Entity, DbContext, Migration, DTO, Service, DI Container, Middleware
3. **Nếu không nhớ chi tiết**, mô tả đúng **vai trò** của nó trong hệ thống
4. **Luôn kết nối lý thuyết với thực tế** trong code của mình
5. **Chuẩn bị giải thích API cuối** (lấy sản phẩm nhiều nhất) – thường bị hỏi nhiều nhất vì logic phức tạp

---

*Cập nhật lần cuối: 03/06/2026*
