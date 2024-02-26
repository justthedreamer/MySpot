using MySpot.Application.Abstractions;
using MySpot.Application.DTO;

namespace MySpot.Application.Queries;

public sealed class GetWeeklyParkingSpot(Guid id) : IQuery<WeeklyParkingSpotDto>
{
    public Guid Id { get; init; } = id;
}