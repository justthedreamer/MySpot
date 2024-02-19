using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

public class Reservation(Guid id, Guid parkingSpotId, string employeeName, string licensePlate, DateTime date)
{
    public Guid Id { get; } = id;
    public Guid ParkingSpotId { get; set; } = parkingSpotId;
    public string EmployeeName { get; private set; } = employeeName;
    public string LicensePlate { get; private set; } = licensePlate;
    public DateTime Date { get; private set; } = date;

    public void ChangeLicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new EmptyLicensePlateException();
        }

        LicensePlate = licensePlate;
    }
}
