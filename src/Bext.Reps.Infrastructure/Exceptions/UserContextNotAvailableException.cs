namespace Bext.Reps.Infrastructure.Exceptions
{
    public sealed class UserContextNotAvailableException : ApplicationException
    {
        public UserContextNotAvailableException()
        {
        }

        public UserContextNotAvailableException(string message) : base(message)
        {
        }

        public UserContextNotAvailableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
