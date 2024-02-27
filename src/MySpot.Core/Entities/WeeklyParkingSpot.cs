using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class WeeklyParkingSpot
{
    public const int MaxCapacity = 2;

    private readonly HashSet<Reservation> _reservations = [];

    public WeeklyParkingSpot()
    {
    }

    private WeeklyParkingSpot(Guid id, Week week, string name, Capacity capacity)
    {
        Id = id;
        Week = week;
        Name = name;
        Capacity = capacity;
    }

    public ParkingSpotId Id { get; }
    public Week Week { get; }
    public ParkingSpotName Name { get; private set; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public Capacity Capacity { get; set; }

    public static WeeklyParkingSpot Create(ParkingSpotId id, Week week, ParkingSpotName parkingSpotName)
    {
        return new WeeklyParkingSpot(id, week, parkingSpotName, 2);
    }


    internal void AddReservation(Reservation reservation, Date now)
    {
        var isReservationDateValid =
            reservation.Date >= Week.From &&
            reservation.Date <= Week.To &&
            reservation.Date >= now;

        if (!isReservationDateValid)
            throw new InvalidReservationDateException(
                $"Invalid reservation date. It should be between ${Week.From} and {Week.To}. But is {reservation.Date}");

        var dateCapacity = _reservations.Where(x => x.Date == reservation.Date)
            .Sum(x => x.Capacity);

        if (dateCapacity + reservation.Capacity > Capacity) throw new ParkingSpotCapacityExceededException(Id);

        _reservations.Add(reservation);
    }

    public void RemoveReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }

    public void RemoveReservations(IEnumerable<Reservation> reservations)
    {
        _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
    }
}