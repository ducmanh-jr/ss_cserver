using System.Net;
using NguyenDucManh0210668.Constants;
using NguyenDucManh0210668.Exceptions;

namespace NguyenDucManh0210668.Utils;

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
        catch (UserFriendlyException0210668De1 exception)
        {
            var statusCode = GetUserFriendlyStatusCode(exception.Message);
            await WriteErrorResponseAsync(context, exception.Message, statusCode);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Đã xảy ra lỗi hệ thống.");
            await WriteErrorResponseAsync(context, "Đã xảy ra lỗi hệ thống.", StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task WriteErrorResponseAsync(HttpContext context, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var response = ApiResponse0210668De1<object>.Fail(message, statusCode);
        await context.Response.WriteAsJsonAsync(response);
    }

    private static int GetUserFriendlyStatusCode(string message)
    {
        var notFoundMessages = new[]
        {
            MessageConstants0210668De1.NotFoundNhanVien,
            MessageConstants0210668De1.NotFoundDuAn,
            MessageConstants0210668De1.NotFoundPhanCong
        };

        return notFoundMessages.Contains(message) ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest;
    }
}
