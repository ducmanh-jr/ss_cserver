using ApiWebCoin.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebBasicPlatFrom.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected ILogger _logger;

        public ApiControllerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected ActionResult HandleException(Exception ex)
        {
            //kiểm tra xem đối tượng trả ra có phải là một thể hiện của class UserFriendlyException
            if (ex is UserFriendlyExceptions)
            {
                // trả về http status code là 400
                return BadRequest(
                    new ApiWebCoin.Dtos.Exceptions.ResponseError() { Message = ex.Message, }
                );
            }
            //những lỗi khác thì sẽ được log lại
            _logger.LogError(ex, ex.Message);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ApiWebCoin.Dtos.Exceptions.ResponseError() { Message = ex.Message, }
            );
        }
    }
}
