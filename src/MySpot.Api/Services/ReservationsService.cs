using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationsService(IClock _clock)
{
    
    private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = 
        [
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), DateTime.UtcNow, DateTime.UtcNow.AddDays(7),"P5")
        ];

    public ReservationDto Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly() => WeeklyParkingSpots.SelectMany(x => x.Reservations).Select(x => new ReservationDto()
    {
        Id = x.Id,
        ParkingSpotId = x.ParkingSpotId,
        LicensePlate = x.LicencePlate.Value,
        Date = x.Date
    });

    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default!;
        }

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,command.LicensePlate,command.Date);
        
        weeklyParkingSpot.AddReservation(reservation,_clock.Current());
        
        return command.ReservationId;
    }

    public bool UpdateLicensePlate(UpdateLicensePlate command)
    {
        var existingReservation = WeeklyParkingSpots.SelectMany(x => x.Reservations).SingleOrDefault(x => x.Id == command.ReservationId);

        if (existingReservation is null)
        {
            return false;
        }
        
        existingReservation.ChangeLicensePlate(command.LicensePlate);
        return true;
    }

    public bool DeleteReservation(CancelReservation command)
    {
        var existingReservation = WeeklyParkingSpots.SelectMany(x => x.Reservations).SingleOrDefault(x => x.Id == command.ReservationId);
  
        if (existingReservation is null) return false;

        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(existingReservation.Id);
        
        weeklyParkingSpot!.RemoveReservation(existingReservation);
        
        return true;
    }
    
    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) => WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(reservation => reservation.Id == reservationId));
}