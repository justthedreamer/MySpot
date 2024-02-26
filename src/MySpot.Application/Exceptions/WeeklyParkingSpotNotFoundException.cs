using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public sealed class WeeklyParkingSpotNotFoundException(Guid id)
    : CustomException($"Weekly parking spot with ID:{id} was not found.")
{
    public Guid Id { get; } = id;
}