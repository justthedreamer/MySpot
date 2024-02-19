namespace MySpot.Api.Exceptions;

public sealed class InvalidParkingSpotNameException() : CustomException("Parking spot name is invalid.")
{
}