using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public sealed record LicencePlate
{
    public LicencePlate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidLicencePlateException(value);
        if (value.Length is < 5 or > 8) throw new InvalidLicencePlateException(value);

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(LicencePlate licencePlate)
    {
        return licencePlate.Value;
    }

    public static implicit operator LicencePlate(string value)
    {
        return new LicencePlate(value);
    }
}