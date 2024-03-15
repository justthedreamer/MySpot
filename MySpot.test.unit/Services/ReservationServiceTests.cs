using MySpot.Core.Abstractions;
using MySpot.Core.Policies;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.test.unit.Shared;
using Xunit;

namespace MySpot.test.unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public async Task given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        // ARRANGE
        // ACT
        // ASSERT
    }

    #region Arrange

    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly ParkingReservationService _parkingReservationService;

    public ReservationServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _parkingReservationService = new ParkingReservationService(new IReservationPolicy[]
        {
            new BossReservationPolicy(),
            new RegularEmployeeReservationPolicy(_clock),
            new ManagerReservationPolicy()
        }, _clock);
    }

    #endregion
}