using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public sealed record ParkingSpotId
{
    public ParkingSpotId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);

        Value = value;
    }

    public Guid Value { get; }

    public static ParkingSpotId Create()
    {
        return new ParkingSpotId(Guid.NewGuid());
    }

    public static implicit operator Guid(ParkingSpotId date)
    {
        return date.Value;
    }

    public static implicit operator ParkingSpotId(Guid value)
    {
        return new ParkingSpotId(value);
    }

    public override string ToString()
    {
        return Value.ToString("N");
    }
}