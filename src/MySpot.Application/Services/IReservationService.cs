using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public interface IReservationService
{

    Task<ReservationDto?> GetAsync(ReservationId id);
    Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync();
    Task<Guid?> CreateAsync(CreateReservation command);
    Task<bool> UpdateLicensePlateAsync(UpdateLicensePlate command);
    Task<bool> DeleteReservationAsync(CancelReservation command);
}
