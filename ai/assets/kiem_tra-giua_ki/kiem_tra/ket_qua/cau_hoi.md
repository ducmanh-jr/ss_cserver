# Giải thích bản chất phép toán Inner Join

**Câu hỏi:** Tại sao kết quả API (dùng Inner Join) lại bị thiếu một hoặc nhiều vật tư so với danh sách mẫu ban đầu?

**Trả lời:**

Dữ liệu bị thiếu là do bản chất hoạt động của phép toán **Inner Join**. 

Trong danh sách mẫu của chúng ta, có một số vật tư (như "Ống nhựa PVC" và "Sơn chống thấm") có trường `SupplierId` mang giá trị `null` (chưa có nhà cung cấp). 

Khi thực hiện phép toán Inner Join giữa bảng Vật tư (Material) và bảng Nhà cung cấp (Supplier), hệ thống chỉ giữ lại các bản ghi có giá trị khớp nhau ở cả hai phía:
- Một vật tư chỉ được hiển thị nếu nó có `SupplierId` hợp lệ.
- `SupplierId` đó phải tồn tại trong danh sách Nhà cung cấp.

Vì những vật tư kể trên không có mã nhà cung cấp, chúng không thỏa mãn điều kiện kết nối và bị loại bỏ khỏi kết quả trả về. Đây chính là lý do tại sao danh sách Inner Join thường ngắn hơn danh sách gốc. Ngược lại, để lấy được đầy đủ cả những vật tư này, chúng ta phải sử dụng phép toán **Left Join**.
