using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Abstractions;
using MySpot.Core.Policies;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.test.unit.Shared;
using Shouldly;
using Xunit;

namespace MySpot.test.unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public async Task given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        // ARRANGE
        var parkingSpot = (await _weeklyParkingSpotRepository.GetAllAsync()).First();
        var command = new ReserveParkingSpotForVehicle(parkingSpot.Id,Guid.NewGuid(),
            "John Doe", "XYZ123", DateTime.UtcNow.AddMinutes(5));
        // ACT
        var reservationId = await _reservationsService.ReserveForVehicleAsync(command);
        
        // ASSERT
        reservationId.ShouldNotBeNull();
        reservationId.ShouldBe(command.ReservationId);
    }

    #region Arrange

    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly ReservationsService _reservationsService;
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
        
        _reservationsService = new ReservationsService(_clock,_weeklyParkingSpotRepository,_parkingReservationService);
    }

    #endregion
}