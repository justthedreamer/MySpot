namespace MySpot.Core.Exceptions;

public class EmptyEmployeeNameException : CustomException
{
    public EmptyEmployeeNameException() : base("Employee name cannot be empty.")
    {
    }
}