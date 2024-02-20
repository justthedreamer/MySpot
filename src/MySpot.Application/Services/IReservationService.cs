using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public interface IReservationService
{

    public ReservationDto? Get(ReservationId id);
    public IEnumerable<ReservationDto> GetAllWeekly();
    public Guid? Create(CreateReservation command);
    public bool UpdateLicensePlate(UpdateLicensePlate command);
    public bool DeleteReservation(CancelReservation command);
}