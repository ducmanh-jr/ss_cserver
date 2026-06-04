using Microsoft.EntityFrameworkCore;
using NguyenDucManh0210668.DbContexts;
using NguyenDucManh0210668.Dtos.DuAns;
using NguyenDucManh0210668.Entities;
using NguyenDucManh0210668.Exceptions;
using NguyenDucManh0210668.Services.Interfaces;

namespace NguyenDucManh0210668.Services.Implements;

public class DuAnService0210668De1 : IDuAnService0210668De1
{
    private readonly AppDbContext0210668De1 _dbContext;

    public DuAnService0210668De1(AppDbContext0210668De1 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DuAnDto0210668De1> CreateAsync(DuAnCreateDto0210668De1 input)
    {
        var duplicatedTenDuAn = await _dbContext.DuAns.AnyAsync(item => item.TenDuAn == input.TenDuAn);
        if (duplicatedTenDuAn)
        {
            throw new UserFriendlyException0210668De1("Tên dự án đã tồn tại.");
        }

        var duplicatedMaDuAn = await _dbContext.DuAns.AnyAsync(item => item.MaDuAn == input.MaDuAn);
        if (duplicatedMaDuAn)
        {
            throw new UserFriendlyException0210668De1("Mã dự án đã tồn tại.");
        }

        var duAn = new DuAn0210668De1
        {
            TenDuAn = input.TenDuAn,
            MaDuAn = input.MaDuAn
        };

        _dbContext.DuAns.Add(duAn);
        await _dbContext.SaveChangesAsync();

        return MapToDto(duAn);
    }

    public async Task<IReadOnlyList<DuAnDto0210668De1>> GetAllAsync()
    {
        return await _dbContext.DuAns
            .AsNoTracking()
            .OrderBy(item => item.TenDuAn)
            .Select(item => new DuAnDto0210668De1
            {
                Id = item.Id,
                TenDuAn = item.TenDuAn,
                MaDuAn = item.MaDuAn
            })
            .ToListAsync();
    }

    private static DuAnDto0210668De1 MapToDto(DuAn0210668De1 duAn)
    {
        return new DuAnDto0210668De1
        {
            Id = duAn.Id,
            TenDuAn = duAn.TenDuAn,
            MaDuAn = duAn.MaDuAn
        };
    }
}
