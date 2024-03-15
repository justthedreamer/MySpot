using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace MySpot.test.unit.Entities;

public class WeeklyParkingSpotTests
{
    [Theory]
    [InlineData("2024-02-11")]
    [InlineData("2024-02-18")]
    public void given_invalid_date_add_for_vehicle_reservation_should_fail(string dateString)
    {
        // ARRANGE
        var invalidDate = DateTimeOffset.Parse(dateString);
        var reservation =
            new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John doe", "KLI0021", invalidDate, 2);

        // ACT
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, new Date(_now)));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void given_reservation_for_already_reserved_parking_spot_add_reservation_should_fail()
    {
        // ARRANGE
        var reservationDate = _now.AddDays(1);
        var reservation =
            new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", reservationDate, 2);
        var nextReservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123",
            reservationDate, 2);

        _weeklyParkingSpot.AddReservation(reservation, new Date(_now));
        // ACT
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, new Date(_now)));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ParkingSpotCapacityExceededException>();
    }

    [Fact]
    public void giver_reservation_for_not_reserved_parking_spot_reservation_should_succeed()
    {
        // ARRANGE
        var reservationDate = _now.AddDays(1);
        var reservation =
            new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", reservationDate, 2);
        // ACT
        _weeklyParkingSpot.AddReservation(reservation, new Date(_now));
        // ASSERT
        _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
    }


    #region Arrange

    private readonly Date _now;
    private readonly WeeklyParkingSpot _weeklyParkingSpot;


    public WeeklyParkingSpotTests()
    {
        _now = new Date(DateTimeOffset.UtcNow);
        _weeklyParkingSpot = WeeklyParkingSpot.Create(Guid.NewGuid(), new Week(_now), "P1");
    }

    #endregion
}