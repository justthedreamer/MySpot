using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories;

internal class PostgresWeeklyParkingSpotRepository(MySpotDbContext dbContext) : IWeeklyParkingSpotRepository
{
    public async  Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync() => 
        await dbContext.WeeklyParkingSpots
        .Include(x => x.Reservations)
        .ToListAsync();

    public async Task<WeeklyParkingSpot?> GetAsync(ParkingSpotId id) =>
        await dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week) =>
        await dbContext.WeeklyParkingSpots.Where(x => x.Week == week)
            .Include(x => x.Reservations)
            .ToListAsync();

    public async Task<WeeklyParkingSpot> GetWeeklyByReservationId(ReservationId id) =>
        await dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Reservations.Any(x => x.Id == id));

    public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        await dbContext.AddAsync(weeklyParkingSpot);
        await Commit();
    }
    public async Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        dbContext.Update(weeklyParkingSpot);
        await Commit();
    }
    public async Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        dbContext.Remove(weeklyParkingSpot);
        await Commit();
    }
    public async Task Commit() => await dbContext.SaveChangesAsync();
}