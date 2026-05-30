using ConstructionMaterialsApi.Models.Dtos;

namespace ConstructionMaterialsApi.Services.Interfaces
{
    /// <summary>
    /// Interface chung cho service quản lý vật tư
    /// Controller chỉ phụ thuộc interface này, không phụ thuộc class cụ thể
    /// </summary>
    public interface IMaterialService
    {
        /// <summary>
        /// Tên implementation đang được sử dụng (Method Syntax Style / Query Syntax Style)
        /// </summary>
        string ImplementationName { get; }

        /// <summary>
        /// Lấy danh sách tất cả vật tư (inner join với supplier)
        /// </summary>
        IEnumerable<MaterialDto> GetAll();

        /// <summary>
        /// Lấy chi tiết vật tư theo id
        /// </summary>
        MaterialDetailDto GetById(int id);

        /// <summary>
        /// Lấy danh sách vật tư bằng Inner Join (chỉ lấy vật tư có nhà cung cấp)
        /// </summary>
        IEnumerable<MaterialDto> GetAllInnerJoin();

        /// <summary>
        /// Lấy danh sách vật tư bằng Left Join (lấy tất cả kể cả không có nhà cung cấp)
        /// </summary>
        IEnumerable<MaterialDto> GetAllLeftJoin();
    }
}
