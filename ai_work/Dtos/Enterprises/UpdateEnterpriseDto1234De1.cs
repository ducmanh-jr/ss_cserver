using System.ComponentModel.DataAnnotations;

namespace DucManhJr1234.Dtos.Enterprises;

public class UpdateEnterpriseDto1234De1
{
    private string _name = string.Empty;
    private string _taxCode = string.Empty;
    private string _address = string.Empty;

    [Required(ErrorMessage = "Ten doanh nghiep khong duoc de trong")]
    [StringLength(255, ErrorMessage = "Ten doanh nghiep toi da 255 ky tu")]
    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Ma so thue khong duoc de trong")]
    [StringLength(50, ErrorMessage = "Ma so thue toi da 50 ky tu")]
    public string TaxCode
    {
        get => _taxCode;
        set => _taxCode = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Dia chi khong duoc de trong")]
    [StringLength(500, ErrorMessage = "Dia chi toi da 500 ky tu")]
    public string Address
    {
        get => _address;
        set => _address = value?.Trim() ?? string.Empty;
    }
}
