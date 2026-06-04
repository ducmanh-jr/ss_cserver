using ApiWebBasicPlatFrom.Dtos.Shared;
using Nguyen_Khanh_Thu_193865.Dtos;
using Nguyen_Khanh_Thu_193865.Dtos.Shipper193865De3Dto;
using Nguyen_Khanh_Thu_193865.Entites;

namespace Nguyen_Khanh_Thu_193865.Services.Interfaces
{
    public interface IShipperServices193865De3
    {
        void Create(CreateShipper193865De3Dtos input);

        void Update(UpdateShipper193865Dto input);

        void Delete(int IdShipper);

        PageResultDto<List<Shipper193865De3>> GetAll(FilterDto input);

        List<ProductDto> GetProductMaxByShipper(int idShipper);
    }
}
