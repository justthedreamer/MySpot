using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class RegularEmployeeReservationPolicy(IClock clock) : IReservationPolicy
{
    public bool CanBeApplied(JobTitle jobTitle) => (jobTitle == JobTitle.Employee);

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservation = weeklyParkingSpots
            .SelectMany(x => x.Reservations)
            .Count(r => r.EmployeeName == employeeName);

        return totalEmployeeReservation < 2 && (clock.Current().Hour > 4);
    }
}