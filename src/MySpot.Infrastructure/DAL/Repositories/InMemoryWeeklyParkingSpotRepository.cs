using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repositories;

internal class InMemoryWeeklyParkingSpotRepository(IClock clock) : IWeeklyParkingSpotRepository
{
    private readonly IClock _clock = clock;

    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots =
    [
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P1"),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P1"),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P1"),
        WeeklyParkingSpot.Create(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P1")
    ];

    public Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
    {
        return Task.FromResult(_weeklyParkingSpots.AsEnumerable());
    }

    public Task<WeeklyParkingSpot?> GetAsync(ParkingSpotId id)
    {
        return Task.FromResult(_weeklyParkingSpots.SingleOrDefault(x => x.Id == id));
    }

    public Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
    {
        return Task.FromResult(_weeklyParkingSpots.Where(x => x.Week == week));
    }

    public Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Remove(weeklyParkingSpot);
        return Task.CompletedTask;
    }


    public Task Commit()
    {
        return Task.CompletedTask;
    }
}