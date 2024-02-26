using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForVehicleHandler(
    IWeeklyParkingSpotRepository weeklyParkingSpotRepository,
    IParkingReservationService parkingReservationService)
    : ICommandHandler<ReserveParkingSpotForVehicle>
    {

    
    public async Task HandleAsync(ReserveParkingSpotForVehicle command)
    {
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var week = new Week(command.Date);

        var weeklyParkingSpots = (await weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
        
        var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        
        if (parkingSpotToReserve is null)
        {
            throw new WeeklyParkingSpotNotFoundException(command.ParkingSpotId);
        }
        
        var reservation = new VehicleReservation(command.ReservationId, command.ParkingSpotId,command.EmployeeName,command.LicensePlate,command.Date,command.Capacity);
        
        parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots,JobTitle.Employee,parkingSpotToReserve,reservation);
        
        await weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);
    }
}