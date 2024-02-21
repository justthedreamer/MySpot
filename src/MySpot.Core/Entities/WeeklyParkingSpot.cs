using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class WeeklyParkingSpot
{
    private readonly HashSet<Reservation> _reservations = [];
    public ParkingSpotId Id { get; private set; }
    public Week Week { get; private set; }
    public ParkingSpotName Name { get; private set; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public WeeklyParkingSpot()
    {
    }
    public WeeklyParkingSpot(Guid id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }
    
    internal void AddReservation(Reservation reservation, Date now)
    {
        var isReservationDateValid = 
            reservation.Date  >= Week.From && 
            reservation.Date <= Week.To && 
            !(reservation.Date < now);

        if (!isReservationDateValid)
        {
            throw new InvalidReservationDateException($"Invalid reservation date. It should be between ${Week.From} and {Week.To}. But is {reservation.Date}");
        }

        var reservationAlreadyExist = _reservations.Any(x => x.Date == reservation.Date);

        if (reservationAlreadyExist)
        {
            throw new ReservationAlreadyExistException();
        }
        
        _reservations.Add(reservation);
    }

    public void RemoveReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }
    
    
}