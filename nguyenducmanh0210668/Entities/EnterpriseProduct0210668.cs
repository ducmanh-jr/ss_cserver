namespace nguyenducmanh0210668.Entities;

public class EnterpriseProduct0210668
{
    public int EnterpriseId { get; set; }
    public Enterprise0210668 Enterprise { get; set; } = null!;

    public int ProductId { get; set; }
    public Product0210668 Product { get; set; } = null!;
}