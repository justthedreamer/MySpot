using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Handlers;

internal sealed class GetWeeklyParkingSpotsHandler(MySpotDbContext dbContext)
    : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>>
{
    public async Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpots query)
    {
        var week = query.Date.HasValue ? new Week(query.Date.Value) : null;
        return await dbContext.WeeklyParkingSpots
            .Where(wps => wps.Week == week || week == null)
            .Include(ps => ps.Reservations)
            .AsNoTracking()
            .Select(ps => ps.GetDto())
            .ToListAsync();
    }
}