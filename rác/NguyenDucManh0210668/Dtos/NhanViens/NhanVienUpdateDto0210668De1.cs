using System.ComponentModel.DataAnnotations;

namespace NguyenDucManh0210668.Dtos.NhanViens;

public class NhanVienUpdateDto0210668De1
{
    private string _tenNhanVien = string.Empty;
    private string _maNhanVien = string.Empty;
    private string _email = string.Empty;

    [Required(ErrorMessage = "Id nhân viên là bắt buộc.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id nhân viên phải lớn hơn 0.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên nhân viên là bắt buộc.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Tên nhân viên phải từ 2 đến 150 ký tự.")]
    public string TenNhanVien
    {
        get => _tenNhanVien;
        set => _tenNhanVien = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Mã nhân viên là bắt buộc.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Mã nhân viên phải từ 2 đến 50 ký tự.")]
    public string MaNhanVien
    {
        get => _maNhanVien;
        set => _maNhanVien = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Email là bắt buộc.")]
    [StringLength(150, ErrorMessage = "Email không được vượt quá 150 ký tự.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    public string Email
    {
        get => _email;
        set => _email = value?.Trim() ?? string.Empty;
    }
}
