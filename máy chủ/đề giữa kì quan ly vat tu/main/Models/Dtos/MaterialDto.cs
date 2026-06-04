namespace ConstructionMaterialsApi.Models.Dtos
{
    /// <summary>
    /// DTO dùng cho danh sách vật tư (GetAll, InnerJoin, LeftJoin)
    /// </summary>
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string SourceImplementation { get; set; } = string.Empty;
    }
}
