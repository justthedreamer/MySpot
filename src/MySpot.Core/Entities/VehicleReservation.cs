using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public sealed class VehicleReservation : Reservation
{
    public EmployeeName EmployeeName { get; private set; }
    public LicencePlate LicencePlate { get; private set; }


    private VehicleReservation(){}
    public VehicleReservation(ReservationId id, ParkingSpotId parkingSpotId, Date date, EmployeeName employeeName, LicencePlate licencePlate) : base(id, parkingSpotId, date)
    {
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
    }
    
    public void ChangeLicensePlate(LicencePlate licencePlate)
    {
        LicencePlate = licencePlate;
    }
}