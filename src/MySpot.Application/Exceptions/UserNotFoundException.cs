using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public sealed class UserNotFoundException() : CustomException("User not found.")
{
    
}