# Test cases Đề 1 - Nguyễn Đức Mạnh 0210668

Project: `NguyenDucManh0210668`  
Database: MySQL `nguyenducmanh0210668de1`  
Base URL khi chạy local: `http://localhost:5188`

## Chuẩn bị

Chạy project:

```powershell
cd C:\Users\Admin\Downloads\kiem_tra\NguyenDucManh0210668
dotnet build
dotnet ef database update
dotnet run
```

Mở Swagger:

```text
http://localhost:5188/swagger
```

Nếu muốn reset dữ liệu test trong MySQL:

```sql
SET FOREIGN_KEY_CHECKS=0;
TRUNCATE TABLE PhanCongs;
TRUNCATE TABLE DuAns;
TRUNCATE TABLE NhanViens;
SET FOREIGN_KEY_CHECKS=1;
```

## Quy ước response chuẩn

Response thành công:

```json
{
  "isSuccess": true,
  "data": {},
  "message": "Thao tác thành công.",
  "code": 200
}
```

Response lỗi:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Thông báo lỗi",
  "code": 400
}
```

## Nhóm 1 - Test API Nhân viên

### TC-NV-01 - Thêm nhân viên thành công

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

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `message = "Thêm mới thành công."`
- `data.id > 0`
- `data.tenNhanVien = "Nguyễn Đức Mạnh"`
- `data.maNhanVien = "NV001"`
- `data.email = "manh001@example.com"`

### TC-NV-02 - Thêm nhân viên có trim chuỗi

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "tenNhanVien": "  Nguyễn Đức Mạnh  ",
  "maNhanVien": "  NV002  ",
  "email": "  manh002@example.com  "
}
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `data.tenNhanVien = "Nguyễn Đức Mạnh"`
- `data.maNhanVien = "NV002"`
- `data.email = "manh002@example.com"`

### TC-NV-03 - Thêm nhân viên thiếu tên

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "maNhanVien": "NV003",
  "email": "manh003@example.com"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `data = null`
- `message` chứa nội dung tên nhân viên bắt buộc.
- `code = 400`

### TC-NV-04 - Thêm nhân viên thiếu mã nhân viên

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "tenNhanVien": "Nguyễn Đức Mạnh",
  "email": "manh004@example.com"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message` chứa nội dung mã nhân viên bắt buộc.

### TC-NV-05 - Thêm nhân viên thiếu email

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "tenNhanVien": "Nguyễn Đức Mạnh",
  "maNhanVien": "NV005"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message` chứa nội dung email bắt buộc.

### TC-NV-06 - Thêm nhân viên email sai định dạng

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "tenNhanVien": "Nguyễn Đức Mạnh",
  "maNhanVien": "NV006",
  "email": "email-sai-dinh-dang"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message` chứa nội dung email không đúng định dạng.

### TC-NV-07 - Thêm nhân viên trùng mã nhân viên

Điều kiện trước:

