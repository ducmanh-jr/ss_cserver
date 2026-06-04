using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace nguyenducmanh0210668.Exceptions
{
    public class ExceptionMiddleware0210668De1
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware0210668De1> _logger;

        public ExceptionMiddleware0210668De1(RequestDelegate next, ILogger<ExceptionMiddleware0210668De1> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserFriendlyException0210668De1 ex)
            {
                _logger.LogWarning(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Lỗi hệ thống");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = "Lỗi hệ thống", details = ex.Message });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
