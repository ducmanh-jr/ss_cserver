namespace ConstructionMaterialsApi.Models.Common
{
    /// <summary>
    /// Chuẩn hoá API response cho toàn bộ hệ thống
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Tạo response thành công
        /// </summary>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }

        /// <summary>
        /// Tạo response thất bại
        /// </summary>
        public static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors
            };
        }
    }
}
