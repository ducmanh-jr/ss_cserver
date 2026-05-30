using ConstructionMaterialsApi.Models.Common;
using ConstructionMaterialsApi.Models.Dtos;
using ConstructionMaterialsApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionMaterialsApi.Controllers
{
    /// <summary>
    /// Controller quản lý vật tư - giữ thật mỏng
    /// Chỉ nhận request → gọi service → trả response chuẩn hoá
    /// Không viết logic nghiệp vụ, join, where, select trong controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _service;

        public MaterialsController(IMaterialService service)
        {
            _service = service;
        }

        /// <summary>
        /// GET /api/materials
        /// Trả danh sách vật tư sau khi join với nhà cung cấp
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _service.GetAll();
            return Ok(ApiResponse<IEnumerable<MaterialDto>>.SuccessResponse(data));
        }

        /// <summary>
        /// GET /api/materials/{id}
        /// Trả chi tiết 1 vật tư theo id
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _service.GetById(id);
            return Ok(ApiResponse<MaterialDetailDto>.SuccessResponse(data));
        }

        /// <summary>
        /// GET /api/materials/inner-join
        /// Trả kết quả dùng inner join (chỉ vật tư có nhà cung cấp)
        /// </summary>
        [HttpGet("inner-join")]
        public IActionResult GetAllInnerJoin()
        {
            var data = _service.GetAllInnerJoin();
            return Ok(ApiResponse<IEnumerable<MaterialDto>>.SuccessResponse(data));
        }

        /// <summary>
        /// GET /api/materials/left-join
        /// Trả kết quả dùng left join (tất cả vật tư, kể cả không có nhà cung cấp)
        /// </summary>
        [HttpGet("left-join")]
        public IActionResult GetAllLeftJoin()
        {
            var data = _service.GetAllLeftJoin();
            return Ok(ApiResponse<IEnumerable<MaterialDto>>.SuccessResponse(data));
        }
    }
}
