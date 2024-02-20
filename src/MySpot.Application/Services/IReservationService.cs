using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public interface IReservationService
{

    public Task<ReservationDto?> Get(ReservationId id);
    public Task<IEnumerable<ReservationDto>> GetAllWeekly();
    public Task<Guid?> Create(CreateReservation command);
    public Task<bool> UpdateLicensePlate(UpdateLicensePlate command);
    public Task<bool> DeleteReservation(CancelReservation command);
}