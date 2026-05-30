using ConstructionMaterialsApi.Data;
using ConstructionMaterialsApi.Exceptions;
using ConstructionMaterialsApi.Models.Dtos;
using ConstructionMaterialsApi.Services.Interfaces;

namespace ConstructionMaterialsApi.Services.Implementations
{
    /// <summary>
    /// Implementation sử dụng LINQ Method Syntax (.Join(), .Where(), .Select(), .GroupJoin())
    /// </summary>
    public class MethodSyntaxMaterialService : IMaterialService
    {
        public string ImplementationName => "Method Syntax Style";

        /// <summary>
        /// Lấy danh sách vật tư - dùng Inner Join bằng Method Syntax
        /// </summary>
        public IEnumerable<MaterialDto> GetAll()
        {
            var result = SeedData.Materials
                .Join(
                    SeedData.Suppliers,
                    material => material.SupplierId,
                    supplier => supplier.Id,
                    (material, supplier) => new MaterialDto
                    {
                        Id = material.Id,
                        Name = material.Name,
                        Unit = material.Unit,
                        UnitPrice = material.UnitPrice,
                        SupplierName = supplier.Name,
                        SupplierAddress = supplier.Address,
                        SourceImplementation = ImplementationName
                    }
                );

            return result;
        }

        /// <summary>
        /// Lấy chi tiết vật tư theo id - dùng Left Join bằng Method Syntax
        /// Nếu không tìm thấy sẽ throw NotFoundException
        /// </summary>
        public MaterialDetailDto GetById(int id)
        {
            var result = SeedData.Materials
                .Where(m => m.Id == id)
                .GroupJoin(
                    SeedData.Suppliers,
                    material => material.SupplierId,
                    supplier => supplier.Id,
                    (material, suppliers) => new { material, suppliers }
                )
                .SelectMany(
                    x => x.suppliers.DefaultIfEmpty(),
                    (x, supplier) => new MaterialDetailDto
                    {
                        Id = x.material.Id,
                        Name = x.material.Name,
                        Unit = x.material.Unit,
                        UnitPrice = x.material.UnitPrice,
                        SupplierName = supplier != null ? supplier.Name : "Chưa có nhà cung cấp",
                        SupplierAddress = supplier?.Address,
                        SourceImplementation = ImplementationName
                    }
                )
                .FirstOrDefault();

            if (result == null)
            {
                throw new NotFoundException($"Vật tư với ID {id} không tồn tại trong hệ thống.");
            }

            return result;
        }

        /// <summary>
        /// Inner Join - chỉ lấy vật tư CÓ nhà cung cấp (Method Syntax)
        /// Vật tư không có SupplierId sẽ bị loại khỏi kết quả
        /// </summary>
        public IEnumerable<MaterialDto> GetAllInnerJoin()
        {
            var result = SeedData.Materials
                .Join(
                    SeedData.Suppliers,
                    material => material.SupplierId,
                    supplier => supplier.Id,
                    (material, supplier) => new MaterialDto
                    {
                        Id = material.Id,
                        Name = material.Name,
                        Unit = material.Unit,
                        UnitPrice = material.UnitPrice,
                        SupplierName = supplier.Name,
                        SupplierAddress = supplier.Address,
                        SourceImplementation = ImplementationName
                    }
                );

            return result;
        }

        /// <summary>
        /// Left Join - lấy TẤT CẢ vật tư kể cả không có nhà cung cấp (Method Syntax)
        /// Sử dụng GroupJoin + DefaultIfEmpty
        /// </summary>
        public IEnumerable<MaterialDto> GetAllLeftJoin()
        {
            var result = SeedData.Materials
                .GroupJoin(
                    SeedData.Suppliers,
                    material => material.SupplierId,
                    supplier => supplier.Id,
                    (material, suppliers) => new { material, suppliers }
                )
                .SelectMany(
                    x => x.suppliers.DefaultIfEmpty(),
                    (x, supplier) => new MaterialDto
                    {
                        Id = x.material.Id,
                        Name = x.material.Name,
                        Unit = x.material.Unit,
                        UnitPrice = x.material.UnitPrice,
                        SupplierName = supplier != null ? supplier.Name : "Chưa có nhà cung cấp",
                        SupplierAddress = supplier?.Address,
                        SourceImplementation = ImplementationName
                    }
                );

            return result;
        }
    }
}
