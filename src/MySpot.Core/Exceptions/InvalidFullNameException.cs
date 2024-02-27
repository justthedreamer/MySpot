namespace MySpot.Core.Exceptions;

public sealed class InvalidFullNameException(string fullName) : CustomException($"Given name is invalid : {fullName}")
{
}