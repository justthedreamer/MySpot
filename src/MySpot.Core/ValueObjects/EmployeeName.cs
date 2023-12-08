using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public record EmployeeName(string Value)
{
    public string Name { get; } = Value ?? throw new EmptyEmployeeNameException();

    public static implicit operator string(EmployeeName name) => name.Value;

    public static implicit operator EmployeeName(string value) =>
        new(value);

}