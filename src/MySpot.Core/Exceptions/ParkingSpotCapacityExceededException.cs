using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions;

public sealed class ParkingSpotCapacityExceededException(ParkingSpotId id) : CustomException($"Parking spot with ID: {id} exceeds its reservation capacity.")
{
    public ParkingSpotId Id { get; } = id;
}