# Đề 1
**TRƯỜNG ĐẠI HỌC XÂY DỰNG HÀ NỘI**  
**BỘ MÔN KHOA HỌC MÁY TÍNH**

**ĐỀ THI KẾT THÚC HỌC PHẦN**  
**PHÁT TRIỂN ỨNG DỤNG PHÍA MÁY CHỦ**  

Xây dựng ứng dụng ASP .NET Core WEB API có tên `<Họ và tên><Mã số sinh viên>` (vd: `NguyenVanA1234`) thỏa mãn: (thời gian làm bài 135 phút)

## 1 Yêu cầu chung

### a. Tổ chức code
*   Coding convention sử dụng `PascalCase` với tên các trường, class, interface. Sử dụng `camelCase` với các biến. Sử dụng `_camelCase` với các trường có access modifier là `private`.
*   Cây thư mục code dạng như sau:
    *   `Properties`: các config chạy ứng dụng
    *   `Constants`: các hằng số
    *   `Controllers`: các API Controller
    *   `DbContexts`: các context entity framework
    *   `Dtos`: chia các thư mục con theo nghiệp vụ
    *   `Entities`: các entity map với bảng trong cơ sở dữ liệu
    *   `Exceptions`: các class xử lý ngoại lệ
    *   `Migrations`: các migration tạo các cấu trúc bảng quan hệ
    *   `Services`: xử lý các logic nghiệp vụ
        *   `Implements`: triển khai các interface
        *   `Interfaces`: khai báo các interface
    *   `Utils`: các hàm hữu ích dùng chung
    *   `appsetting.json`: các setting của ứng dụng dạng json
    *   `Program.cs`

### b. Yêu cầu từng thành phần
*   Các class Dto phải xử lý trim đối với các trường kiểu dữ liệu string.
*   Validate model bằng các built-in annotation attribute với các class Dto phục vụ cho việc create, update, delete (bao gồm bắt buộc nhập, số lượng ký tự với trường kiểu chuỗi, giá trị tối thiểu tối đa với trường kiểu số, . . . ).
*   Kiểu dữ liệu trong các entity sử dụng phù hợp với bài toán.
*   Các API Controller trả về kiểu dữ liệu phù hợp với interface `IActionResult`.
*   Các API Controller kế thừa từ class `ApiControllerBase` (tự tạo) xử lý các hàm dùng chung ví dụ như xử lý ngoại lệ trả về.
*   Xử lý logic trả ra các ngoại lệ thông qua class `UserFriendlyException`:
    ```csharp
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message) : base(message)
        {
        }
    }
    ```
*   Truy vấn cơ sở dữ liệu quan hệ sử dụng Entity Framework Core cho Sql Server tiếp cận theo hướng Code First (tạo các migration update cấu trúc bảng vào database).
*   Các bảng phải có khoá chính (primary key) dạng số nguyên và tự tăng.
*   Các bảng có quan hệ khoá ngoại phải được tạo liên kết khoá ngoại trong migration.
*   Các câu lệnh truy vấn cơ sở dữ liệu sử dụng các method Linq, keyword Linq.
*   Xử lý logic nghiệp vụ theo design pattern Dependency injection (DI): dưới dạng các class service được inject vào controller khi sử dụng.
*   Tổ chức các class theo hướng kế thừa (tùy chọn).
*   Đặt tên các class theo dạng `<Tên class><Mã số sinh viên><Đề thi số>` (vd: `StudentDto1234De1`).

## 2 Bài toán cụ thể

Cho quan hệ n-n shipper (gồm: tên, mã, ngày tham gia hệ thống, cccd (số căn cước công dân), tên và mã không được trùng) và sản phẩm (gồm: tên sản phẩm, mã sản phẩm, tên và mã không được trùng). Trong bảng quan hệ thêm một trường lưu số lượng kiểu số nguyên lớn hơn hoặc bằng 0 để quản lý shipper đã giao 1 sản phẩm với số lượng bao nhiêu.

*   Thực hiện các chức năng sau:
    *   Tạo migration và update vào database cấu trúc các bảng của bài toán trên.
    *   Tạo các API thêm, sửa, xóa shipper (các hàm thêm và sửa lưu ý kiểm tra trùng).
    *   Tạo API xem danh sách có phân trang (gợi ý phân trang bằng `PageSize` (số phần tử một trang) và `PageIndex` (trang số mấy tính từ 1)) danh sách shipper có cho phép lọc gần đúng theo tên hoặc theo số cccd (gợi ý dùng trường `Keyword` trong class filter Dto).
    *   Tạo API liệt kê danh sách những sản phẩm được giao với số lượng nhiều nhất của một shipper với đầu vào là id shipper đầu ra là danh sách sản phẩm gồm tên và mã của sản phẩm xếp giảm dần theo mã (Kiểm tra lại API bằng cách nhập các dữ liệu mẫu vào các bảng trong cơ sở dữ liệu).

*Ghi chú: Sinh viên đọc kỹ đề, cán bộ coi thi không giải thích gì thêm.*

---
<div style="text-align: center;">
    <b>———————————– Kết thúc ————————————</b>
</div>

<table style="width: 100%; border: none; margin-top: 20px;">
    <tr style="border: none;">
        <td style="width: 50%; text-align: center; border: none; vertical-align: top;">
            <b>TRƯỞNG BỘ MÔN</b><br>
            <i>(Ký và ghi rõ họ tên)</i><br><br><br><br>
            <b>ThS. Hoàng Nam Thắng</b>
        </td>
        <td style="width: 50%; text-align: center; border: none; vertical-align: top;">
            <i>Hà Nội, ngày 16 tháng 05 năm 2024</i><br>
            <b>GIẢNG VIÊN RA ĐỀ</b><br><br><br><br><br>
            <b>KS. Lê Văn Minh</b>
        </td>
    </tr>
</table>
