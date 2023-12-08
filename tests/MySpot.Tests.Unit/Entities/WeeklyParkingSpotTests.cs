using System.Runtime.InteropServices.JavaScript;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Entities;

public class WeeklyParkingSpotTests
{
    [Theory]
    [InlineData("2023-11-02")]
    [InlineData("2023-11-09")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        //arrange
        var invalidDate = DateTime.Parse(dateString);
        var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "DDD01A",
            new Date(invalidDate), 1);

        //act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, new Date(_date)));

        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void add_reservation_when_other_already_exist_should_fail()
    {
        //arrange
        var id = Guid.NewGuid();
        var reservationDate = _date.AddDays(1);
        var reservation = new VehicleReservation(Guid.NewGuid(), id, "Mariusz ", "XXX01", reservationDate, 1);

        var nextReservation = new VehicleReservation(Guid.NewGuid(), id, "Mariusz ", "XXX01", reservationDate, 1);

        //act        
        _weeklyParkingSpot.AddReservation(reservation, _date);

        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, new Date(_date)));

        //assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
    }

    [Fact]
    public void given_reservation_for_not_taken_date_add_reservation_should_succesed()
    {
        //arrange
        var reservationDate = _date.AddDays(1);
        var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "Mariusz ", "XXX01",
            reservationDate, 1);

        //act
        _weeklyParkingSpot.AddReservation(reservation, _date);
        //assert
        _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
    }

    #region Arrange

    private readonly Date _date;
    private readonly WeeklyParkingSpot _weeklyParkingSpot;

    public WeeklyParkingSpotTests()
    {
        /*Monday*/
        _date = new Date(new DateTime(2023, 11, 3));

        _weeklyParkingSpot = WeeklyParkingSpot.Create(Guid.NewGuid(), new Week(_date), "P1");
    }

    #endregion
}