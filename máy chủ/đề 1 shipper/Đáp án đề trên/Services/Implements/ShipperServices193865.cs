using ApiWebBasicPlatFrom.Context;
using ApiWebBasicPlatFrom.Dtos.Shared;
using ApiWebCoin.Exceptions;
using Microsoft.EntityFrameworkCore;
using Nguyen_Khanh_Thu_193865.Dtos;
using Nguyen_Khanh_Thu_193865.Dtos.Shipper193865De3Dto;
using Nguyen_Khanh_Thu_193865.Entites;
using Nguyen_Khanh_Thu_193865.Services.Interfaces;

namespace Nguyen_Khanh_Thu_193865.Services.Implements
{
    public class ShipperServices193865 : IShipperServices193865De3
    {
        private readonly ApplicationDbContext _context;

        public ShipperServices193865(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(CreateShipper193865De3Dtos input)
        {
            if (_context.Shipper193865De3s.Any(c => c.Ten == input.Ten))
            {
                throw new UserFriendlyExceptions($"{input.Ten} đã tồn tại");
            }
            if (_context.Shipper193865De3s.Any(c => c.MaShipper == input.MaShipper))
            {
                throw new UserFriendlyExceptions($"{input.MaShipper} đã tồn tại");
            }

            _context.Shipper193865De3s.Add(
                new Shipper193865De3
                {
                    CCCD = input.CCCD,
                    MaShipper = input.MaShipper,
                    NgayThamGia = input.NgayThamGia,
                    Ten = input.Ten
                }
            );
            _context.SaveChanges();
        }

        public void Delete(int IdShipper)
        {
            var shipper = _context.Shipper193865De3s.FirstOrDefault(c => c.Id.Equals(IdShipper));
            if (shipper == null)
            {
                throw new UserFriendlyExceptions($"không tìm thấyshipper nào có Id là{IdShipper}");
            }
            _context.Shipper193865De3s.Remove(shipper);
            _context.SaveChanges();
        }

        public PageResultDto<List<Shipper193865De3>> GetAll(FilterDto input)
        {
            var shipperQuery = _context.Shipper193865De3s.AsQueryable();
            if (input.Keyword != null)
            {
                shipperQuery = shipperQuery.Where(s => s.Ten.ToLower().Contains(input.Keyword));
            }
            int totalItem = shipperQuery.Count();
            shipperQuery = shipperQuery
                .Skip(input.PageSize * (input.PageIndex - 1))
                .Take(input.PageSize);

            return new PageResultDto<List<Shipper193865De3>>
            {
                Items = shipperQuery.ToList(),
                TotalItem = totalItem,
            };
        }

        public List<ProductDto> GetProductMaxByShipper(int idShipper)
        {
            var query =
                from shipperProduct in _context.ShipperProduct193865De3s
                join product in _context.Product193865De3s
                    on shipperProduct.ProductId equals product.Id
                where shipperProduct.Id.Equals(idShipper)
                select shipperProduct;
            var productsMax = query.Max(s => s.SoLuong);

            var result =
                from q in query
                join p in _context.Product193865De3s on q.ProductId equals p.Id
                where q.SoLuong == productsMax
                select new ProductDto { ProductId = p.MaProduct, TenProduct = p.TenProduct };
            result = result.OrderBy(p => p.ProductId);

            return result.ToList();

            throw new NotImplementedException();
        }

        public void Update(UpdateShipper193865Dto input)
        {
            var shipper = _context.Shipper193865De3s.FirstOrDefault(c => c.Id.Equals(input.Id));
            if (shipper == null)
            {
                throw new UserFriendlyExceptions($"không tìm thấy shipper nào có Id là{input.Id}");
            }
            if (_context.Shipper193865De3s.Any(c => c.Ten == input.Ten))
            {
                throw new UserFriendlyExceptions($"{input.Ten} đã tồn tại");
            }
            if (_context.Shipper193865De3s.Any(c => c.MaShipper == input.MaShipper))
            {
                throw new UserFriendlyExceptions($"{input.MaShipper} đã tồn tại");
            }
            shipper.Ten = input.Ten;
            shipper.MaShipper = input.MaShipper;
            shipper.NgayThamGia = input.NgayThamGia;
            shipper.CCCD = input.CCCD;
            _context.SaveChanges();
        }
    }
}
