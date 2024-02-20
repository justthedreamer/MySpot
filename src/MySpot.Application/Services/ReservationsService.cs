using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public class ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
{
    private readonly IClock _clock = clock;

    public ReservationDto Get(Guid id) => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly() => weeklyParkingSpotRepository.GetAll().SelectMany(x => x.Reservations).Select(x =>
        new ReservationDto()
        {
            Date = x.Date.Value.Date,
            LicensePlate = x.LicencePlate,
            ParkingSpotId = x.ParkingSpotId,
            Id = x.Id
        });
    public Guid? Create(CreateReservation command)
    {
        var weeklyParkingSpot = weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Id == command.ParkingSpotId);
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
        var reservation = GetWeeklyParkingSpotByReservation(command.ReservationId).Reservations.SingleOrDefault(x => x.Id.Value == command.ReservationId);


        if (reservation is null)
        {
            return false;
        }
        
        reservation.ChangeLicensePlate(command.LicensePlate);
        return true;
    }

    public bool DeleteReservation(CancelReservation command)
    {
        var reservation = GetWeeklyParkingSpotByReservation(command.ReservationId).Reservations.SingleOrDefault(x => x.Id.Value == command.ReservationId);
        
        if (reservation is null) return false;

        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(reservation.Id);
        
        weeklyParkingSpot!.RemoveReservation(reservation);
        
        return true;
    }
    
    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId) => weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Reservations.Any(reservation => reservation.Id.Value == reservationId));
}