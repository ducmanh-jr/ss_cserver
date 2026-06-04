using NguyenDucManh0210668.Dtos.DuAns;

namespace NguyenDucManh0210668.Services.Interfaces;

public interface IDuAnService0210668De1
{
    Task<DuAnDto0210668De1> CreateAsync(DuAnCreateDto0210668De1 input);
    Task<IReadOnlyList<DuAnDto0210668De1>> GetAllAsync();
}
