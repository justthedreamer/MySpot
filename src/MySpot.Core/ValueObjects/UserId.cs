using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public sealed record UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static implicit operator UserId(Guid value) => new UserId(value);
    public static implicit operator Guid(UserId userId) => userId.Value;
}