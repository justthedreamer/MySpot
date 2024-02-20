using MySpot.Application.Services;
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

    public async Task<List<WeeklyParkingSpot>> GetAll() => _weeklyParkingSpots;

    public WeeklyParkingSpot? GetById(ParkingSpotId id) => _weeklyParkingSpots.SingleOrDefault(x => x.Id == id);

    public async Task Add(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Add(weeklyParkingSpot);

    public async Task Update(WeeklyParkingSpot weeklyParkingSpot){} 

    public async Task Delete(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Remove(weeklyParkingSpot);

    public async Task Commit()
    {
    }
}