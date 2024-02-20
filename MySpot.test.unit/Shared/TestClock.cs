using MySpot.Api.Services;

namespace MySpot.test.unit.Shared;

public class TestClock : IClock
{
    public DateTime Current() => new DateTime(2024, 02, 20);
}