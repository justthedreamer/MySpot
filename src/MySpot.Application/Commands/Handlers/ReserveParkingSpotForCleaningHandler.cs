using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForCleaningHandler(
    IParkingReservationService parkingReservationService,
    IWeeklyParkingSpotRepository weeklyParkingSpotRepository) : ICommandHandler<ReserveParkingSpotForCleaning>
{
    public async Task HandleAsync(ReserveParkingSpotForCleaning command)
    {
        var week = new Week(command.Date);
        var weeklyParkingSpots = (await weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();

        parkingReservationService.ReserveParkingForCleaning(weeklyParkingSpots, new Date(command.Date));

        var tasks = weeklyParkingSpots.Select(weeklyParkingSpotRepository.UpdateAsync);

        await Task.WhenAll(tasks);
    }
}