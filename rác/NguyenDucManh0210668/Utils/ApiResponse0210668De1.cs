namespace NguyenDucManh0210668.Utils;

public class ApiResponse0210668De1<TData>
{
    public bool IsSuccess { get; set; }
    public TData? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public int Code { get; set; }

    public static ApiResponse0210668De1<TData> Success(TData? data, string message, int code = StatusCodes.Status200OK)
    {
        return new ApiResponse0210668De1<TData>
        {
            IsSuccess = true,
            Data = data,
            Message = message,
            Code = code
        };
    }

    public static ApiResponse0210668De1<TData> Fail(string message, int code)
    {
        return new ApiResponse0210668De1<TData>
        {
            IsSuccess = false,
            Data = default,
            Message = message,
            Code = code
        };
    }
}
