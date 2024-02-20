using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories;

public interface IWeeklyParkingSpotRepository
{
    Task<List<WeeklyParkingSpot>> GetAll();
    WeeklyParkingSpot? GetById(ParkingSpotId id);
    Task Add(WeeklyParkingSpot weeklyParkingSpot);
    Task Update(WeeklyParkingSpot weeklyParkingSpot);
    Task Delete(WeeklyParkingSpot weeklyParkingSpot);
    Task Commit();
}