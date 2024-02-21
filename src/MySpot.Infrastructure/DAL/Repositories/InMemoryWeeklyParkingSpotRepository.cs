using MySpot.Application.Services;
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
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current()), "P1"),
    ];

    public Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync() => Task.FromResult(_weeklyParkingSpots.AsEnumerable());

    public Task<WeeklyParkingSpot?> GetAsync(ParkingSpotId id) =>
        Task.FromResult(_weeklyParkingSpots.SingleOrDefault(x => x.Id == id));

    public Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot) => Task.CompletedTask;

    public Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Remove(weeklyParkingSpot);
        return Task.CompletedTask;
    }
    

    public Task Commit() => Task.CompletedTask;
}