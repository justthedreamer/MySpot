using MySpot.Core.Abstractions;

namespace MySpot.test.unit.Shared;

public class TestClock : IClock
{
    public DateTime Current()
    {
        return new DateTime(2024, 02, 20);
    }
}