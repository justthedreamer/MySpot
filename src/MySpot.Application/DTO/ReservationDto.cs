namespace MySpot.Application.DTO;

public sealed record ReservationDto(Guid Id, Guid ParkingSpotId, string EmployeeName, string Type, DateTime Date)
{
}