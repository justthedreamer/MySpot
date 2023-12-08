using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public sealed class WeeklyParkingSpotNotFoundException : CustomException
{
    public Guid Id { get; }

    public WeeklyParkingSpotNotFoundException(Guid id) : base($"Weekly Parking Spot with ID {id} was not found.")
    {
        Id = id;
    }

    public WeeklyParkingSpotNotFoundException(string message) : base(message)
    {
    }
}