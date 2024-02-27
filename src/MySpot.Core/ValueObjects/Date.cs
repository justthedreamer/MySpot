namespace MySpot.Core.ValueObjects;

public sealed record Date
{
    public Date(DateTimeOffset value)
    {
        Value = value.Date;
    }

    public DateTimeOffset Value { get; }

    public static Date Now => new(DateTimeOffset.Now);

    public Date AddDays(int days)
    {
        return new Date(Value.AddDays(days));
    }

    public static implicit operator DateTimeOffset(Date date)
    {
        return date.Value;
    }

    public static implicit operator Date(DateTimeOffset value)
    {
        return new Date(value);
    }

    public static bool operator <(Date date1, Date date2)
    {
        return date1.Value < date2.Value;
    }

    public static bool operator >(Date date1, Date date2)
    {
        return date1.Value > date2.Value;
    }

    public static bool operator <=(Date date1, Date date2)
    {
        return date1.Value <= date2.Value;
    }

    public static bool operator >=(Date date1, Date date2)
    {
        return date1.Value >= date2.Value;
    }

    public override string ToString()
    {
        return Value.ToString("d");
    }
}