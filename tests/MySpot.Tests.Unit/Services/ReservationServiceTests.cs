using MySpot.Application.Commands;
using MySpot.Application.Services;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Policies;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Tests.Unit.Shared;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public async Task create_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        //arrange
        var parkingSpot = (await _weeklyParkingSpotRepository.GetAllAsync()).First();
        var command = new ReserveParkingSpotForVehicle(parkingSpot.Id, Guid.NewGuid(),
            "Arek Mi elc", "XXX-1", _clock.Current(),1);

        //act
        var reservationId = await _reservationsService.ReserveForVehicleAsync(command);
        
        //assert
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }

    #region Arrage

    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly IReservationService _reservationsService;

    public ReservationServiceTests()
    {
        
        _clock = new TestClock();
        
        var parkingReservationService = new ParkingReservationService(new IReservationPolicy[]
        {
            new RegularEmployeeReservationPolicy(_clock),
            new ManagerReservationPolicy(),
            new BossReservationPolicy()
        }, _clock);
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotRepository,parkingReservationService);

    }

    #endregion
}