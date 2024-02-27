namespace MySpot.Core.Exceptions;

public sealed class InvalidPasswordException(string message) : CustomException(message)
{
}