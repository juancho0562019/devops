namespace Bext.Reps.Domain.Commons.Exceptions
{
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
