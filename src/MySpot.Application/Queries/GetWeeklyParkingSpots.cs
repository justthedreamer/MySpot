using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Core.Entities;

namespace MySpot.Application.Queries;

public sealed record GetWeeklyParkingSpots(DateTime? Date) :  IQuery<IEnumerable<WeeklyParkingSpotDto>>
{
}