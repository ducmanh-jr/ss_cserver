namespace NguyenDucManh0210668.Entities;

public class DuAn0210668De1
{
    public int Id { get; set; }
    public string TenDuAn { get; set; } = string.Empty;
    public string MaDuAn { get; set; } = string.Empty;
    public ICollection<PhanCong0210668De1> PhanCongs { get; set; } = new List<PhanCong0210668De1>();
}
