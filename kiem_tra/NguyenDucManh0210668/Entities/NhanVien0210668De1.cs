namespace NguyenDucManh0210668.Entities;

public class NhanVien0210668De1
{
    public int Id { get; set; }
    public string TenNhanVien { get; set; } = string.Empty;
    public string MaNhanVien { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<PhanCong0210668De1> PhanCongs { get; set; } = new List<PhanCong0210668De1>();
}
