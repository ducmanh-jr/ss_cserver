using Microsoft.AspNetCore.Mvc;
using NguyenDucManh0210668.Constants;
using NguyenDucManh0210668.Dtos.DuAns;
using NguyenDucManh0210668.Services.Interfaces;
using NguyenDucManh0210668.Utils;

namespace NguyenDucManh0210668.Controllers;

[ApiController]
[Route("api/du-ans")]
public class DuAnController0210668De1 : ControllerBase
{
    private readonly IDuAnService0210668De1 _duAnService;

    public DuAnController0210668De1(IDuAnService0210668De1 duAnService)
    {
        _duAnService = duAnService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] DuAnCreateDto0210668De1 input)
    {
        var result = await _duAnService.CreateAsync(input);
        return Ok(ApiResponse0210668De1<DuAnDto0210668De1>.Success(result, MessageConstants0210668De1.Created));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _duAnService.GetAllAsync();
        return Ok(ApiResponse0210668De1<object>.Success(result, MessageConstants0210668De1.Success));
    }
}
