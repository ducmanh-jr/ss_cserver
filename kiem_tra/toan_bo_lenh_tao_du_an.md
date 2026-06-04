# TOÀN BỘ LỆNH KHỞI TẠO VÀ CHẠY DỰ ÁN TỪ A ĐẾN Z

Dưới đây là tổng hợp toàn bộ các dòng lệnh terminal/powershell từ bước đầu tiên khởi tạo dự án trống cho đến khi cài đặt xong môi trường và có thể chạy dự án.

## 1. Khởi tạo dự án
Tạo một Web API project mới mang tên bạn và mã số sinh viên:
```powershell
dotnet new webapi -n nguyenducmanh0210668
```

## 2. Truy cập vào thư mục dự án
Di chuyển Terminal vào trong thư mục vừa tạo để bắt đầu làm việc:
```powershell
cd nguyenducmanh0210668
```

## 3. Cài đặt các thư viện cần thiết (Packages)
Cài đặt Entity Framework Core và Swagger để làm việc với cơ sở dữ liệu SQL Server và tạo giao diện test API:
```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Swashbuckle.AspNetCore
```

## 4. Tạo cấu trúc thư mục
Tạo hàng loạt các thư mục chuẩn hóa theo yêu cầu đề bài chỉ với một lệnh duy nhất:
```powershell
mkdir Constants, DbContexts, Dtos, Entities, Exceptions, Services, Services/Implements, Services/Interfaces, Utils
```

*(Sau bước này, bạn tiến hành viết code/thêm file vào các thư mục đã tạo tương tự như tôi đã làm ở trên)*

## 5. Tạo Migration (Sau khi viết code DbContext và Entities)
Khi code Entity và DbContext đã hoàn tất, hãy quét cấu trúc này để sinh ra kịch bản tạo bảng cho cơ sở dữ liệu:
```powershell
dotnet ef migrations add InitialCreate
```

## 6. Cập nhật Database
Áp dụng các bảng vào SQL Server thật của bạn (nhớ cấu hình `DefaultConnection` trong `appsettings.json` cho đúng trước khi chạy lệnh):
```powershell
dotnet ef database update
```

## 7. Biên dịch và Chạy dự án
Build dự án để kiểm tra lỗi:
```powershell
dotnet build
```

Khởi động dự án:
```powershell
dotnet run
```

Mở trình duyệt vào `https://localhost:<port_của_bạn>/swagger/index.html` để kiểm tra.
