using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Repositories;

namespace MySpot.Application.Commands.Handlers;

public sealed class DeleteReservationHandler(IWeeklyParkingSpotRepository repository)
    : ICommandHandler<DeleteReservation>
{
    public async Task HandleAsync(DeleteReservation command)
    {
        var weeklyParkingSpot = await repository.GetWeeklyByReservationId(command.ReservationId);

        if (weeklyParkingSpot is null) throw new ReservationNotFoundException(command.ReservationId);

        var reservation = weeklyParkingSpot.Reservations
            .Single(x => x.Id.Value == command.ReservationId);
        weeklyParkingSpot
            .RemoveReservation(reservation);

        await repository.UpdateAsync(weeklyParkingSpot);
    }
}