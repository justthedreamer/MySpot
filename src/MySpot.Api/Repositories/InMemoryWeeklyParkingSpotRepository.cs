using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories;

public class InMemoryWeeklyParkingSpotRepository(IClock clock) : IWeeklyParkingSpotRepository
{
    private readonly IClock _clock = clock;

    public readonly List<WeeklyParkingSpot> _weeklyParkingSpots =
    [
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(clock.Current()), "P1"),
    ];

    public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots;

    public WeeklyParkingSpot GetById(ParkingSpotId id) => _weeklyParkingSpots.SingleOrDefault(x => x.Id == id.Value);

    public void Add(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Add(weeklyParkingSpot);

    public void Update(WeeklyParkingSpot weeklyParkingSpot) {}

    public void Delete(WeeklyParkingSpot weeklyParkingSpot) => _weeklyParkingSpots.Remove(weeklyParkingSpot);
}