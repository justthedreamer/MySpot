using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;

internal sealed class VehicleReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
{
    public void Configure(EntityTypeBuilder<VehicleReservation> builder)
    {
        builder.Property(x => x.EmployeeName)
            .HasConversion(x => x.Value, s => new EmployeeName(s));

        builder.Property(x => x.LicencePlate)
            .HasConversion(x => x.Value, s => new LicencePlate(s));    
    }
}