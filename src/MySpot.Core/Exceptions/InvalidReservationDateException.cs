namespace MySpot.Core.Exceptions;

public sealed class InvalidReservationDateException(string message) : CustomException(message)
{}