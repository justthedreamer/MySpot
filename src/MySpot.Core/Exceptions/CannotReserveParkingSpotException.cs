using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions;

public sealed class CannotReserveParkingSpotException(ParkingSpotId parkingSpotId)
    : CustomException($"Cannot reserve {parkingSpotId} parking spot.")
{
    public ParkingSpotId ParkingSpotId { get; } = parkingSpotId;
}