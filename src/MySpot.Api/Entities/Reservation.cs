using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities;

public class Reservation(Guid id, Guid parkingSpotId, string employeeName, string licensePlate, DateTimeOffset date)
{
    public ReservationId Id { get; } = id;
    public ParkingSpotId ParkingSpotId { get; set; } = parkingSpotId;
    public EmployeeName EmployeeName { get; private set; } = employeeName;
    public LicencePlate LicencePlate { get; private set; }
    public Date Date { get; private set; } = date;

    public void ChangeLicensePlate(LicencePlate licencePlate)
    {
        LicencePlate = licencePlate;
    }
}
