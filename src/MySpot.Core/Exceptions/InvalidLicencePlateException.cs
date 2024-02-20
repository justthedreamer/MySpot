namespace MySpot.Core.Exceptions;

public sealed class InvalidLicencePlateException(string message) : CustomException(message)
{
}