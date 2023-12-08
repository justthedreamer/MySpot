using MySpot.Application.Abstractions;
using MySpot.Core.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly IParkingReservationService _parkingReservationService;

    public ReserveParkingSpotForCleaningHandler(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository,IParkingReservationService parkingReservationService)
    {
        _clock = clock;
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
        _parkingReservationService = parkingReservationService;
    }
    
    public async Task HandleAsync(ReserveParkingSpotForCleaning command)
    {
            
        var date = new Date(command.Date);
        var week = new Week(_clock.Current());
        
        var allParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
        
        _parkingReservationService.ReserveParkingForCleaning(allParkingSpots,date);

        foreach (var parkingSpot in allParkingSpots)
        {
            await _weeklyParkingSpotRepository.UpdateAsync(parkingSpot);
        }
    }
}