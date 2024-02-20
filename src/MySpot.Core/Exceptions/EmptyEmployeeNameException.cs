namespace MySpot.Core.Exceptions;

public sealed class EmptyEmployeeNameException() : CustomException("Employee name cannot be empty.")
{
}