using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class Reservation
{
    public ReservationId Id { get; private set; }
    public ParkingSpotId ParkingSpotId { get; private set; }
    public EmployeeName EmployeeName { get; private set; }
    public LicencePlate LicencePlate { get; private set; }
    public Date Date { get; private set; }

    public void ChangeLicensePlate(LicencePlate licencePlate)
    {
        LicencePlate = licencePlate;
    }
    public Reservation(Guid id, Guid parkingSpotId, string employeeName, string licensePlate, Date date)
    {
        Id = id;
        ParkingSpotId = parkingSpotId;
        EmployeeName = employeeName;
        LicencePlate = licensePlate;
        Date = date;
    }
    public Reservation()
    {
    }
}
