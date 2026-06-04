namespace nguyenducmanh0210668.Entities
{
    public class EnterpriseProduct0210668De1
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public Enterprise0210668De1 Enterprise { get; set; } = null!;
        public int ProductId { get; set; }
        public Product0210668De1 Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
