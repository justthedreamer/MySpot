namespace MySpot.Core.Exceptions;

public class InvalidLicensePlateException : CustomException
{
    public InvalidLicensePlateException(string licencePlate) : base($"License plate :{licencePlate} is invalid")
    {
    }
}