using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Services;
using MySpot.Core.Entities;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationsService;

    public ReservationsController(IReservationService reservationsService)
    {
        _reservationsService = reservationsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> Get()
    {
        var reservations = await _reservationsService.GetAllWeekly();
        return Ok(reservations);
    }


    [HttpGet("{id:guid}")]
    public async Task <ActionResult<Reservation>> Get(Guid id)
    {
        var reservation = await _reservationsService.Get(id);
        
        if (reservation is null)
        {
            return BadRequest();
        }

        return Ok(reservation);
    }
    
    //CREATE
    [HttpPost]
    public async Task<ActionResult<Reservation>> Post(CreateReservation command)
    {
        var createdReservationId = await _reservationsService.Create(command with{ReservationId = Guid.NewGuid()});

        if (createdReservationId is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id = createdReservationId },null);
    }

    //UPDATE LICENSE PLATE
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id,UpdateLicensePlate command)
    {
        var result = await _reservationsService.UpdateLicensePlate(command with {ReservationId = id});

        if (!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _reservationsService.DeleteReservation(new CancelReservation(id));
        if (!result) return NotFound();
        
        return NoContent();
    }
}