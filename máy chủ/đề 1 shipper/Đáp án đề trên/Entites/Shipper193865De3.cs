using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nguyen_Khanh_Thu_193865.Entites
{
    [Table("Shipper193865De3")]
    public class Shipper193865De3
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string MaShipper { get; set; }
        [Required]
        public string CCCD { get; set; }
        [Required]
        public string Ten { get; set; }
        [Required]
        public DateTime NgayThamGia { get; set; }

    }
}
