using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Nguyen_Khanh_Thu_193865.Entites
{
    [Table("Product193865De3")]
    public class Product193865De3
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string MaProduct { get; set; }
        [Required]
        public string TenProduct { get; set; }
    }
}
