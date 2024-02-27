using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public sealed class VehicleReservation : Reservation
{
    private VehicleReservation()
    {
    }

    public VehicleReservation(ReservationId id, ParkingSpotId parkingSpotId, EmployeeName employeeName,
        LicencePlate licencePlate, Date date, Capacity capacity) : base(id, parkingSpotId, date, capacity)
    {
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
    }

    public EmployeeName EmployeeName { get; private set; }
    public LicencePlate LicencePlate { get; private set; }

    public void ChangeLicensePlate(LicencePlate licencePlate)
    {
        LicencePlate = licencePlate;
    }
}