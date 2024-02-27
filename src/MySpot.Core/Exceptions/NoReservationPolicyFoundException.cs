namespace MySpot.Core.Exceptions;

public sealed class NoReservationPolicyFoundException(string jobTitle)
    : CustomException($"No reservation policy for {jobTitle} has been found.")
{
    public string JobTitle { get; } = jobTitle;
}