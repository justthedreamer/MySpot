using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Entities;

public class WeeklyParkingSpot(Guid id, Week week, string name)
{
    private readonly HashSet<Reservation> _reservations = [];
    
    public Guid Id { get; } = id;
    public Week Week { get; } = week;
    public string Name { get;} = name;
    public IEnumerable<Reservation> Reservations => _reservations;

    public void AddReservation(Reservation reservation, Date now)
    {
        var isReservationDateValid = 
            reservation.Date  >= Week.From && 
            reservation.Date <= Week.To && 
            reservation.Date > now;

        if (!isReservationDateValid)
        {
            throw new InvalidReservationDateException($"Invalid reservation date. It should be between ${Week.From:d} and {Week.To:d}");
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