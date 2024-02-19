using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private static int _id = 1;
    private static readonly List<Reservation> Reservations = [];
    private static readonly List<string> ParkingSpotNames = ["P1", "P2", "P3", "P4", "P5"];

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(Reservations);


    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = Reservations.SingleOrDefault(x => x.Id == id);
        
        if (reservation is null)
        {
            return BadRequest();
        }

        return Ok(reservation);
    }
    
    [HttpPost]
    public ActionResult<Reservation> Post(Reservation reservation)
    {
        if (ParkingSpotNames.All(x => x != reservation.ParkingSpotName))
        {
            return BadRequest();
        }
        reservation.Date = DateTime.Now.AddDays(1).Date;

        var reservationAlreadyExist = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName && x.Date == reservation.Date);

        if (reservationAlreadyExist)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return BadRequest();
        }
        
        reservation.Id = _id;
        _id++;
        Reservations.Add(reservation);

        return CreatedAtAction(nameof(Get),new {id = reservation.Id}, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Reservation reservation)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
        if (existingReservation is null) return NotFound();

        existingReservation.LicensePlate = reservation.LicensePlate;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var reservation = Reservations.SingleOrDefault(x => x.Id == id);
        if (reservation is null) return NotFound();

        Reservations.Remove(reservation);
        return NoContent();
    }
}