- Đã có nhân viên mã `NV001`.

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "tenNhanVien": "Nhân viên khác",
  "maNhanVien": "NV001",
  "email": "nhanvienkhac@example.com"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message = "Mã nhân viên đã tồn tại."`
- `code = 400`

### TC-NV-08 - Thêm nhân viên trùng email

Điều kiện trước:

- Đã có nhân viên email `manh001@example.com`.

Endpoint:

```http
POST /api/nhan-viens
```

Body:

```json
{
  "tenNhanVien": "Nhân viên khác",
  "maNhanVien": "NV008",
  "email": "manh001@example.com"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message = "Email đã tồn tại."`
- `code = 400`

### TC-NV-09 - Cập nhật nhân viên thành công

Điều kiện trước:

- Đã có nhân viên `id = 1`.

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

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `message = "Cập nhật thành công."`
- `data.id = 1`
- `data.tenNhanVien = "Nguyễn Đức Mạnh Updated"`

### TC-NV-10 - Cập nhật nhân viên không tồn tại

Endpoint:

```http
PUT /api/nhan-viens
```

Body:

```json
{
  "id": 999999,
  "tenNhanVien": "Không tồn tại",
  "maNhanVien": "NV999999",
  "email": "notfound@example.com"
}
```

Kết quả mong đợi:

- HTTP status: `404 Not Found`
- `isSuccess = false`
- `message = "Không tìm thấy nhân viên."`
- `code = 404`

### TC-NV-11 - Cập nhật nhân viên trùng mã của nhân viên khác

Điều kiện trước:

- Có nhân viên 1 mã `NV001`.
- Có nhân viên 2 mã `NV002`.

Endpoint:

```http
PUT /api/nhan-viens
```

Body:

```json
{
  "id": 2,
  "tenNhanVien": "Nguyễn Đức Mạnh 2",
  "maNhanVien": "NV001",
  "email": "manh002@example.com"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `message = "Mã nhân viên đã tồn tại."`

### TC-NV-12 - Xóa nhân viên thành công

Điều kiện trước:

- Có nhân viên `id = 1`.

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

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `message = "Xóa thành công."`

### TC-NV-13 - Xóa nhân viên không tồn tại

Endpoint:

```http
DELETE /api/nhan-viens
```

Body:

```json
{
  "id": 999999
}
```

Kết quả mong đợi:

- HTTP status: `404 Not Found`
- `isSuccess = false`
- `message = "Không tìm thấy nhân viên."`

## Nhóm 2 - Test phân trang và tìm kiếm Nhân viên

### TC-PAGE-01 - Lấy danh sách nhân viên trang đầu

Endpoint:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=10
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `data.items` là danh sách.
- `data.pageIndex = 1`
- `data.pageSize = 10`
- `data.totalItems >= 0`
- `data.totalPages >= 0`

### TC-PAGE-02 - Tìm kiếm gần đúng theo mã nhân viên

Điều kiện trước:

- Có nhân viên mã `NV001`.

Endpoint:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=NV001
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `data.totalItems >= 1`
- Trong `data.items` có nhân viên mã `NV001`.

### TC-PAGE-03 - Tìm kiếm gần đúng theo tên nhân viên

Điều kiện trước:

- Có nhân viên tên `Nguyễn Đức Mạnh`.

Endpoint:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=Mạnh
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- Trong danh sách có nhân viên tên chứa `Mạnh`.

### TC-PAGE-04 - Tìm kiếm không phân biệt hoa thường

Điều kiện trước:

- Có nhân viên mã `NV001`.

Endpoint:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=nv001
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- Vẫn tìm được nhân viên mã `NV001`.

### TC-PAGE-05 - PageIndex không hợp lệ

Endpoint:

```http
GET /api/nhan-viens?PageIndex=0&PageSize=10
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message` chứa nội dung `PageIndex phải lớn hơn 0`.

### TC-PAGE-06 - PageSize vượt giới hạn

Endpoint:

```http
GET /api/nhan-viens?PageIndex=1&PageSize=101
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message` chứa nội dung `PageSize phải từ 1 đến 100`.

## Nhóm 3 - Test API Dự án

### TC-DA-01 - Thêm dự án thành công

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

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `message = "Thêm mới thành công."`
- `data.id > 0`
- `data.tenDuAn = "Dự án Server"`
- `data.maDuAn = "DA001"`

### TC-DA-02 - Thêm dự án trùng tên

Điều kiện trước:

- Có dự án tên `Dự án Server`.

Endpoint:

```http
POST /api/du-ans
```

Body:

```json
{
  "tenDuAn": "Dự án Server",
  "maDuAn": "DA002"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message = "Tên dự án đã tồn tại."`

### TC-DA-03 - Thêm dự án trùng mã

Điều kiện trước:

- Có dự án mã `DA001`.

Endpoint:

```http
POST /api/du-ans
```

Body:

```json
{
  "tenDuAn": "Dự án khác",
  "maDuAn": "DA001"
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message = "Mã dự án đã tồn tại."`

### TC-DA-04 - Lấy danh sách dự án

Endpoint:

```http
GET /api/du-ans
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `data` là danh sách dự án.

## Nhóm 4 - Test API Phân công

### TC-PC-01 - Tạo phân công thành công

Điều kiện trước:

- Có nhân viên `id = 1`.
- Có dự án `id = 1`.

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

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `data.nhanVienId = 1`
- `data.duAnId = 1`
- `data.soGioLamViec = 80`

### TC-PC-02 - Cập nhật số giờ nếu phân công đã tồn tại

Điều kiện trước:

- Đã có phân công `nhanVienId = 1`, `duAnId = 1`, `soGioLamViec = 80`.

Endpoint:

```http
POST /api/phan-congs
```

Body:

```json
{
  "nhanVienId": 1,
  "duAnId": 1,
  "soGioLamViec": 120
}
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- Không tạo bản ghi phân công trùng.
- `data.soGioLamViec = 120`.

### TC-PC-03 - Phân công với nhân viên không tồn tại

Endpoint:

```http
POST /api/phan-congs
```

Body:

```json
{
  "nhanVienId": 999999,
  "duAnId": 1,
  "soGioLamViec": 80
}
```

Kết quả mong đợi:

- HTTP status: `404 Not Found`
- `isSuccess = false`
- `message = "Không tìm thấy nhân viên."`

### TC-PC-04 - Phân công với dự án không tồn tại

Endpoint:

```http
POST /api/phan-congs
```

Body:

```json
{
  "nhanVienId": 1,
  "duAnId": 999999,
  "soGioLamViec": 80
}
```

Kết quả mong đợi:

- HTTP status: `404 Not Found`
- `isSuccess = false`
- `message = "Không tìm thấy dự án."`

### TC-PC-05 - Số giờ làm việc không hợp lệ

Endpoint:

```http
POST /api/phan-congs
```

Body:

```json
{
  "nhanVienId": 1,
  "duAnId": 1,
  "soGioLamViec": 0
}
```

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `message` chứa nội dung `Số giờ làm việc phải từ 1 đến 10000`.

## Nhóm 5 - Test API dự án theo số giờ làm việc

### TC-TK-01 - Lấy danh sách dự án theo số giờ giảm dần

Điều kiện trước:

Tạo dữ liệu:

```text
Nhân viên 1: Nguyễn Đức Mạnh
Dự án 1: DA001, số giờ 35
Dự án 2: DA002, số giờ 90
Dự án 3: DA003, số giờ 60
```

Endpoint:

```http
GET /api/nhan-viens/1/du-ans-theo-so-gio
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- Danh sách sắp xếp giảm dần theo `soGioLamViec`.

Thứ tự mong đợi:

```text
DA002 - 90
DA003 - 60
DA001 - 35
```

### TC-TK-02 - Nhân viên tồn tại nhưng chưa có phân công

Điều kiện trước:

- Có nhân viên `id = 1`.
- Nhân viên chưa có phân công nào.

Endpoint:

```http
GET /api/nhan-viens/1/du-ans-theo-so-gio
```

Kết quả mong đợi:

- HTTP status: `200 OK`
- `isSuccess = true`
- `data = []`
- `message = "Thao tác thành công."`

### TC-TK-03 - Nhân viên không tồn tại

Endpoint:

```http
GET /api/nhan-viens/999999/du-ans-theo-so-gio
```

Kết quả mong đợi:

- HTTP status: `404 Not Found`
- `isSuccess = false`
- `message = "Không tìm thấy nhân viên."`
- `code = 404`

## Nhóm 6 - Test database và migration

### TC-DB-01 - Migration tạo đủ bảng

Lệnh kiểm tra:

```sql
SHOW TABLES;
```

Kết quả mong đợi có các bảng:

```text
NhanViens
DuAns
PhanCongs
__EFMigrationsHistory
```

### TC-DB-02 - Bảng Nhân viên có unique index

Lệnh kiểm tra:

```sql
SHOW INDEX FROM NhanViens;
```

Kết quả mong đợi:

- Có unique index cho `MaNhanVien`.
- Có unique index cho `Email`.

### TC-DB-03 - Bảng Dự án có unique index

Lệnh kiểm tra:

```sql
SHOW INDEX FROM DuAns;
```

Kết quả mong đợi:

- Có unique index cho `TenDuAn`.
- Có unique index cho `MaDuAn`.

### TC-DB-04 - Bảng Phân công có khóa ngoại

Lệnh kiểm tra:

```sql
SHOW CREATE TABLE PhanCongs;
```

Kết quả mong đợi:

- Có foreign key `NhanVienId` tham chiếu `NhanViens(Id)`.
- Có foreign key `DuAnId` tham chiếu `DuAns(Id)`.
- Có `ON DELETE CASCADE`.

### TC-DB-05 - Bảng Phân công không cho trùng cặp nhân viên - dự án

Điều kiện trước:

- Đã có phân công `NhanVienId = 1`, `DuAnId = 1`.

Kiểm tra:

```sql
SHOW INDEX FROM PhanCongs;
```

Kết quả mong đợi:

- Có unique index trên cặp `NhanVienId`, `DuAnId`.

## Nhóm 7 - Test exception và response

### TC-EX-01 - Lỗi nghiệp vụ trả UserFriendlyException

Tình huống:

- Thêm nhân viên trùng mã.

Kết quả mong đợi:

- Service ném `UserFriendlyException0210668De1`.
- Middleware bắt lỗi.
- API trả JSON thống nhất:

```json
{
  "isSuccess": false,
  "data": null,
  "message": "Mã nhân viên đã tồn tại.",
  "code": 400
}
```

### TC-EX-02 - Lỗi không tìm thấy trả 404

Tình huống:

- Cập nhật, xóa hoặc lấy dự án theo giờ với nhân viên không tồn tại.

Kết quả mong đợi:

- HTTP status: `404 Not Found`
- `isSuccess = false`
- `message = "Không tìm thấy nhân viên."`
- `code = 404`

### TC-EX-03 - Lỗi validate model trả 400

Tình huống:

- Gửi `PageIndex = 0`.
- Gửi email sai định dạng.
- Gửi `SoGioLamViec = 0`.

Kết quả mong đợi:

- HTTP status: `400 Bad Request`
- `isSuccess = false`
- `data = null`
- `code = 400`
- `message` chứa lỗi validation cụ thể.

## Checklist test nhanh trước khi nộp

Chạy các lệnh:

```powershell
dotnet build
dotnet ef migrations list
dotnet ef database update
dotnet run
```

Kiểm tra nhanh trên Swagger:

```text
1. POST /api/nhan-viens
2. POST /api/du-ans
3. POST /api/phan-congs
4. GET /api/nhan-viens?PageIndex=1&PageSize=10&Keyword=NV
5. GET /api/nhan-viens/{id}/du-ans-theo-so-gio
```

Nếu cả 5 bước chạy đúng, bài đã đáp ứng các yêu cầu chính của đề.

