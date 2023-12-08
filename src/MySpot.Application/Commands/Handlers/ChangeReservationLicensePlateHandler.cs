using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class ChangeReservationLicensePlateHandler : ICommandHandler<ChangeReservationLicensePlate>
{
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ChangeReservationLicensePlateHandler(IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
    }
    
    public async Task HandleAsync(ChangeReservationLicensePlate command)
    {
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);
        
        if (weeklyParkingSpot is null)
        {
            //TODO
            throw new WeeklyParkingSpotNotFoundException(Guid.NewGuid());
        }

        var existingReservation = weeklyParkingSpot.Reservations
            .OfType<VehicleReservation>()
            .SingleOrDefault(x => x.Id.Value == command.ReservationId);
        
        if (existingReservation is null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }
        
        existingReservation.ChangeLicensePlate(command.LicensePlate);
        await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
    }
    
    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(Guid commandReservationId)
    {
        var reservationId = new ReservationId(commandReservationId);
        var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAllAsync();
            
        return weeklyParkingSpot.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
    }
}