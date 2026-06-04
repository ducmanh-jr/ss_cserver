using NguyenDucManh0210668.Dtos.DuAns;
using NguyenDucManh0210668.Dtos.NhanViens;
using NguyenDucManh0210668.Utils;

namespace NguyenDucManh0210668.Services.Interfaces;

public interface INhanVienService0210668De1
{
    Task<NhanVienDto0210668De1> CreateAsync(NhanVienCreateDto0210668De1 input);
    Task<NhanVienDto0210668De1> UpdateAsync(NhanVienUpdateDto0210668De1 input);
    Task DeleteAsync(NhanVienDeleteDto0210668De1 input);
    Task<PagedResult0210668De1<NhanVienDto0210668De1>> GetPagedAsync(NhanVienFilterDto0210668De1 input);
    Task<IReadOnlyList<DuAnTheoSoGioDto0210668De1>> GetDuAnsTheoSoGioNhieuNhatAsync(int nhanVienId);
}
