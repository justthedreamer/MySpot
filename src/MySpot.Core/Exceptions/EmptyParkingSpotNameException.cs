namespace MySpot.Core.Exceptions;

public class EmptyParkingSpotNameException: CustomException
{
    public EmptyParkingSpotNameException() : base("Parking spot name cannot be empty")
    {
    }
}