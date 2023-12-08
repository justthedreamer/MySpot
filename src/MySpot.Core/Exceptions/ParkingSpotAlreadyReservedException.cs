
using System;

namespace MySpot.Core.Exceptions;

public class ParkingSpotAlreadyReservedException : CustomException
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ParkingSpotAlreadyReservedException(string name, DateTime date) : base($"Parking spot: {name} is already reserved ad {date:d}.")
    {
        Name = name;
        Date = date;
    }
}