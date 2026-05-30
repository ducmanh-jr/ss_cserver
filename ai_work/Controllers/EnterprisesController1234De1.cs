using DucManhJr1234.Constants;
using DucManhJr1234.Dtos.Enterprises;
using DucManhJr1234.Exceptions;
using DucManhJr1234.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DucManhJr1234.Controllers;

[ApiController]
[Route("api/enterprises")]
public class EnterprisesController1234De1 : ControllerBase
{
    private readonly IEnterpriseService1234De1 _enterpriseService;

    public EnterprisesController1234De1(IEnterpriseService1234De1 enterpriseService)
    {
        _enterpriseService = enterpriseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnterpriseDto1234De1 input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _enterpriseService.CreateAsync(input);
            return Ok(new
            {
                Message = SuccessMessages1234De1.CreateEnterpriseSuccess,
                Data = result
            });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ErrorMessages1234De1.SystemError });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEnterpriseDto1234De1 input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _enterpriseService.UpdateAsync(id, input);
            return Ok(new
            {
                Message = SuccessMessages1234De1.UpdateEnterpriseSuccess,
                Data = result
            });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ErrorMessages1234De1.SystemError });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _enterpriseService.DeleteAsync(id);
            return Ok(new { Message = SuccessMessages1234De1.DeleteEnterpriseSuccess });
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ErrorMessages1234De1.SystemError });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] FilterEnterpriseDto1234De1 input)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _enterpriseService.GetListAsync(input);
            return Ok(result);
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ErrorMessages1234De1.SystemError });
        }
    }

    [HttpGet("{enterpriseId:int}/top-products")]
    public async Task<IActionResult> GetTopProducts(int enterpriseId)
    {
        try
        {
            var result = await _enterpriseService.GetTopProductsAsync(enterpriseId);
            return Ok(result);
        }
        catch (UserFriendlyException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ErrorMessages1234De1.SystemError });
        }
    }
}
