namespace MySpot.Core.Exceptions;

public sealed class EmptyParkingSpotNameException() : CustomException("Parking spot name cannot be empty.")
{
}