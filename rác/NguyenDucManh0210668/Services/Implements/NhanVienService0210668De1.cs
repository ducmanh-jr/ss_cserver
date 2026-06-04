using Microsoft.EntityFrameworkCore;
using NguyenDucManh0210668.Constants;
using NguyenDucManh0210668.DbContexts;
using NguyenDucManh0210668.Dtos.DuAns;
using NguyenDucManh0210668.Dtos.NhanViens;
using NguyenDucManh0210668.Entities;
using NguyenDucManh0210668.Exceptions;
using NguyenDucManh0210668.Services.Interfaces;
using NguyenDucManh0210668.Utils;

namespace NguyenDucManh0210668.Services.Implements;

public class NhanVienService0210668De1 : INhanVienService0210668De1
{
    private readonly AppDbContext0210668De1 _dbContext;

    public NhanVienService0210668De1(AppDbContext0210668De1 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NhanVienDto0210668De1> CreateAsync(NhanVienCreateDto0210668De1 input)
    {
        await EnsureUniqueNhanVienAsync(input.MaNhanVien, input.Email, null);

        var nhanVien = new NhanVien0210668De1
        {
            TenNhanVien = input.TenNhanVien,
            MaNhanVien = input.MaNhanVien,
            Email = input.Email
        };

        _dbContext.NhanViens.Add(nhanVien);
        await _dbContext.SaveChangesAsync();

        return MapToDto(nhanVien);
    }

    public async Task<NhanVienDto0210668De1> UpdateAsync(NhanVienUpdateDto0210668De1 input)
    {
        var nhanVien = await _dbContext.NhanViens.FirstOrDefaultAsync(item => item.Id == input.Id);
        if (nhanVien is null)
        {
            throw new UserFriendlyException0210668De1(MessageConstants0210668De1.NotFoundNhanVien);
        }

        await EnsureUniqueNhanVienAsync(input.MaNhanVien, input.Email, input.Id);

        nhanVien.TenNhanVien = input.TenNhanVien;
        nhanVien.MaNhanVien = input.MaNhanVien;
        nhanVien.Email = input.Email;
        await _dbContext.SaveChangesAsync();

        return MapToDto(nhanVien);
    }

    public async Task DeleteAsync(NhanVienDeleteDto0210668De1 input)
    {
        var nhanVien = await _dbContext.NhanViens.FirstOrDefaultAsync(item => item.Id == input.Id);
        if (nhanVien is null)
        {
            throw new UserFriendlyException0210668De1(MessageConstants0210668De1.NotFoundNhanVien);
        }

        _dbContext.NhanViens.Remove(nhanVien);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PagedResult0210668De1<NhanVienDto0210668De1>> GetPagedAsync(NhanVienFilterDto0210668De1 input)
    {
        var query = _dbContext.NhanViens.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            query = query.Where(item => item.TenNhanVien.Contains(input.Keyword) || item.MaNhanVien.Contains(input.Keyword));
        }

        var totalItems = await query.CountAsync();
        var items = await query
            .OrderBy(item => item.TenNhanVien)
            .ThenBy(item => item.MaNhanVien)
            .Skip((input.PageIndex - 1) * input.PageSize)
            .Take(input.PageSize)
            .Select(item => new NhanVienDto0210668De1
            {
                Id = item.Id,
                TenNhanVien = item.TenNhanVien,
                MaNhanVien = item.MaNhanVien,
                Email = item.Email
            })
            .ToListAsync();

        return new PagedResult0210668De1<NhanVienDto0210668De1>
        {
            Items = items,
            TotalItems = totalItems,
            PageIndex = input.PageIndex,
            PageSize = input.PageSize,
            TotalPages = totalItems == 0 ? 0 : (int)Math.Ceiling(totalItems / (double)input.PageSize)
        };
    }

    public async Task<IReadOnlyList<DuAnTheoSoGioDto0210668De1>> GetDuAnsTheoSoGioNhieuNhatAsync(int nhanVienId)
    {
        var existsNhanVien = await _dbContext.NhanViens.AnyAsync(item => item.Id == nhanVienId);
        if (!existsNhanVien)
        {
            throw new UserFriendlyException0210668De1(MessageConstants0210668De1.NotFoundNhanVien);
        }

        return await _dbContext.PhanCongs
            .AsNoTracking()
            .Where(item => item.NhanVienId == nhanVienId)
            .OrderByDescending(item => item.SoGioLamViec)
            .ThenBy(item => item.DuAn!.TenDuAn)
            .Select(item => new DuAnTheoSoGioDto0210668De1
            {
                TenDuAn = item.DuAn!.TenDuAn,
                MaDuAn = item.DuAn.MaDuAn,
                SoGioLamViec = item.SoGioLamViec
            })
            .ToListAsync();
    }

    private async Task EnsureUniqueNhanVienAsync(string maNhanVien, string email, int? ignoreId)
    {
        var duplicatedMaNhanVien = await _dbContext.NhanViens
            .AnyAsync(item => item.MaNhanVien == maNhanVien && (!ignoreId.HasValue || item.Id != ignoreId.Value));
        if (duplicatedMaNhanVien)
        {
            throw new UserFriendlyException0210668De1("Mã nhân viên đã tồn tại.");
        }

        var duplicatedEmail = await _dbContext.NhanViens
            .AnyAsync(item => item.Email == email && (!ignoreId.HasValue || item.Id != ignoreId.Value));
        if (duplicatedEmail)
        {
            throw new UserFriendlyException0210668De1("Email đã tồn tại.");
        }
    }

    private static NhanVienDto0210668De1 MapToDto(NhanVien0210668De1 nhanVien)
    {
        return new NhanVienDto0210668De1
        {
            Id = nhanVien.Id,
            TenNhanVien = nhanVien.TenNhanVien,
            MaNhanVien = nhanVien.MaNhanVien,
            Email = nhanVien.Email
        };
    }
}
