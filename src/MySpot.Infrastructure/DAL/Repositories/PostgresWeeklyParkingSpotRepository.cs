using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories;

internal class PostgresWeeklyParkingSpotRepository(MySpotDbContext dbContext) : IWeeklyParkingSpotRepository
{
    public async Task<List<WeeklyParkingSpot>> GetAll() => await dbContext.WeeklyParkingSpots
        .Include(x => x.Reservations)
        .ToListAsync();

    public WeeklyParkingSpot? GetById(ParkingSpotId id) => dbContext.WeeklyParkingSpots
        .Include(x => x.Reservations)
        .SingleOrDefault(x => x.Id == id);

    public async Task Add(WeeklyParkingSpot weeklyParkingSpot)
    {
        await dbContext.AddAsync(weeklyParkingSpot);
        await Commit();
    }
    public async Task Update(WeeklyParkingSpot weeklyParkingSpot)
    {
        dbContext.Update(weeklyParkingSpot);
        await Commit();
    }

    public async Task Delete(WeeklyParkingSpot weeklyParkingSpot)
    {
        dbContext.Remove(weeklyParkingSpot);
        await Commit();
    }

    public async Task Commit() => await dbContext.SaveChangesAsync();
}