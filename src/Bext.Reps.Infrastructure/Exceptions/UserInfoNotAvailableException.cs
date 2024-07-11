namespace Bext.Reps.Infrastructure.Exceptions
{
    public sealed class UserInfoNotAvailableException : ApplicationException
    {
        public UserInfoNotAvailableException()
        {
        }

        public UserInfoNotAvailableException(string message) : base(message)
        {
        }

        public UserInfoNotAvailableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
