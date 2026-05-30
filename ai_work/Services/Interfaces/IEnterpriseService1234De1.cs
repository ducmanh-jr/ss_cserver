using DucManhJr1234.Dtos.Common;
using DucManhJr1234.Dtos.Enterprises;
using DucManhJr1234.Dtos.Products;

namespace DucManhJr1234.Services.Interfaces;

public interface IEnterpriseService1234De1
{
    Task<EnterpriseDto1234De1> CreateAsync(CreateEnterpriseDto1234De1 input);

    Task<EnterpriseDto1234De1> UpdateAsync(int id, UpdateEnterpriseDto1234De1 input);

    Task DeleteAsync(int id);

    Task<PagedResultDto1234De1<EnterpriseDto1234De1>> GetListAsync(FilterEnterpriseDto1234De1 input);

    Task<List<TopProductDto1234De1>> GetTopProductsAsync(int enterpriseId);
}
