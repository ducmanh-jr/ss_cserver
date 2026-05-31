namespace ConstructionMaterialsApi.Exceptions
{
    /// <summary>
    /// Exception dùng để thông báo lỗi thân thiện với người dùng
    /// Thường dùng cho các trường hợp Not Found hoặc lỗi logic nghiệp vụ
    /// </summary>
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message) : base(message)
        {
        }
    }
}
