using System.ComponentModel.DataAnnotations;

namespace nguyenducmanh0210668.Entities;

public class Enterprise0210668
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string TaxCode { get; set; } = null!;

    public string? Address { get; set; }

    public ICollection<EnterpriseProduct0210668> EnterpriseProducts { get; set; } = new List<EnterpriseProduct0210668>();
}