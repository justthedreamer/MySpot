using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, guid => new ReservationId(guid));

        builder.Property(x => x.Date)
            .HasConversion(x => x.Value, date => new Date(date));

        builder.Property(x => x.ParkingSpotId)
            .HasConversion(x => x.Value, guid => new ParkingSpotId(guid));

        builder.Property(x => x.EmployeeName)
            .HasConversion(x => x.Value, s => new EmployeeName(s));

        builder.Property(x => x.LicencePlate)
            .HasConversion(x => x.Value, s => new LicencePlate(s));
    }
}