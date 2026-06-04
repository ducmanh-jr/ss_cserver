using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nguyenducmanh0210668.Dtos;
using nguyenducmanh0210668.Services.Interfaces;

namespace nguyenducmanh0210668.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Enterprise0210668De1Controller : ControllerBase
    {
        private readonly IEnterpriseService0210668De1 _enterpriseService;

        public Enterprise0210668De1Controller(IEnterpriseService0210668De1 enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnterpriseDto0210668De1 dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _enterpriseService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEnterpriseDto0210668De1 dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _enterpriseService.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _enterpriseService.DeleteAsync(id);
            return Ok(new { success = result });
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] FilterDto0210668De1 filter)
        {
            var result = await _enterpriseService.GetPagedAsync(filter);
            return Ok(result);
        }

        [HttpGet("{id}/most-imported-products")]
        public async Task<IActionResult> GetMostImportedProducts(int id)
        {
            var result = await _enterpriseService.GetMostImportedProductsAsync(id);
            return Ok(result);
        }
    }
}
