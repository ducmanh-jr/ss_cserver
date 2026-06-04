# CHẶNG 0: KHỞI TẠO DỰ ÁN TỪ SỐ KHÔNG (ZERO TO HERO)

## 📖 PHẦN LÝ THUYẾT: MỤC TIÊU
Trước khi phân chia thư mục hay viết code, chúng ta cần một **bộ khung dự án rỗng**. 
- .NET cung cấp sẵn lệnh tạo một dự án Web API.
- Các công cụ Entity Framework (EF Core) giống như những bộ "đồ nghề" giúp C# giao tiếp với SQL Server, nên ta phải tải chúng về (thông qua NuGet Package).
- Cấu hình lại `appsettings.json` và dọn dẹp `Program.cs` để dự án sạch sẽ nhất.

---

## 🛠️ PHẦN THAO TÁC TAY

### THAO TÁC 1: TẠO PROJECT WEB API
**🎯 Mục tiêu & Ý nghĩa:** 
- Lệnh này sẽ tạo ra một **bộ khung dự án chuẩn** của ASP.NET Core Web API, tạo sẵn các thư mục và file cần thiết ban đầu để bạn bắt đầu viết code ngay lập tức thay vì phải tạo thủ công từng file cấu hình.

1. Mở VSCode ở thư mục trống mà bạn muốn chứa toàn bộ bài làm.
2. Bấm **Terminal -> New Terminal** ở menu phía trên cùng.
3. Gõ lệnh tạo dự án (thay thế mã sinh viên của bạn vào tên):
   ```powershell
   dotnet new webapi -n nguyenducmanh0210668
   ```
4. Gõ tiếp lệnh để đi vào thư mục dự án vừa tạo:
   ```powershell
   cd nguyenducmanh0210668
   ```

### THAO TÁC 2: TẢI ĐỒ NGHỀ LÀM VIỆC VỚI DATABASE (EF CORE)
**🎯 Mục tiêu & Ý nghĩa:** 
- Tải các "plugin/thư viện" (Packages) từ mạng về. Mặc định C# chưa biết cách nói chuyện với SQL Server, nên ta cần tải **Entity Framework Core SQL Server** và **Tools** để có thể chạy các lệnh tạo bảng (Migration). Swagger thì giúp có giao diện web để test API dễ dàng.

1. Đảm bảo Terminal đang đứng trong thư mục `nguyenducmanh0210668`.
2. Lần lượt gõ 3 lệnh sau (Gõ xong 1 dòng thì nhấn Enter, đợi chạy xong rồi gõ dòng tiếp theo):
   ```powershell
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   dotnet add package Swashbuckle.AspNetCore
   ```
*(Giải thích: Tải công cụ kết nối SQL Server, công cụ chạy lệnh Migration, và công cụ tạo giao diện test API Swagger).*

### THAO TÁC 3: TẠO SẴN CẤU TRÚC THƯ MỤC DỰ ÁN
**🎯 Mục tiêu & Ý nghĩa:** 
- Thay vì cứ đến chặng nào lại tạo thư mục chặng đó, bạn có thể tạo trước toàn bộ các thư mục quan trọng bằng một lệnh duy nhất. Điều này giúp hệ thống thư mục của bạn chuyên nghiệp ngay từ đầu.

1. Đảm bảo Terminal vẫn đang đứng trong thư mục `nguyenducmanh0210668`.
2. Gõ (hoặc copy/paste) lệnh sau vào Terminal và nhấn Enter để tạo hàng loạt thư mục:
   ```powershell
   mkdir Entities, DbContexts, Dtos/Enterprises, Services/Interfaces, Services/Implements, Exceptions, Controllers
   ```

### THAO TÁC 4: CẤU HÌNH ĐỊA CHỈ DATABASE
**🎯 Mục tiêu & Ý nghĩa:** 
- File `appsettings.json` là nơi chứa **các thiết lập của ứng dụng**. Cấu hình "ConnectionStrings" ở đây giống như việc lưu lại "địa chỉ nhà, tên đăng nhập, mật khẩu" của SQL Server, để lát nữa ứng dụng biết đường mà tìm đến kết nối với Database.

1. Tại thư mục gốc dự án (`nguyenducmanh0210668`), tìm và mở file **`appsettings.json`** (file này đã được tạo tự động ở Thao tác 1).
2. Xóa hết nội dung cũ và dán **Code số 1** ở dưới vào. (Bạn có thể sửa `Database=NguyenDucManh0210668Db` thành tên database mong muốn).

### THAO TÁC 5: DỌN DẸP GIÁM ĐỐC (PROGRAM.CS)
**🎯 Mục tiêu & Ý nghĩa:** 
- Dọn dẹp đi những đoạn code mẫu mặc định không cần thiết của Microsoft để làm cho file `Program.cs` - "nhân vật chính điều phối mọi hoạt động" trở nên gọn gàng, sạch sẽ, chuẩn bị chỗ trống sẵn sàng cho việc cắm các tính năng nghiệp vụ của bạn vào ở các chặng sau.

1. Tại thư mục gốc dự án, tìm và mở file **`Program.cs`** (file này cũng đã được tạo tự động ở Thao tác 1).
2. Xóa sạch toàn bộ nội dung mặc định bên trong.
3. Dán **Code số 2** ở bên dưới vào (Code đã được viết gọn gàng, chuẩn bị sẵn chỗ để đăng ký Database và Service ở các chặng sau).

---

## 💻 PHẦN CODE ĐỂ COPY

**Code số 1: appsettings.json**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=NguyenDucManh0210668Db;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Code số 2: Program.cs**
```csharp
var builder = WebApplication.CreateBuilder(args);

// --- 1. ĐĂNG KÝ SERVICES (Nơi tuyển nhân viên) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

// (Chỗ này để dành lát nữa đăng ký DbContext và Service ở Chặng 1 và Chặng 3)


var app = builder.Build();

// --- 2. CẤU HÌNH MIDDLEWARE (Quy trình phục vụ) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```
