namespace MySpot.Core.Exceptions;

public sealed class InvalidEmailAddressException(string email) : CustomException($"Email Address : {email} is invalid.")
{
}