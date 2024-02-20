namespace MySpot.Core.Exceptions;

public sealed class EmptyLicensePlateException(string message) : CustomException(message)
{
}

