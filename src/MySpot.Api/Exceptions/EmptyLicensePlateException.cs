namespace MySpot.Api.Exceptions;

public sealed class EmptyLicensePlateException(string message) : CustomException(message)
{
}

