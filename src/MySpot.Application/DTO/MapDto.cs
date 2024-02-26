using MySpot.Core.Entities;

namespace MySpot.Application.DTO;

public static class MapDto
{
    public static ReservationDto GetDto(this Reservation reservation) => new ReservationDto(
            Id: reservation.Id,
            ParkingSpotId: reservation.ParkingSpotId,
            EmployeeName: reservation is VehicleReservation vs ? vs.EmployeeName : string.Empty,
            Type: nameof(reservation),
            Date: reservation.Date.Value.Date);

    public static WeeklyParkingSpotDto GetDto(this WeeklyParkingSpot weeklyParkingSpot) => new WeeklyParkingSpotDto(
        Id: weeklyParkingSpot.Id,
        Name: weeklyParkingSpot.Name,
        From: weeklyParkingSpot.Week.From.Value.Date,
        To: weeklyParkingSpot.Week.From.Value.Date,
        Capacity: weeklyParkingSpot.Capacity,
        Reservations: weeklyParkingSpot.Reservations.Select(r => r.GetDto())
    );
}