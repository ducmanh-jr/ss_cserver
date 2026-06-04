using Microsoft.AspNetCore.Mvc;
using NguyenDucManh0210668.Constants;
using NguyenDucManh0210668.Dtos.NhanViens;
using NguyenDucManh0210668.Services.Interfaces;
using NguyenDucManh0210668.Utils;

namespace NguyenDucManh0210668.Controllers;

[ApiController]
[Route("api/nhan-viens")]
public class NhanVienController0210668De1 : ControllerBase
{
    private readonly INhanVienService0210668De1 _nhanVienService;

    public NhanVienController0210668De1(INhanVienService0210668De1 nhanVienService)
    {
        _nhanVienService = nhanVienService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] NhanVienCreateDto0210668De1 input)
    {
        var result = await _nhanVienService.CreateAsync(input);
        return Ok(ApiResponse0210668De1<NhanVienDto0210668De1>.Success(result, MessageConstants0210668De1.Created));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] NhanVienUpdateDto0210668De1 input)
    {
        var result = await _nhanVienService.UpdateAsync(input);
        return Ok(ApiResponse0210668De1<NhanVienDto0210668De1>.Success(result, MessageConstants0210668De1.Updated));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromBody] NhanVienDeleteDto0210668De1 input)
    {
        await _nhanVienService.DeleteAsync(input);
        return Ok(ApiResponse0210668De1<object>.Success(null, MessageConstants0210668De1.Deleted));
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedAsync([FromQuery] NhanVienFilterDto0210668De1 input)
    {
        var result = await _nhanVienService.GetPagedAsync(input);
        return Ok(ApiResponse0210668De1<PagedResult0210668De1<NhanVienDto0210668De1>>.Success(result, MessageConstants0210668De1.Success));
    }

    [HttpGet("{id:int}/du-ans-theo-so-gio")]
    public async Task<IActionResult> GetDuAnsTheoSoGioNhieuNhatAsync(int id)
    {
        var result = await _nhanVienService.GetDuAnsTheoSoGioNhieuNhatAsync(id);
        return Ok(ApiResponse0210668De1<object>.Success(result, MessageConstants0210668De1.Success));
    }
}
