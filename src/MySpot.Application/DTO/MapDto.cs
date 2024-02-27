using MySpot.Core.Entities;

namespace MySpot.Application.DTO;

public static class MapDto
{
    public static ReservationDto GetDto(this Reservation reservation)
    {
        return new ReservationDto(
            reservation.Id,
            reservation.ParkingSpotId,
            reservation is VehicleReservation vs ? vs.EmployeeName : string.Empty,
            nameof(reservation),
            reservation.Date.Value.Date);
    }

    public static WeeklyParkingSpotDto GetDto(this WeeklyParkingSpot weeklyParkingSpot)
    {
        return new WeeklyParkingSpotDto(
            weeklyParkingSpot.Id,
            weeklyParkingSpot.Name,
            weeklyParkingSpot.Week.From.Value.Date,
            weeklyParkingSpot.Week.From.Value.Date,
            weeklyParkingSpot.Capacity,
            weeklyParkingSpot.Reservations.Select(r => r.GetDto())
        );
    }

    public static UserDto GetDto(this User user)
    {
        return new UserDto(
            Id: user.Id,
            Username: user.Username,
            Fullname: user.FullName
        );

    }
}