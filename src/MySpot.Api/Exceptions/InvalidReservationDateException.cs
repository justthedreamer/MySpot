namespace MySpot.Api.Exceptions;

public sealed class InvalidReservationDateException(string message) : CustomException(message)
{}