using System.Runtime.CompilerServices;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Exceptions;
using MySpot.Core.Policies;
using MySpot.Core.ValueObjects;

[assembly: InternalsVisibleTo("MySpot.test.unit")]
namespace MySpot.Core.Services;

internal sealed class ParkingReservationService(IEnumerable<IReservationPolicy> policies, IClock clock)
    : IParkingReservationService
{
    
    public void ReserveSpotForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTitle, WeeklyParkingSpot parkingSpotToReserve,
        VehicleReservation reservation){}
    public void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTitle, WeeklyParkingSpot parkingSpotToReserve,
        VehicleReservation reservation)
    {
        var parkingSpotId = parkingSpotToReserve.Id;
        var policy = policies.SingleOrDefault(x => x.CanBeApplied(jobTitle));

        if (policy is null)
        {
            throw new NoReservationPolicyFoundException(jobTitle);
        }

        if (!policy.CanReserve(allParkingSpots,reservation.EmployeeName))
        {
            throw new CannotReserveParkingSpotException(parkingSpotId);
        }
        
        parkingSpotToReserve.AddReservation(reservation,new Date(clock.Current()));
    }
    public void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots, Date date)
    {
        foreach (var parkingSpot in allParkingSpots)
        {
            var cleaningReservation = new CleaningReservation(new ReservationId(Guid.NewGuid()), parkingSpot.Id, date);

            var reservations = parkingSpot.Reservations.Where(x => x.Date == date);
            
            parkingSpot.RemoveReservations(reservations);
            
            parkingSpot.AddReservation(cleaningReservation,new Date(clock.Current()));
        }
    }
}