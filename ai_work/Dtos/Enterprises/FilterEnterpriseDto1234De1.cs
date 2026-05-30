using System.ComponentModel.DataAnnotations;

namespace DucManhJr1234.Dtos.Enterprises;

public class FilterEnterpriseDto1234De1
{
    private string? _keyword;

    [Range(1, 100, ErrorMessage = "PageSize phai tu 1 den 100")]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue, ErrorMessage = "PageIndex phai lon hon 0")]
    public int PageIndex { get; set; } = 1;

    [StringLength(255, ErrorMessage = "Keyword toi da 255 ky tu")]
    public string? Keyword
    {
        get => _keyword;
        set => _keyword = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
