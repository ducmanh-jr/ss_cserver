using Microsoft.EntityFrameworkCore;
using NguyenDucManh0210668.Constants;
using NguyenDucManh0210668.DbContexts;
using NguyenDucManh0210668.Dtos.PhanCongs;
using NguyenDucManh0210668.Entities;
using NguyenDucManh0210668.Exceptions;
using NguyenDucManh0210668.Services.Interfaces;

namespace NguyenDucManh0210668.Services.Implements;

public class PhanCongService0210668De1 : IPhanCongService0210668De1
{
    private readonly AppDbContext0210668De1 _dbContext;

    public PhanCongService0210668De1(AppDbContext0210668De1 dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PhanCongDto0210668De1> CreateOrUpdateAsync(PhanCongCreateOrUpdateDto0210668De1 input)
    {
        var existsNhanVien = await _dbContext.NhanViens.AnyAsync(item => item.Id == input.NhanVienId);
        if (!existsNhanVien)
        {
            throw new UserFriendlyException0210668De1(MessageConstants0210668De1.NotFoundNhanVien);
        }

        var existsDuAn = await _dbContext.DuAns.AnyAsync(item => item.Id == input.DuAnId);
        if (!existsDuAn)
        {
            throw new UserFriendlyException0210668De1(MessageConstants0210668De1.NotFoundDuAn);
        }

        var phanCong = await _dbContext.PhanCongs
            .FirstOrDefaultAsync(item => item.NhanVienId == input.NhanVienId && item.DuAnId == input.DuAnId);

        if (phanCong is null)
        {
            phanCong = new PhanCong0210668De1
            {
                NhanVienId = input.NhanVienId,
                DuAnId = input.DuAnId,
                SoGioLamViec = input.SoGioLamViec
            };
            _dbContext.PhanCongs.Add(phanCong);
        }
        else
        {
            phanCong.SoGioLamViec = input.SoGioLamViec;
        }

        await _dbContext.SaveChangesAsync();

        return await GetByIdAsync(phanCong.Id);
    }

    public async Task<IReadOnlyList<PhanCongDto0210668De1>> GetAllAsync()
    {
        return await _dbContext.PhanCongs
            .AsNoTracking()
            .OrderBy(item => item.NhanVien!.TenNhanVien)
            .ThenBy(item => item.DuAn!.TenDuAn)
            .Select(item => new PhanCongDto0210668De1
            {
                Id = item.Id,
                NhanVienId = item.NhanVienId,
                TenNhanVien = item.NhanVien!.TenNhanVien,
                DuAnId = item.DuAnId,
                TenDuAn = item.DuAn!.TenDuAn,
                SoGioLamViec = item.SoGioLamViec
            })
            .ToListAsync();
    }

    private async Task<PhanCongDto0210668De1> GetByIdAsync(int id)
    {
        var phanCong = await _dbContext.PhanCongs
            .AsNoTracking()
            .Where(item => item.Id == id)
            .Select(item => new PhanCongDto0210668De1
            {
                Id = item.Id,
                NhanVienId = item.NhanVienId,
                TenNhanVien = item.NhanVien!.TenNhanVien,
                DuAnId = item.DuAnId,
                TenDuAn = item.DuAn!.TenDuAn,
                SoGioLamViec = item.SoGioLamViec
            })
            .FirstOrDefaultAsync();

        if (phanCong is null)
        {
            throw new UserFriendlyException0210668De1(MessageConstants0210668De1.NotFoundPhanCong);
        }

        return phanCong;
    }
}
