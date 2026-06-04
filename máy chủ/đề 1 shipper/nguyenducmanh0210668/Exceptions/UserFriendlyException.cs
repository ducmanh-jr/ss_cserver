using System;

namespace nguyenducmanh0210668.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message) : base(message) { }
    }
}
