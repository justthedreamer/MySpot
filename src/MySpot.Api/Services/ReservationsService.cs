using MySpot.Api.Models;

namespace MySpot.Api.Services;

public class ReservationsService
{
    private static int _id = 1;
    private static readonly List<string> ParkingSpotNames = ["P1", "P2", "P3", "P4", "P5"];
    private static readonly List<Reservation> Reservations = [];

    public Reservation Get(int id) => Reservations.SingleOrDefault(x => x.Id == id);

    public IEnumerable<Reservation> GetAll() => Reservations;

    public int? Create(Reservation reservation)
    {
        if (ParkingSpotNames.All(x => x != reservation.ParkingSpotName))
        {
            return null;
        }
        reservation.Date = DateTime.Now.AddDays(1).Date;

        var reservationAlreadyExist = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName && x.Date == reservation.Date);

        if (reservationAlreadyExist)
        {
            return null;
        }
        
        reservation.Id = _id;
        _id++;
        Reservations.Add(reservation);

        return reservation.Id;
    }

    public bool UpdateLicensePlate(int id, string licensePlate)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return false;
        }

        existingReservation.LicensePlate = licensePlate;
        return true;
    }

    public bool DeleteReservation(int id)
    {
        var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null) return false;

        Reservations.Remove(existingReservation);
        return true;
    }
}