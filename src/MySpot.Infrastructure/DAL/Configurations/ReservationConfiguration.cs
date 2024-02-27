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

        builder.Property(x => x.Capacity)
            .HasConversion(x => x.Value, value => new Capacity(value));

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<CleaningReservation>(nameof(CleaningReservation))
            .HasValue<VehicleReservation>(nameof(VehicleReservation));
    }
}