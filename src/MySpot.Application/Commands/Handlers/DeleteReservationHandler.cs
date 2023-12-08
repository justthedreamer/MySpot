using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class DeleteReservationHandler : ICommandHandler<DeleteReservation>
{
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public DeleteReservationHandler(IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
    }
    
    public async Task HandleAsync(DeleteReservation command)
    {
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

        if (weeklyParkingSpot is null)
        {
            throw new WeeklyParkingSpotNotFoundException("Weekly parking spot was not found.");
        }
        
        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id.Value == command.ReservationId);
        
        if (existingReservation is null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
        
    }
    
    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(Guid commandReservationId)
    {
        var reservationId = new ReservationId(commandReservationId);
        var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAllAsync();
            
        return weeklyParkingSpot.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
    }
}

