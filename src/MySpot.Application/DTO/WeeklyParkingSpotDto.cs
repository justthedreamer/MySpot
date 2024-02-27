namespace MySpot.Application.DTO;

public sealed record WeeklyParkingSpotDto(
    Guid Id,
    string Name,
    DateTime From,
    DateTime To,
    int Capacity,
    IEnumerable<ReservationDto> Reservations)
{
}