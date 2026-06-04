using System.ComponentModel.DataAnnotations;

namespace NguyenDucManh0210668.Dtos.NhanViens;

public class NhanVienFilterDto0210668De1
{
    private string? _keyword;

    [Range(1, int.MaxValue, ErrorMessage = "PageIndex phải lớn hơn 0.")]
    public int PageIndex { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "PageSize phải từ 1 đến 100.")]
    public int PageSize { get; set; } = 10;

    [StringLength(150, ErrorMessage = "Keyword không được vượt quá 150 ký tự.")]
    public string? Keyword
    {
        get => _keyword;
        set => _keyword = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
