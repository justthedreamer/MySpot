using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services;

public class ReservationsService(IClock clock)
{
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots = 
        [
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()) ,"P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()),"P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()),"P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()),"P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()),"P5")
        ];

    public ReservationDto Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly() => _weeklyParkingSpots.SelectMany(x => x.Reservations).Select(x =>
        new ReservationDto()
        {
            Date = x.Date.Value.Date,
            LicensePlate = x.LicencePlate,
            ParkingSpotId = x.ParkingSpotId,
            Id = x.Id
        });
    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default!;
        }

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,command.LicensePlate,command.Date);
        
        weeklyParkingSpot.AddReservation(reservation,new Date(clock.Current()));
        
        return command.ReservationId;
    }

    public bool UpdateLicensePlate(UpdateLicensePlate command)
    {
        var existingReservation = _weeklyParkingSpots.SelectMany(x => x.Reservations).SingleOrDefault(x => x.Id.Value == command.ReservationId);

        if (existingReservation is null)
        {
            return false;
        }
        
        existingReservation.ChangeLicensePlate(command.LicensePlate);
        return true;
    }

    public bool DeleteReservation(CancelReservation command)
    {
        var existingReservation = _weeklyParkingSpots.SelectMany(x => x.Reservations).SingleOrDefault(x => x.Id.Value == command.ReservationId);
  
        if (existingReservation is null) return false;

        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(existingReservation.Id);
        
        weeklyParkingSpot!.RemoveReservation(existingReservation);
        
        return true;
    }
    
    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) => _weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(reservation => reservation.Id.Value == reservationId));
}