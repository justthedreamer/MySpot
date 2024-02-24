using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public class ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository, IParkingReservationService parkingReservationService) : IReservationService
{
    public async Task<ReservationDto?> GetAsync(ReservationId id)
    {
        var weeklyParkingSpots = await GetAllWeeklyAsync();
        return weeklyParkingSpots.SingleOrDefault(x => x.Id == id.Value);
    }
    public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
    {
        var weeklyParkingSpots = await weeklyParkingSpotRepository.GetAllAsync();
        var reservations = weeklyParkingSpots.SelectMany(w => w.Reservations);
        return reservations.Select(r => new ReservationDto()
        {
            Date = r.Date.Value.Date,
            EmployeeName = r is VehicleReservation vr ? vr.EmployeeName : string.Empty,
            ParkingSpotId = r.ParkingSpotId,
            Id = r.Id
        });
    }
    public async Task<Guid?> ReserveForVehicleAsync(ReserveParkingSpotForVehicle command)
    {
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var week = new Week(command.Date);

        var weeklyParkingSpots = (await weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
        
        var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        
        if (parkingSpotToReserve is null)
        {
            return default!;
        }
        
        var reservation = new VehicleReservation(command.ReservationId, command.ParkingSpotId,command.EmployeeName,command.LicensePlate,command.Date,command.Capacity);
        
        //TODO job title
        parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots,JobTitle.Employee,parkingSpotToReserve,reservation);
        
        await weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);
        return command.ReservationId;
    }
    public async Task ReserveForCleaningAsync(ReserveParkingSpotForCleaning command)
    {   
        var week = new Week(command.Date);

        var weeklyParkingSpots = (await weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
        
        parkingReservationService.ReserveParkingForCleaning(weeklyParkingSpots,new Date(command.Date));

        foreach (var weeklyParkingSpot in weeklyParkingSpots)
        {
            await weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
        }
    }
    public async Task<bool> UpdateLicensePlateAsync(UpdateLicensePlate command)
    {

        var weeklyParkingSpot = await GetWeeklyByReservationId(command.ReservationId);

        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var reservation = weeklyParkingSpot.Reservations.OfType<VehicleReservation>()
            .Single(x => x.Id.Value == command.ReservationId);
        
        reservation.ChangeLicensePlate(command.LicensePlate);

        await weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);

        return true;
    }
    public async Task<bool> DeleteReservationAsync(CancelReservation command)
    {
        var weeklyParkingSpot = await GetWeeklyByReservationId(command.ReservationId);

        if (weeklyParkingSpot is null)
        {
            //todo exception
            return false;
        }

        var reservation = weeklyParkingSpot.Reservations.Single(x => x.Id.Value == command.ReservationId);
        weeklyParkingSpot.RemoveReservation(reservation);

        await weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);
        
        return true;
    }
    private async Task<WeeklyParkingSpot?> GetWeeklyByReservationId(ReservationId id)
    {
        var weeklySpots = await weeklyParkingSpotRepository.GetAllAsync();
        return weeklySpots.SingleOrDefault(x => x.Reservations.Any(reservation => reservation.Id == id));
    }
    
}   