using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Policies;

internal sealed class BossReservationPolicy : IReservationPolicy
{
    public bool CanBeApplied(JobTitle jobTitle)
    {
        return jobTitle == JobTitle.Boss;
    }

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        return true;
    }
}