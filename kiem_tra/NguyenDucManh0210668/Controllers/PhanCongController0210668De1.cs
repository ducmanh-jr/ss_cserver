using Microsoft.AspNetCore.Mvc;
using NguyenDucManh0210668.Constants;
using NguyenDucManh0210668.Dtos.PhanCongs;
using NguyenDucManh0210668.Services.Interfaces;
using NguyenDucManh0210668.Utils;

namespace NguyenDucManh0210668.Controllers;

[ApiController]
[Route("api/phan-congs")]
public class PhanCongController0210668De1 : ControllerBase
{
    private readonly IPhanCongService0210668De1 _phanCongService;

    public PhanCongController0210668De1(IPhanCongService0210668De1 phanCongService)
    {
        _phanCongService = phanCongService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateAsync([FromBody] PhanCongCreateOrUpdateDto0210668De1 input)
    {
        var result = await _phanCongService.CreateOrUpdateAsync(input);
        return Ok(ApiResponse0210668De1<PhanCongDto0210668De1>.Success(result, MessageConstants0210668De1.Success));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _phanCongService.GetAllAsync();
        return Ok(ApiResponse0210668De1<object>.Success(result, MessageConstants0210668De1.Success));
    }
}
