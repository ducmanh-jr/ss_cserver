using System.Collections.Generic;
using System.Threading.Tasks;
using nguyenducmanh0210668.Dtos;

namespace nguyenducmanh0210668.Services.Interfaces
{
    public interface IEnterpriseService0210668De1
    {
        Task<EnterpriseDto0210668De1> CreateAsync(CreateEnterpriseDto0210668De1 dto);
        Task<EnterpriseDto0210668De1> UpdateAsync(int id, UpdateEnterpriseDto0210668De1 dto);
        Task<bool> DeleteAsync(int id);
        Task<PagedResultDto0210668De1<EnterpriseDto0210668De1>> GetPagedAsync(FilterDto0210668De1 filter);
        Task<List<ProductDto0210668De1>> GetMostImportedProductsAsync(int enterpriseId);
    }
}
