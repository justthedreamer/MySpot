using MySpot.Api.Exceptions;

namespace MySpot.Api.Entities;

public class WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
{
    private readonly HashSet<Reservation> _reservations = [];
    
    public Guid Id { get; } = id;
    public DateTime From { get;} = from;
    public DateTime To { get;} = to;
    public string Name { get;} = name;
    public IEnumerable<Reservation> Reservations => _reservations;

    public void AddReservation(Reservation reservation, DateTime now)
    {
        var isReservationDateValid = reservation.Date.Date  >= From && reservation.Date <= To && reservation.Date.Date > now;

        if (!isReservationDateValid)
        {
            throw new InvalidReservationDateException($"Invalid reservation date. It should be between ${From:d} and {To:d}");
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