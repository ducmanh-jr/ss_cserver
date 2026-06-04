using System.ComponentModel.DataAnnotations;

namespace nguyenducmanh0210668.Entities
{
    public class SanPham0210668De1
    {
        [Key]
        public int Id { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        
        // Móc nối quan hệ n-n
        public ICollection<ChiTietGiaoHang0210668De1> ChiTietGiaoHangs { get; set; }
    }
}
