namespace MySpot.Core.Exceptions;

public sealed class InvalidEntityIdException(Guid message) : CustomException($"{message} is invalid.")
{
    
}