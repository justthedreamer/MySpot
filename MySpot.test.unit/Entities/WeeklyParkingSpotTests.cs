using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;
using Shouldly;
using Xunit;

namespace MySpot.test.unit.Entities;

public class WeeklyParkingSpotTests
{

    [Theory]
    [InlineData("2024-02-11")]
    [InlineData("2024-02-18")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
     // ARRANGE
     var invalidDate = DateTime.Parse(dateString);
     var reservation = new Reservation(Guid.NewGuid(),_weeklyParkingSpot.Id, "John doe", "KLI0021", invalidDate);
     
     // ACT
     var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation,new Date(_now))) ;
     
     // ASSERT
     exception.ShouldNotBeNull();
     exception.ShouldBeOfType<InvalidReservationDateException>();
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_should_fail()
    {
        // ARRANGE
        var reservationDate = _now.AddDays(1);
        var reservation =
            new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", reservationDate);
        _weeklyParkingSpot.AddReservation(reservation,_now);
        // ACT
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation,_now)) ;

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReservationAlreadyExistException>();
    }

    [Fact]
    public void giver_reservation_for_not_taken_date_add_reservation_should_succeed()
    {
        // ARRANGE
        var reservationDate = _now.AddDays(1);
        var reservation =
            new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Doe", "XYZ123", reservationDate);
        // ACT
        _weeklyParkingSpot.AddReservation(reservation,_now);
        // ASSERT
        _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
    }
    
    
    #region Arrange
    
    private readonly DateTimeOffset _now;
    private readonly WeeklyParkingSpot _weeklyParkingSpot;


    public WeeklyParkingSpotTests()
    {
        _now = DateTime.Now;
        _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
    }

    #endregion
}