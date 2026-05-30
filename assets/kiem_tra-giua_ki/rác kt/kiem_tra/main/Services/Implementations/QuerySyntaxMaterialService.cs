using ConstructionMaterialsApi.Data;
using ConstructionMaterialsApi.Exceptions;
using ConstructionMaterialsApi.Models.Dtos;
using ConstructionMaterialsApi.Services.Interfaces;

namespace ConstructionMaterialsApi.Services.Implementations
{
    /// <summary>
    /// Implementation sử dụng LINQ Query Syntax (from, join, on, equals, where, select)
    /// </summary>
    public class QuerySyntaxMaterialService : IMaterialService
    {
        public string ImplementationName => "Query Syntax Style";

        /// <summary>
        /// Lấy danh sách vật tư - dùng Inner Join bằng Query Syntax
        /// </summary>
        public IEnumerable<MaterialDto> GetAll()
        {
            var result = from material in SeedData.Materials
                         join supplier in SeedData.Suppliers
                         on material.SupplierId equals supplier.Id
                         select new MaterialDto
                         {
                             Id = material.Id,
                             Name = material.Name,
                             Unit = material.Unit,
                             UnitPrice = material.UnitPrice,
                             SupplierName = supplier.Name,
                             SupplierAddress = supplier.Address,
                             SourceImplementation = ImplementationName
                         };

            return result;
        }

        /// <summary>
        /// Lấy chi tiết vật tư theo id - dùng Left Join bằng Query Syntax
        /// Nếu không tìm thấy sẽ throw NotFoundException
        /// </summary>
        public MaterialDetailDto GetById(int id)
        {
            var query = from material in SeedData.Materials
                        where material.Id == id
                        join supplier in SeedData.Suppliers
                        on material.SupplierId equals supplier.Id into supplierGroup
                        from supplier in supplierGroup.DefaultIfEmpty()
                        select new MaterialDetailDto
                        {
                            Id = material.Id,
                            Name = material.Name,
                            Unit = material.Unit,
                            UnitPrice = material.UnitPrice,
                            SupplierName = supplier != null ? supplier.Name : "Chưa có nhà cung cấp",
                            SupplierAddress = supplier?.Address,
                            SourceImplementation = ImplementationName
                        };

            var result = query.FirstOrDefault();

            if (result == null)
            {
                throw new UserFriendlyException($"Vật tư với ID {id} không tồn tại trong hệ thống.");
            }

            return result;
        }

        /// <summary>
        /// Inner Join - chỉ lấy vật tư CÓ nhà cung cấp (Query Syntax)
        /// Vật tư không có SupplierId sẽ bị loại khỏi kết quả
        /// </summary>
        public IEnumerable<MaterialDto> GetAllInnerJoin()
        {
            var result = from material in SeedData.Materials
                         join supplier in SeedData.Suppliers
                         on material.SupplierId equals supplier.Id
                         select new MaterialDto
                         {
                             Id = material.Id,
                             Name = material.Name,
                             Unit = material.Unit,
                             UnitPrice = material.UnitPrice,
                             SupplierName = supplier.Name,
                             SupplierAddress = supplier.Address,
                             SourceImplementation = ImplementationName
                         };

            return result;
        }

        /// <summary>
        /// Left Join - lấy TẤT CẢ vật tư kể cả không có nhà cung cấp (Query Syntax)
        /// Sử dụng join ... into ... DefaultIfEmpty()
        /// </summary>
        public IEnumerable<MaterialDto> GetAllLeftJoin()
        {
            var result = from material in SeedData.Materials
                         join supplier in SeedData.Suppliers
                         on material.SupplierId equals supplier.Id into supplierGroup
                         from supplier in supplierGroup.DefaultIfEmpty()
                         select new MaterialDto
                         {
                             Id = material.Id,
                             Name = material.Name,
                             Unit = material.Unit,
                             UnitPrice = material.UnitPrice,
                             SupplierName = supplier != null ? supplier.Name : "Chưa có nhà cung cấp",
                             SupplierAddress = supplier?.Address,
                             SourceImplementation = ImplementationName
                         };

            return result;
        }
    }
}
