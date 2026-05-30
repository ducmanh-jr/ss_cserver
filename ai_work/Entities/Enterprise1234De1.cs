namespace DucManhJr1234.Entities;

public class Enterprise1234De1
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TaxCode { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public ICollection<EnterpriseProduct1234De1> EnterpriseProducts { get; set; } = new List<EnterpriseProduct1234De1>();
}
