using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.test.unit.Shared;
using Shouldly;
using Xunit;

namespace MySpot.test.unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        // ARRANGE todo
        var parkingSpot = _weeklyParkingSpotRepository.GetAll().Result.First();
        var command = new CreateReservation(parkingSpot.Id,Guid.NewGuid(),
            "John Doe", "XYZ123", DateTime.UtcNow.AddMinutes(5));
        // ACT
        var reservationId = _reservationsService.Create(command);
        
        // ASSERT
        reservationId.ShouldNotBeNull();
        reservationId.Result.Value.ShouldBe(command.ReservationId);
    }

    #region Arrange

    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly ReservationsService _reservationsService;

 
    
    public ReservationServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
}