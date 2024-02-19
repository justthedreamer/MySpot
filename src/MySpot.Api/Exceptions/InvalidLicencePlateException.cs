namespace MySpot.Api.Exceptions;

public sealed class InvalidLicencePlateException(string message) : CustomException(message)
{
}