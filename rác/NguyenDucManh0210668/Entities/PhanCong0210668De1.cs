namespace NguyenDucManh0210668.Entities;

public class PhanCong0210668De1
{
    public int Id { get; set; }
    public int NhanVienId { get; set; }
    public int DuAnId { get; set; }
    public int SoGioLamViec { get; set; }
    public NhanVien0210668De1? NhanVien { get; set; }
    public DuAn0210668De1? DuAn { get; set; }
}
