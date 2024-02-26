using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;

namespace MySpot.Application.Commands.Handlers;

public sealed class ChangeReservationLicensePlateHandler(IWeeklyParkingSpotRepository repository) : ICommandHandler<ChangeReservationLicensePlate> 
{
    
    public async Task HandleAsync(ChangeReservationLicensePlate command)
    {
        var weeklyParkingSpot = await repository.GetWeeklyByReservationId(command.ReservationId);

        if (weeklyParkingSpot == null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }

        var reservation = weeklyParkingSpot.Reservations.Single(x => x.Id.Value == command.ReservationId);

        if (reservation is not VehicleReservation vehicleReservation)
        {
            throw new InvalidReservationTypeException(reservation);
        }

        vehicleReservation.ChangeLicensePlate(command.LicensePlate);
    }
}