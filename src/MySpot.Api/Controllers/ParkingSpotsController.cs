using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Services;
using MySpot.Core.Entities;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots")]
public class ParkingSpotsController(
    ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotForVehicleHandler,
    ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotForCleaningHandler,
    ICommandHandler<ChangeReservationLicensePlate> changeReservationLicensePlateHandler,
    ICommandHandler<DeleteReservation> deleteReservationHandler,
    IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler,
    IQueryHandler<GetWeeklyParkingSpot, WeeklyParkingSpotDto> getWeeklyParkingSpotHandler)
    : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots command) => 
        Ok(await getWeeklyParkingSpotsHandler.HandleAsync(command));

    [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
    public async Task<ActionResult> Post(Guid parkingSpotId,ReserveParkingSpotForVehicle command)
    {
        await reserveParkingSpotForVehicleHandler.HandleAsync(command with
        {
            ParkingSpotId = parkingSpotId,
            ReservationId = Guid.NewGuid()
        });

        return NoContent();
    }

    [HttpPost("reservations/cleaning")]
    public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await reserveParkingSpotForCleaningHandler.HandleAsync(command);
        return NoContent();
    }
    
    [HttpPut("reservations/{reservationId:guid}")]
    public async Task<ActionResult> Put(ChangeReservationLicensePlate command)
    {
        await changeReservationLicensePlateHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpDelete("{reservationId:guid}")]
    public async Task<ActionResult> Delete(Guid reservationId)
    {
        await deleteReservationHandler.HandleAsync(new DeleteReservation(reservationId));
        return NoContent();
    }
}