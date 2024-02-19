using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[Route("reservations")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public void Get()
    {
        
    }

    [HttpPost]
    public void Post([FromBody]Reservation reservation)
    {
        
    }
}