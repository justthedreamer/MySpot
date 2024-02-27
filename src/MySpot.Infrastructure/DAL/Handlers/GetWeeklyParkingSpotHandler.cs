using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Exceptions;
using MySpot.Application.Queries;

namespace MySpot.Infrastructure.DAL.Handlers;

internal sealed class GetWeeklyParkingSpotHandler(MySpotDbContext dbContext)
    : IQueryHandler<GetWeeklyParkingSpot, WeeklyParkingSpotDto>
{
    public async Task<WeeklyParkingSpotDto> HandleAsync(GetWeeklyParkingSpot query)
    {
        var weeklyParkingSpot =
            await dbContext
                .WeeklyParkingSpots
                .Include(x => x.Reservations)
                .SingleOrDefaultAsync(x => x.Id.Value == query.Id);

        if (weeklyParkingSpot == null) throw new WeeklyParkingSpotNotFoundException(query.Id);

        return weeklyParkingSpot.GetDto();
    }
}