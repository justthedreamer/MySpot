using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public sealed record ReservationId
{
    public ReservationId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);

        Value = value;
    }

    public Guid Value { get; }

    public static ReservationId Create()
    {
        return new ReservationId(Guid.NewGuid());
    }

    public static implicit operator Guid(ReservationId date)
    {
        return date.Value;
    }

    public static implicit operator ReservationId(Guid value)
    {
        return new ReservationId(value);
    }
}