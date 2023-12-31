﻿using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public sealed class VehicleReservation : Reservation
{
    public EmployeeName EmployeeName { get; private set; }
    public LicensePlate LicensePlate { get; private set; }

    private VehicleReservation(){}
    public VehicleReservation(ReservationId id, ParkingSpotId parkingSpotId,EmployeeName employeeName,LicensePlate licensePlate , Date date, Capacity capacity) : base(id, parkingSpotId, date, capacity)
    {
        EmployeeName = employeeName;
        ChangeLicensePlate(licensePlate);
    }

    public void ChangeLicensePlate(LicensePlate licensePlate) => LicensePlate = licensePlate;
}
