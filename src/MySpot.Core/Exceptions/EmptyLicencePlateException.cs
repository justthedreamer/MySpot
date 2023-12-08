namespace MySpot.Core.Exceptions;

public sealed class EmptyLicencePlateException: CustomException
{
    public EmptyLicencePlateException() : base("Licence plate is empty")
    {
    }
}