using System.ComponentModel.DataAnnotations;

namespace nguyenducmanh0210668.Entities
{
    public class Shipper0210668De1
    {
        [Key]
        public int Id { get; set; }
        public string MaShipper { get; set; }
        public string TenShipper { get; set; }
        public string CCCD { get; set; }
        public DateTime NgayThamGia { get; set; }
        
        // Móc nối quan hệ n-n
        public ICollection<ChiTietGiaoHang0210668De1> ChiTietGiaoHangs { get; set; }
    }
}
