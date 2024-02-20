using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;

internal sealed class WeeklyParkingSpotConfiguration : IEntityTypeConfiguration<WeeklyParkingSpot>
{
    public void Configure(EntityTypeBuilder<WeeklyParkingSpot> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, guid => new ParkingSpotId(guid));

        builder.Property(x => x.Week)
            .HasConversion(x => x.To.Value, offset => new Week(offset));

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, s => new ParkingSpotName(s));

    }
}