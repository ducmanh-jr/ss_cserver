using NguyenDucManh0210668.Dtos.PhanCongs;

namespace NguyenDucManh0210668.Services.Interfaces;

public interface IPhanCongService0210668De1
{
    Task<PhanCongDto0210668De1> CreateOrUpdateAsync(PhanCongCreateOrUpdateDto0210668De1 input);
    Task<IReadOnlyList<PhanCongDto0210668De1>> GetAllAsync();
}
