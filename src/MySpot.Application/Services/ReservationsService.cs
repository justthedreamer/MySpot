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
        var reservations = weeklyParkingSpots.SelectMany(x => x.Reservations);
        return reservations.Select(x => new ReservationDto()
        {
            Date = x.Date.Value.Date,
            LicensePlate = x.LicencePlate,
            ParkingSpotId = x.ParkingSpotId,
            Id = x.Id
        });
    }
    
    public async Task<Guid?> CreateAsync(CreateReservation command)
    {
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var week = new Week(clock.Current());
        
        var weeklyParkingSpots = (await weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
        
        var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        
        if (parkingSpotToReserve is null)
        {
            return default!;
        }

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName,command.LicensePlate,command.Date);
        
        //TODO jobtitle
        parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots,JobTitle.Employee,parkingSpotToReserve,reservation);
        
        await weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);
        return command.ReservationId;
    }
    
    public async Task<bool> UpdateLicensePlateAsync(UpdateLicensePlate command)
    {

        var weeklyParkingSpot = await GetWeeklyByReservationId(command.ReservationId);


        if (weeklyParkingSpot is null)
        {
            //todo exception
            return false;
        }

        var reservation = weeklyParkingSpot.Reservations.Single(x => x.Id.Value == command.ReservationId);
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