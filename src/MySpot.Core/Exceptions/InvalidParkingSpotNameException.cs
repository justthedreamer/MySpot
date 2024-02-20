namespace MySpot.Core.Exceptions;

public sealed class InvalidParkingSpotNameException() : CustomException("Parking spot name is invalid.")
{
}