using DucManhJr1234.Constants;
using DucManhJr1234.DbContexts;
using DucManhJr1234.Dtos.Common;
using DucManhJr1234.Dtos.Enterprises;
using DucManhJr1234.Dtos.Products;
using DucManhJr1234.Entities;
using DucManhJr1234.Exceptions;
using DucManhJr1234.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DucManhJr1234.Services.Implements;

public class EnterpriseService1234De1 : IEnterpriseService1234De1
{
    private readonly AppDbContext1234De1 _dbContext;

    public EnterpriseService1234De1(AppDbContext1234De1 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EnterpriseDto1234De1> CreateAsync(CreateEnterpriseDto1234De1 input)
    {
        await ValidateEnterpriseUniqueAsync(input.Name, input.TaxCode);

        var enterprise = new Enterprise1234De1
        {
            Name = input.Name,
            TaxCode = input.TaxCode,
            Address = input.Address
        };

        _dbContext.Enterprises.Add(enterprise);
        await _dbContext.SaveChangesAsync();

        return MapToEnterpriseDto(enterprise);
    }

    public async Task<EnterpriseDto1234De1> UpdateAsync(int id, UpdateEnterpriseDto1234De1 input)
    {
        var enterprise = await _dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
        if (enterprise == null)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);
        }

        await ValidateEnterpriseUniqueAsync(input.Name, input.TaxCode, id);

        enterprise.Name = input.Name;
        enterprise.TaxCode = input.TaxCode;
        enterprise.Address = input.Address;

        await _dbContext.SaveChangesAsync();

        return MapToEnterpriseDto(enterprise);
    }

    public async Task DeleteAsync(int id)
    {
        var enterprise = await _dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
        if (enterprise == null)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);
        }

        _dbContext.Enterprises.Remove(enterprise);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PagedResultDto1234De1<EnterpriseDto1234De1>> GetListAsync(FilterEnterpriseDto1234De1 input)
    {
        if (input.PageSize <= 0)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.PageSizeInvalid);
        }

        if (input.PageIndex <= 0)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.PageIndexInvalid);
        }

        var query = _dbContext.Enterprises.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim();
            query = query.Where(e => e.Name.Contains(keyword) || e.TaxCode.Contains(keyword));
        }

        var totalItems = await query.CountAsync();
        var items = await query
            .OrderBy(e => e.Id)
            .Skip((input.PageIndex - 1) * input.PageSize)
            .Take(input.PageSize)
            .Select(e => new EnterpriseDto1234De1
            {
                Id = e.Id,
                Name = e.Name,
                TaxCode = e.TaxCode,
                Address = e.Address
            })
            .ToListAsync();

        return new PagedResultDto1234De1<EnterpriseDto1234De1>
        {
            TotalItems = totalItems,
            PageSize = input.PageSize,
            PageIndex = input.PageIndex,
            Items = items
        };
    }

    public async Task<List<TopProductDto1234De1>> GetTopProductsAsync(int enterpriseId)
    {
        var enterpriseExists = await _dbContext.Enterprises.AnyAsync(e => e.Id == enterpriseId);
        if (!enterpriseExists)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNotFound);
        }

        var maxQuantity = await _dbContext.EnterpriseProducts
            .Where(ep => ep.EnterpriseId == enterpriseId)
            .MaxAsync(ep => (int?)ep.Quantity);

        if (maxQuantity == null)
        {
            return new List<TopProductDto1234De1>();
        }

        return await _dbContext.EnterpriseProducts
            .AsNoTracking()
            .Where(ep => ep.EnterpriseId == enterpriseId && ep.Quantity == maxQuantity)
            .Select(ep => new TopProductDto1234De1
            {
                Name = ep.Product.Name,
                Code = ep.Product.Code
            })
            .ToListAsync();
    }

    private async Task ValidateEnterpriseUniqueAsync(string name, string taxCode, int? currentId = null)
    {
        var nameExists = await _dbContext.Enterprises.AnyAsync(e =>
            e.Name == name && (!currentId.HasValue || e.Id != currentId.Value));

        if (nameExists)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseNameExists);
        }

        var taxCodeExists = await _dbContext.Enterprises.AnyAsync(e =>
            e.TaxCode == taxCode && (!currentId.HasValue || e.Id != currentId.Value));

        if (taxCodeExists)
        {
            throw new UserFriendlyException(ErrorMessages1234De1.EnterpriseTaxCodeExists);
        }
    }

    private static EnterpriseDto1234De1 MapToEnterpriseDto(Enterprise1234De1 enterprise)
    {
        return new EnterpriseDto1234De1
        {
            Id = enterprise.Id,
            Name = enterprise.Name,
            TaxCode = enterprise.TaxCode,
            Address = enterprise.Address
        };
    }
}
