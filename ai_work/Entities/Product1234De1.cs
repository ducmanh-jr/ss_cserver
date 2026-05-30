namespace DucManhJr1234.Entities;

public class Product1234De1
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public DateTime ImportDate { get; set; }

    public ICollection<EnterpriseProduct1234De1> EnterpriseProducts { get; set; } = new List<EnterpriseProduct1234De1>();
}
