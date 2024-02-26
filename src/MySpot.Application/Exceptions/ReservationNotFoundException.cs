using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public sealed class ReservationNotFoundException(Guid id) : CustomException($"Cannot find reservation with ID: {id}")
{
    
}