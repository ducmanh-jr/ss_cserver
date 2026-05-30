using System.Net;
using System.Text.Json;
using ConstructionMaterialsApi.Exceptions;
using ConstructionMaterialsApi.Models.Common;

namespace ConstructionMaterialsApi.Middlewares
{
    /// <summary>
    /// Global Exception Handling Middleware
    /// Bắt tất cả exception và trả về response chuẩn hoá
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (UserFriendlyException ex)
            {
                _logger.LogWarning(ex, "Business/User error: {Message}", ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError,
                    "Đã xảy ra lỗi không mong muốn trên hệ thống.",
                    new List<string> { ex.Message });
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode statusCode,
            string message,
            List<string>? errors = null)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse<object>.FailureResponse(message, errors);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}
