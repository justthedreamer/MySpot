namespace MySpot.Api.Exceptions;

public sealed class InvalidEntityIdException(Guid message) : CustomException($"{message} is invalid.")
{
    
}