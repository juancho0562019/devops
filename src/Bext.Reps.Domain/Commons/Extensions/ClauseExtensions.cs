
namespace Bext.Reps.Domain.Commons.Extensions;
public static class ClauseExtensions
{

    public static T ValidateNull<T>(this T? input, string? parameterName = null, string? message = null)
    {
        if (input is null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(parameterName, DefaultMessage.IsRequired);
            }
            throw new ArgumentNullException(parameterName, message);
        }

        return input;
    }

    public static string ValidateNotEmpty(this string? input, string? parameterName = null, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException(DefaultMessage.CannotBeEmpty, parameterName);
            }
            throw new ArgumentException(message, parameterName);
        }

        return input;
    }

    public static string ValidateNotNullOrEmpty(this string? input, string? parameterName = null, string? message = null)
    {
        input = input.ValidateNull(parameterName, message);
        input = input.ValidateNotEmpty(parameterName, message);

        return input;
    }

}

