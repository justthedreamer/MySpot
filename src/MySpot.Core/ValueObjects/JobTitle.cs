namespace MySpot.Core.ValueObjects;

public sealed record JobTitle
{
    public const string Employee = nameof(Employee);
    public const string Manager = nameof(Manager);
    public const string Boss = nameof(Boss);

    private JobTitle(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(JobTitle jobTitle)
    {
        return jobTitle.Value;
    }

    public static implicit operator JobTitle(string value)
    {
        return new JobTitle(value);
    }
}