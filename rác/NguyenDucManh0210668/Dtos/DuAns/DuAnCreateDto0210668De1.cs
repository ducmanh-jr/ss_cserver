using System.ComponentModel.DataAnnotations;

namespace NguyenDucManh0210668.Dtos.DuAns;

public class DuAnCreateDto0210668De1
{
    private string _tenDuAn = string.Empty;
    private string _maDuAn = string.Empty;

    [Required(ErrorMessage = "Tên dự án là bắt buộc.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Tên dự án phải từ 2 đến 150 ký tự.")]
    public string TenDuAn
    {
        get => _tenDuAn;
        set => _tenDuAn = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Mã dự án là bắt buộc.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Mã dự án phải từ 2 đến 50 ký tự.")]
    public string MaDuAn
    {
        get => _maDuAn;
        set => _maDuAn = value?.Trim() ?? string.Empty;
    }
}
