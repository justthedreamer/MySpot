using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities;

public class Reservation(Guid id, Guid parkingSpotId, string employeeName, string licensePlate, DateTime date)
{
    public Guid Id { get; } = id;
    public Guid ParkingSpotId { get; set; } = parkingSpotId;
    public string EmployeeName { get; private set; } = employeeName;
    public LicencePlate LicencePlate { get; private set; }
    public DateTime Date { get; private set; } = date;

    public void ChangeLicensePlate(LicencePlate licencePlate)
    {
        LicencePlate = licencePlate;
    }
}
