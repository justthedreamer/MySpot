using MySpot.Core.Entities;
using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public sealed class InvalidReservationTypeException(Reservation reservation)
    : CustomException($"Invalid reservation type: {nameof(reservation)}")
{
}