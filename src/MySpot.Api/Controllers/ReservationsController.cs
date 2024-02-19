using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Entities;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationsService _reservationsService;

    public ReservationsController(ReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(_reservationsService.GetAll());


    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
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
    public ActionResult<Reservation> Post(Reservation reservation)
    {
        var createdReservationId = _reservationsService.Create(reservation);

        if (createdReservationId is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { id = createdReservationId },null);
    }

    //UPDATE LICENSE PLATE
    [HttpPut("{id:int}")]
    public ActionResult Put(int id,string licensePlate)
    {
        var result = _reservationsService.UpdateLicensePlate(id, licensePlate);

        if (!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = _reservationsService.DeleteReservation(id);
        if (!result) return NotFound();
        
        return NoContent();
    }
}