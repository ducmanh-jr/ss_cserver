using System.ComponentModel.DataAnnotations;

namespace NguyenDucManh0210668.Dtos.PhanCongs;

public class PhanCongCreateOrUpdateDto0210668De1
{
    [Required(ErrorMessage = "Id nhân viên là bắt buộc.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id nhân viên phải lớn hơn 0.")]
    public int NhanVienId { get; set; }

    [Required(ErrorMessage = "Id dự án là bắt buộc.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id dự án phải lớn hơn 0.")]
    public int DuAnId { get; set; }

    [Required(ErrorMessage = "Số giờ làm việc là bắt buộc.")]
    [Range(1, 10000, ErrorMessage = "Số giờ làm việc phải từ 1 đến 10000.")]
    public int SoGioLamViec { get; set; }
}
