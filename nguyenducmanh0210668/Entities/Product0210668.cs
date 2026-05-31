using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nguyenducmanh0210668.Entities;

public class Product0210668
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public ICollection<EnterpriseProduct0210668> EnterpriseProducts { get; set; } = new List<EnterpriseProduct0210668>();
}