using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using nguyenducmanh0210668.Constants;
using nguyenducmanh0210668.DbContexts;
using nguyenducmanh0210668.Dtos;
using nguyenducmanh0210668.Entities;
using nguyenducmanh0210668.Exceptions;
using nguyenducmanh0210668.Services.Interfaces;

namespace nguyenducmanh0210668.Services.Implements
{
    public class EnterpriseService0210668De1 : IEnterpriseService0210668De1
    {
        private readonly AppDbContext0210668De1 _context;

        public EnterpriseService0210668De1(AppDbContext0210668De1 context)
        {
            _context = context;
        }

        public async Task<EnterpriseDto0210668De1> CreateAsync(CreateEnterpriseDto0210668De1 dto)
        {
            if (await _context.Enterprises.AnyAsync(e => e.Name == dto.Name))
            {
                throw new UserFriendlyException0210668De1(AppConstants0210668De1.ErrorDuplicateEnterpriseName);
            }
            if (await _context.Enterprises.AnyAsync(e => e.TaxCode == dto.TaxCode))
            {
                throw new UserFriendlyException0210668De1(AppConstants0210668De1.ErrorDuplicateEnterpriseTaxCode);
            }

            var enterprise = new Enterprise0210668De1
            {
                Name = dto.Name,
                TaxCode = dto.TaxCode,
                Address = dto.Address
            };

            _context.Enterprises.Add(enterprise);
            await _context.SaveChangesAsync();

            return new EnterpriseDto0210668De1
            {
                Id = enterprise.Id,
                Name = enterprise.Name,
                TaxCode = enterprise.TaxCode,
                Address = enterprise.Address
            };
        }

        public async Task<EnterpriseDto0210668De1> UpdateAsync(int id, UpdateEnterpriseDto0210668De1 dto)
        {
            var enterprise = await _context.Enterprises.FindAsync(id);
            if (enterprise == null)
            {
                throw new UserFriendlyException0210668De1(AppConstants0210668De1.ErrorEnterpriseNotFound);
            }

            if (await _context.Enterprises.AnyAsync(e => e.Name == dto.Name && e.Id != id))
            {
                throw new UserFriendlyException0210668De1(AppConstants0210668De1.ErrorDuplicateEnterpriseName);
            }
            if (await _context.Enterprises.AnyAsync(e => e.TaxCode == dto.TaxCode && e.Id != id))
            {
                throw new UserFriendlyException0210668De1(AppConstants0210668De1.ErrorDuplicateEnterpriseTaxCode);
            }

            enterprise.Name = dto.Name;
            enterprise.TaxCode = dto.TaxCode;
            enterprise.Address = dto.Address;

            await _context.SaveChangesAsync();

            return new EnterpriseDto0210668De1
            {
                Id = enterprise.Id,
                Name = enterprise.Name,
                TaxCode = enterprise.TaxCode,
                Address = enterprise.Address
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var enterprise = await _context.Enterprises.FindAsync(id);
            if (enterprise == null)
            {
                throw new UserFriendlyException0210668De1(AppConstants0210668De1.ErrorEnterpriseNotFound);
            }

            _context.Enterprises.Remove(enterprise);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResultDto0210668De1<EnterpriseDto0210668De1>> GetPagedAsync(FilterDto0210668De1 filter)
        {
            var query = _context.Enterprises.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                query = query.Where(e => e.Name.Contains(filter.Keyword) || e.TaxCode.Contains(filter.Keyword));
            }

            var totalItems = await query.CountAsync();
            var items = await query.OrderBy(e => e.Id)
                                   .Skip((filter.PageIndex - 1) * filter.PageSize)
                                   .Take(filter.PageSize)
                                   .Select(e => new EnterpriseDto0210668De1
                                   {
                                       Id = e.Id,
                                       Name = e.Name,
                                       TaxCode = e.TaxCode,
                                       Address = e.Address
                                   })
                                   .ToListAsync();

            return new PagedResultDto0210668De1<EnterpriseDto0210668De1>
            {
                TotalItems = totalItems,
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize,
                Items = items
            };
        }

        public async Task<List<ProductDto0210668De1>> GetMostImportedProductsAsync(int enterpriseId)
        {
            var enterprise = await _context.Enterprises.FindAsync(enterpriseId);
            if (enterprise == null)
            {
                throw new UserFriendlyException0210668De1(AppConstants0210668De1.ErrorEnterpriseNotFound);
            }

            var maxQuantity = await _context.EnterpriseProducts
                                            .Where(ep => ep.EnterpriseId == enterpriseId)
                                            .MaxAsync(ep => (int?)ep.Quantity) ?? 0;

            if (maxQuantity == 0) return new List<ProductDto0210668De1>();

            var products = await _context.EnterpriseProducts
                                         .Where(ep => ep.EnterpriseId == enterpriseId && ep.Quantity == maxQuantity)
                                         .Select(ep => new ProductDto0210668De1
                                         {
                                             Id = ep.Product.Id,
                                             Name = ep.Product.Name,
                                             Code = ep.Product.Code,
                                             ImportDate = ep.Product.ImportDate
                                         })
                                         .ToListAsync();

            return products;
        }
    }
}
