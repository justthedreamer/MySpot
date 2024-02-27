namespace MySpot.Core.Exceptions;

public sealed class InvalidUsernameException(string message) : CustomException(message)
{
}