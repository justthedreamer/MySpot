using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

public sealed class ReservationAlreadyExistException() : CustomException("Reservation already exist.")
{
}