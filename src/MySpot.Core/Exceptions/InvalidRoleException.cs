namespace MySpot.Core.Exceptions;

public sealed class InvalidRoleException(string role) : CustomException($"Given role: {role} is invalid.")
{
    
}