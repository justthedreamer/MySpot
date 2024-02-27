namespace MySpot.Core.ValueObjects;

public sealed record Week
{
    public Week(DateTimeOffset value)
    {
        var pastDays = value.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)value.DayOfWeek;
        var remainingDays = 7 - pastDays;
        From = new Date(value.AddDays(-1 * pastDays + 1));
        To = new Date(value.AddDays(remainingDays));
    }

    public Date From { get; }
    public Date To { get; }

    public override string ToString()
    {
        return $"{From} -> {To}";
    }
}