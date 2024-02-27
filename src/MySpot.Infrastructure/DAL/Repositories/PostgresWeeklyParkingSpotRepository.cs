using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories;

internal class PostgresWeeklyParkingSpotRepository(MySpotDbContext dbContext) : IWeeklyParkingSpotRepository
{
    public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
    {
        return await dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .ToListAsync();
    }

    public async Task<WeeklyParkingSpot?> GetAsync(ParkingSpotId id)
    {
        return await dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
    {
        return await dbContext.WeeklyParkingSpots.Where(x => x.Week == week)
            .Include(x => x.Reservations)
            .ToListAsync();
    }

    public async Task<WeeklyParkingSpot> GetWeeklyByReservationId(ReservationId id)
    {
        return await dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Reservations.Any(x => x.Id == id));
    }

    public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        await dbContext.AddAsync(weeklyParkingSpot);
    }

    public Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        dbContext.Update(weeklyParkingSpot);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        dbContext.Remove(weeklyParkingSpot);
        return Task.CompletedTask;
    }

    public async Task Commit()
    {
        await dbContext.SaveChangesAsync();
    }
}