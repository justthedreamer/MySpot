using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationsService _reservationsService;

    public ReservationsController()
    {
        _reservationsService = new ReservationsService();
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(_reservationsService.GetAllWeekly());


    [HttpGet("{id:guid}")]
    public ActionResult<Reservation> Get(Guid id)
    {
        var reservation = _reservationsService.Get(id);
        
        if (reservation is null)
        {
            return BadRequest();
        }

        return Ok(reservation);
    }
    
    //CREATE
    [HttpPost]
    public ActionResult<Reservation> Post(CreateReservation command)
    {
        var createdReservationId = _reservationsService.Create(command with{ReservationId = Guid.NewGuid()});

        if (createdReservationId is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id = createdReservationId },null);
    }

    //UPDATE LICENSE PLATE
    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id,UpdateLicensePlate command)
    {
        var result = _reservationsService.UpdateLicensePlate(command with {ReservationId = id});

        if (!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        var result = _reservationsService.DeleteReservation(new CancelReservation(id));
        if (!result) return NotFound();
        
        return NoContent();
    }
}