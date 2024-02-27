namespace MySpot.Core.Exceptions;

public class UserAlreadyExistsException(string message) : CustomException($"User already exist.")
{
    
}