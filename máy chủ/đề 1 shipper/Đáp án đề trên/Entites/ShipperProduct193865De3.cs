using System.ComponentModel.DataAnnotations.Schema;

namespace Nguyen_Khanh_Thu_193865.Entites
{
    [Table("ShipperProduct193865De3")]
    public class ShipperProduct193865De3
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ShipperID { get; set; }
        
        public int ProductId { get; set; }
        public int SoLuong {  get; set; }
        public Product193865De3 product193865 { get; set; }

        public Shipper193865De3 shipper193865 { get; set; }
    }
}
