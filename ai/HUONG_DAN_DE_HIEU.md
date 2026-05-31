# LỘ TRÌNH XÂY DỰNG WEB API QUẢN LÝ DOANH NGHIỆP VÀ SẢN PHẨM

Dự án này là một **Hệ thống API Quản lý Doanh nghiệp nhập khẩu Sản phẩm**. 
Mục tiêu là cung cấp các đường dẫn (API) để giao diện Web hoặc Mobile có thể gọi vào hệ thống để Thêm/Sửa/Xóa Doanh nghiệp, và xem danh sách Sản phẩm mà Doanh nghiệp đó nhập.

Chúng ta chia dự án thành **4 Chặng Đường Chính**:

---

## 🚩 CHẶNG 1: XÂY DỰNG NỀN MÓNG & DATABASE
**Mục tiêu:** Định nghĩa cấu trúc dữ liệu và tạo bảng thật trong SQL Server.

* **Thư mục dự án:** Chia code thành các thư mục `Entities`, `Controllers`, `Services`, `Dtos` để dễ quản lý thay vì vứt tất cả vào một chỗ.
* **Entities (Thực thể dữ liệu):** `Enterprise0210668.cs` và `Product0210668.cs` là cấu trúc tương đương với các bảng sẽ nằm trong Database. Bạn quy định bảng Doanh nghiệp phải có cột Tên, Mã số thuế. Bảng Sản phẩm phải có Mã sản phẩm.
* **DbContext (Cầu nối CSDL):** C# không tự hiểu SQL. `AppDbContext0210668.cs` là thành phần làm cầu nối, dịch các Entity thành bảng trong SQL Server, thiết lập khóa ngoại và khóa chính (như bảng trung gian `EnterpriseProduct0210668`).
* **Migration (Thi công CSDL):** Khi code xong Entity và DbContext, lệnh `dotnet ef migrations add` và `database update` sẽ tự động sinh ra file script SQL và tạo bảng thực tế trong SQL Server dựa trên chuỗi kết nối ở `appsettings.json`.

---

## 🚩 CHẶNG 2: ĐÓNG GÓI DỮ LIỆU & KIỂM TRA (DTO & VALIDATION)
**Mục tiêu:** Không cho phép Web/Mobile truyền trực tiếp dữ liệu thô vào Entity, phải qua một lớp bọc an toàn.

* **DTO (Data Transfer Object - Lớp bọc dữ liệu):**
  Giả sử bảng Doanh nghiệp có 20 cột, nhưng khi Web gửi yêu cầu Thêm mới, họ chỉ cần gửi 3 cột (Tên, Mã số thuế, Địa chỉ). Ta tạo ra `EnterpriseCreateDto.cs` chỉ chứa 3 cột này. Nó giúp ẩn các cột nhạy cảm (như Id, Ngày tạo) khỏi người dùng.
* **Validation (Ràng buộc dữ liệu):**
  Khi Web gửi `EnterpriseCreateDto` lên, ta phải kiểm tra ngay lập tức: *"Tên có bị để trống không?", "Mã số thuế có quá dài không?"*. Các thẻ `[Required]`, `[MaxLength]` làm nhiệm vụ chặn đứng dữ liệu sai ngay từ đầu.

---

## 🚩 CHẶNG 3: XỬ LÝ NGHIỆP VỤ (SERVICES)
**Mục tiêu:** Viết các tính năng cốt lõi (Thêm, Sửa, Xóa, Tìm kiếm, Phân trang Doanh nghiệp).

* **Interface (`IEnterpriseService`):**
  Nơi khai báo danh sách các tính năng mà hệ thống cung cấp (Ví dụ: `CreateEnterprise`, `GetTopProducts`). Nó giúp ẩn giấu cách code bên trong.
* **Service (`EnterpriseService`):**
  Nơi chứa toàn bộ logic. Tại đây bạn lấy dữ liệu `DTO` đã hợp lệ, chọc xuống `DbContext` để kiểm tra nghiệp vụ: *"Mã số thuế này đã tồn tại trong DB chưa?"*. Nếu rồi thì ném ra lỗi (`UserFriendlyException`). Nếu chưa thì chuyển DTO thành Entity và lưu xuống SQL Server.

---

## 🚩 CHẶNG 4: CUNG CẤP API CHO WEB/MOBILE (CONTROLLERS)
**Mục tiêu:** Tạo ra các đường dẫn (URL) để bên ngoài có thể gọi vào hệ thống.

* **Controller (`EnterprisesController`):**
  Nơi định nghĩa các Endpoints (ví dụ: `POST /api/enterprises`). 
  Khi Web gọi URL này, Controller sẽ tiếp nhận `DTO`, đưa cho `Service` xử lý. Sau khi `Service` lưu thành công vào SQL Server, Controller sẽ trả kết quả báo mã `200 OK` về cho Web. 
  Quy tắc vàng: **Không bao giờ viết logic nghiệp vụ (kiểm tra trùng lặp, lưu DB) trực tiếp trong Controller. Mọi thứ phải giao cho Service làm.**

---
**TỔNG KẾT LUỒNG CHẠY CỦA HỆ THỐNG:**
Giao diện Web/Mobile gọi API ➡️ **Controller** nhận Request ➡️ Dữ liệu tự động bọc vào **DTO** và bị kiểm tra **Validation** ➡️ Đưa vào **Service** xử lý nghiệp vụ ➡️ Service gọi **DbContext** lấy/lưu dữ liệu từ **SQL Server**. Xong xuôi thì Response trả ngược về cho giao diện!
