namespace MySpot.Api.Exceptions;

public sealed class EmptyEmployeeNameException() : CustomException("Employee name cannot be empty.")
{
}