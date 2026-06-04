using System.ComponentModel.DataAnnotations;

namespace NguyenDucManh0210668.Dtos.NhanViens;

public class NhanVienDeleteDto0210668De1
{
    [Required(ErrorMessage = "Id nhân viên là bắt buộc.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id nhân viên phải lớn hơn 0.")]
    public int Id { get; set; }
}
