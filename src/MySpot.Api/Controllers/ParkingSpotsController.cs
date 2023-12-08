using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.Commands.Handlers;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spots")]
public class ParkingSpotsController : ControllerBase
{
    private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotForVehicleHandler;
    private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotForCleaningHandler;
    private readonly ICommandHandler<ChangeReservationLicensePlate> _changeReservationLicensePlateHandler;
    private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;
    private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _getWeeklyParkingSpotsHandler;

    public ParkingSpotsController(ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotForVehicleHandler,ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotForCleaningHandler, ICommandHandler<ChangeReservationLicensePlate> changeReservationLicensePlateHandler, ICommandHandler<DeleteReservation> deleteReservationHandler, IQueryHandler<GetWeeklyParkingSpots,IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler)
    {
        _reserveParkingSpotForVehicleHandler = reserveParkingSpotForVehicleHandler;
        _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
        _changeReservationLicensePlateHandler = changeReservationLicensePlateHandler;
        _deleteReservationHandler = deleteReservationHandler;
        _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
        => Ok(await _getWeeklyParkingSpotsHandler.HandleAsync(query));


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> Get(Guid id)
    {
        var reservation = await _reservationsService.GetAsync(id);
        
        if (reservation is null)
        {
            return NotFound();
        }
        
        return Ok(reservation);
    }
    
    [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
    public async Task<ActionResult> Post(Guid parkingSpotId,ReserveParkingSpotForVehicle command)
    {
        await _reserveParkingSpotForVehicleHandler.HandleAsync(command with
        {
            ReservationId = Guid.NewGuid(),
            ParkingSpotId = parkingSpotId
        });
        return NoContent();
    }
    
    [HttpPost("reservations/cleaning")]
    public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reserveParkingSpotForCleaningHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id,ChangeReservationLicensePlate command)
    {
        var result = await _reservationsService.ChangeReservationLicensePlateAsync(command with { ReservationId = id });
        if (result)
        {
            return NoContent();
        }
        
        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _reservationsService.DeleteReservationAsync(new DeleteReservation(id));
        if (result)
        {
            return NoContent();
        }else
            return NotFound();
    }
}