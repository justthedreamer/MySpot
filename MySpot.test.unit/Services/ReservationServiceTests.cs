using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using Shouldly;
using Xunit;

namespace MySpot.test.unit.Services;

public class ReservationServiceTests
{
    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succeed()
    {
        // ARRANGE
        var parkingSpot = _weeklyParkingSpots.First();
        var command = new CreateReservation(parkingSpot.Id,Guid.NewGuid(),
            "John Doe", "XYZ123", DateTime.UtcNow.AddMinutes(5));
        // ACT
        var reservationId = _reservationsService.Create(command);
        
        // ASSERT
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }

    #region Arrange

    private static readonly Clock Clock = new Clock();
    private readonly ReservationsService _reservationsService;

    public readonly List<WeeklyParkingSpot> _weeklyParkingSpots =
    [
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"),new Week(Clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"),new Week(Clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"),new Week(Clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"),new Week(Clock.Current()), "P1"),
        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"),new Week(Clock.Current()), "P1"),
    ];
    
    public ReservationServiceTests()
    {
        _reservationsService = new ReservationsService(new Clock(),_weeklyParkingSpots);
    }

    #endregion
}